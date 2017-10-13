using BackEnd.Dal;
using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using BackEnd.Helpers;
using System.Transactions;

namespace BackEnd.BL
{
    public interface IJadwalTglBL
    {
        void Save(JadwalTglModel jadwalTgl);

        void Delete(string kodeDokter, string tanggal, string jam);

        void Delete(string kodeDokter, string tanggal);

        JadwalTglModel GetData(string kodeDokter, string tanggal, string jam);

        List<JadwalTglModel> ListData(DokterModel dokter, string tanggal);

        List<JadwalTglModel> ListData(LayananModel layanan, string tanggal);

        List<JadwalTglModel> Generate(string tanggal);
    }

    public class JadwalTglBL:IJadwalTglBL
    {
        IJadwalTglDal _jadwalTglDal;
        IJadwalHariBL _jadwalHariBL;
        IDokterBL _dokterBL;
        ILayananBL _layananBL;

        public JadwalTglBL()
        {
            _jadwalTglDal = new JadwalTglDal();
            _jadwalHariBL = new JadwalHariBL();
            _dokterBL = new DokterBL();
            _layananBL = new LayananBL();
        }

        public JadwalTglBL(IJadwalTglDal injJadwalTglDal,
                           IJadwalHariBL injJadwalHariBL,
                           IDokterBL injDokterBL,
                           ILayananBL injLayananBL)
        {
            _jadwalTglDal = injJadwalTglDal;
            _jadwalHariBL = injJadwalHariBL;
            _dokterBL = injDokterBL;
            _layananBL = injLayananBL;
        }

        public void Save(JadwalTglModel jadwalTgl)
        {
            //  validasi dokter
            if (_layananBL.IsExist(jadwalTgl.KodeLayanan))
            {
                throw new ArgumentException("KODE LAYANAN invalid");
            }

            //  validasi layanan
            if (_dokterBL.IsExist(jadwalTgl.KodeDokter))
            {
                throw new ArgumentException("KODE DOKTER invalid");
            }

            //  validasi tgl
            if (!DateTimeHelpers.IsValidTgl(jadwalTgl.TglJadwal,"dd-MM-yyyy"))
            {
                throw new ArgumentException("TGL JADWAL invalid");
            }

            //  validasi jam
            if (!DateTimeHelpers.IsValidJam(jadwalTgl.Jam, "HH:mm"))
            {
                throw new ArgumentException("JAM JADWAL invalid");
            }

            //  validasi booked
            if(jadwalTgl.Booked > jadwalTgl.Max)
            {
                throw new ArgumentException("BOOKED invalid");
            }

            //  simpan data
            if (_jadwalTglDal.IsExist(jadwalTgl.KodeDokter,
                jadwalTgl.TglJadwal, jadwalTgl.Jam))
            {
                _jadwalTglDal.Insert(jadwalTgl);
            }
            else
            { 
                _jadwalTglDal.Update(jadwalTgl);
            }
        }

        public void Delete(string kodeDokter, string tanggal, string jam)
        {
            _jadwalTglDal.Delete(kodeDokter, tanggal, jam);
        }

        public void Delete(string kodeDokter, string tanggal)
        {
            //  ambil semua jam dari dokter dan tanggal tsb
            var jadwals = _jadwalTglDal.ListData(tanggal, new DokterModel { Kode = kodeDokter });
            TransactionOptions tranOpt = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            };
            using (TransactionScope tran = new TransactionScope(TransactionScopeOption.Required, tranOpt))
            {
                foreach (var jadwal in jadwals)
                {
                    _jadwalTglDal.Delete(kodeDokter, tanggal, jadwal.Jam);
                }
                tran.Complete();
            }
        }

        public JadwalTglModel GetData(string kodeDokter, string tanggal, string jam)
        {
            return _jadwalTglDal.GetData(kodeDokter, tanggal, jam);
        }

        public List<JadwalTglModel> ListData(DokterModel dokter, string tanggal)
        {
            return _jadwalTglDal.ListData(tanggal, dokter);
        }

        public List<JadwalTglModel> ListData(LayananModel layanan, string tanggal)
        {
            return _jadwalTglDal.ListData(tanggal, layanan);
        }

        /// <summary>
        ///     Mengenerate Jadwal Dokter per-Tanggal, 
        ///     dari jadwal per hari
        /// </summary>
        /// <param name="kodeDokter"></param>
        /// <param name="tanggal"></param>
        /// <returns></returns>
        public List<JadwalTglModel> Generate(string tanggal)
        {
            List<JadwalTglModel> retVal = null;

            //  convert tanggal menjadi hari
            var trueDate = DateTimeHelpers.ToDate(tanggal, "dd-MM-yyyy");
            var hari = (int)trueDate.DayOfWeek;

            //  ambil semua jadwal yang ada pada hari tsb
            var jadwals = _jadwalHariBL.ListData(hari);
            var jadwalToday = jadwals.Where(x => x.Hari == hari);

            if (jadwalToday != null) retVal = new List<JadwalTglModel>();

            //  convert ke jadwalTgl
            foreach(var item in jadwalToday)
            {
                foreach(var itemPerJam in item.JadwalPerJams)
                {
                    var jadwalTgl = new JadwalTglModel
                    {
                        KodeDokter = item.KodeDokter,
                        NamaDokter = item.NamaDokter,
                        KodeLayanan = item.KodeLayanan,
                        NamaLayanan = item.NamaLayanan,
                        TglJadwal = tanggal,
                        Jam = itemPerJam.Jam,
                        Durasi = itemPerJam.Durasi,
                        Max = itemPerJam.Max,
                        Booked = itemPerJam.Booked
                    };
                    retVal.Add(jadwalTgl);
                }
            }
            return retVal;
        }
    }
}