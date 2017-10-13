using BackEnd.Dal;
using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;

namespace BackEnd.BL
{
    public interface IJadwalHariBL
    {
        bool IsExist(string id);
        bool IsExist(string kodeDokter, int hari, string jam);
        bool IsExist(string kodeDokter, string tanggal, string jam);
        void Save(JadwalHariModel jadwal);
        void Delete(string id);
        void Delete(string kodeDokter, int hari, string jamMulai);
        JadwalHariModel GetById(string id);
        string GetId(string kodeDokter, int hari, string jamMulai);
        List<JadwalHariModel> ListData();
        List<JadwalHariModel> ListData(LayananModel layanan);
        List<JadwalHariModel> ListData(DokterModel dokter);
        List<JadwalHariModel> ListData(int hari);

    }

    public class JadwalHariBL:IJadwalHariBL
    {
        IJadwalHariDal _jadwalHariDal;
        IDokterBL _dokterBL;
        ILayananBL _layananBL;
        IParamNoBL _paramNoBL;

        public JadwalHariBL()
        {
            _jadwalHariDal = new JadwalHariDal();
            _dokterBL = new DokterBL();
            _layananBL = new LayananBL();
            _paramNoBL = new ParamNoBL();
        }

        public JadwalHariBL(IJadwalHariDal injJadwalHariDal,
                        IDokterBL injDokterBL, 
                        ILayananBL injLayananBL,
                        IParamNoBL injParamNoBL) 
        {
            _jadwalHariDal = injJadwalHariDal;
            _dokterBL = injDokterBL;
            _layananBL = injLayananBL;
            _paramNoBL = injParamNoBL;
        }

        public bool IsExist(string id)
        {
            var jadwal = _jadwalHariDal.GetById(id);
            if (jadwal == null)
            {
                return false;
            }
            else return true;
        }

        public bool IsExist(string kodeDokter, int hari, string jam)
        {
            var retVal = false;
            //  query data dokter
            var jadwals = _jadwalHariDal.ListData(new DokterModel { Kode = kodeDokter });
            if (jadwals == null)
            {
                return false;
            }
            //  query hari
            var jadwalshari = jadwals.Where(x => x.Hari == hari).ToList();
            //  query jam
            foreach (JadwalHariModel item in jadwalshari)
            {
                retVal = item.JadwalPerJams.Exists(x => x.Jam == jam);
            }
            return retVal;
        }

        public bool IsExist(string kodeDokter, string tanggal, string jam)
        {
            //  convert tanggak ke hari
            int hari = (int)DateTime.Parse(tanggal).DayOfWeek;
            //  call function satunya :-D
            return IsExist(kodeDokter, hari, jam);
        }

        public JadwalHariModel GetById(string id)
        {
            return _jadwalHariDal.GetById(id);
        }

        public string GetId(string kodeDokter, int hari, string jamMulai)
        {
            return _jadwalHariDal.GetId(kodeDokter, hari, jamMulai);
        }

        public void Save(JadwalHariModel jadwal)
        {
            //--validasi
            //  dokter
            if (_dokterBL.GetById(jadwal.KodeDokter).Nama.Trim() == "")
            {
                throw new ArgumentException("Invalid DOKTER");
            }
        
            //  layanan
            if(_layananBL.GetById(jadwal.KodeLayanan).Nama.Trim() == "")
            {
                throw new ArgumentException("Invalid LAYANAN");
            }
            
            //  hari
            if (jadwal.Hari < 1 || jadwal.Hari > 7)
            {
                throw new ArgumentException("Invalid HARI");
            }

            //  jam mulai
            TimeSpan jamMulai;
            if (!TimeSpan.TryParse(jadwal.JamMulai, out jamMulai))
            {
                throw new ArgumentException("Invalid JAM MULAI");
            }

            //  jam selesai
            TimeSpan jamSelesai;
            if (!TimeSpan.TryParse(jadwal.JamSelesai, out jamSelesai))
            {
                throw new ArgumentException("Invalid JAM SELESAI");
            }

            //  urutan jam mulai-jam selesai
            if (TimeSpan.Compare(jamMulai, jamSelesai) == 1)
            {
                throw new ArgumentException("Invalid JAM MULAI-SELESAI");
            }
            
            //--validasi detil per jam
            foreach(JadwalHariPerJamModel item in jadwal.JadwalPerJams)
            {
                //  format jam
                TimeSpan jamDetil;
                if (!TimeSpan.TryParse(item.Jam, out jamDetil))
                {
                    throw new ArgumentException("Invalid Format Detil JAM");
                }

                //  jam detil harus berada di range jam di header
                if (TimeSpan.Compare(jamMulai, jamDetil) == 1)
                {
                    throw new ArgumentException("Invalid Range Detil JAM");
                }
                if (TimeSpan.Compare(jamDetil, jamSelesai) == 1)
                {
                    throw new ArgumentException("Invalid Range Detil JAM");
                }
                
                //  booked tidak boleh melebihi max
                if(item.Booked > item.Max)
                {
                    throw new ArgumentException("Invalid BOOKED QTY");
                }
            }

            //  cek duplikasi dokter-hari-jam (case beda jadwal sama (ini)))
            //      ambil data header
            var result =
                from jadwalJam in jadwal.JadwalPerJams
                group jadwalJam by jadwalJam.Jam into jamGroup
                select new { Jam = jamGroup.Key, Count = jamGroup.Count() };
            if(result.ToList().Exists(x => x.Count > 1))
            {
                throw new ArgumentException("Duplicated JAM PRAKTEK");
            }


            TransactionOptions tranOption = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            };
            using (TransactionScope tran = new TransactionScope(TransactionScopeOption.Required, tranOption))
            {
                //  generate kode jadwal jika kosong
                if (jadwal.Kode.Trim() == "")
                {
                    ParamNoModel paramNo = _paramNoBL.GetById("JD");
                    jadwal.Kode = _paramNoBL.FormatKode(paramNo, 5, "");
                    _jadwalHariDal.Insert(jadwal);
                }
                else
                {
                    _jadwalHariDal.Update(jadwal);
                }
                tran.Complete();
            }
        }

        public void Delete(string id)
        {
            if (IsExist(id))
            {
                _jadwalHariDal.Delete(id);
            }
        }

        public void Delete(string kodeDokter, int hari, string jamMulai)
        {
            string id = _jadwalHariDal.GetId(kodeDokter, hari, jamMulai);
            Delete(id);
        }

        public List<JadwalHariModel> ListData()
        {
            return _jadwalHariDal.ListData();
        }

        public List<JadwalHariModel> ListData(LayananModel layanan)
        {
            return _jadwalHariDal.ListData(layanan);
        }

        public List<JadwalHariModel> ListData(DokterModel dokter)
        {
            return _jadwalHariDal.ListData(dokter);
        }

        public List<JadwalHariModel> ListData(int hari)
        {
            return _jadwalHariDal.ListData(hari);
        }

        public void Clear()
        {
            _jadwalHariDal.Clear();
        }
    }
}