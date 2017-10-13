using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BackEnd.Models;
using System.Data.SqlClient;
using MyOnlineBooking.Helpers;

namespace BackEnd.Dal
{
    public interface IJadwalTglDal
    {
        void Insert(JadwalTglModel timeSlotJadwal);

        void Update(JadwalTglModel timeSlotJadwal);

        void Delete(string kodeDokter, string tanggal, string jam);

        bool IsExist(string kodeDokter, string tanggal, string jam);

        JadwalTglModel GetData(string kodeDokter, string tanggal, string jam);

        List<JadwalTglModel> ListData(string tanggal, LayananModel layanan);
        List<JadwalTglModel> ListData(string tanggal, DokterModel dokter);
        List<JadwalTglModel> ListData(string tanggal);

    }

    public class JadwalTglDal : IJadwalTglDal
    {
        string _connString;

        public JadwalTglDal()
        {
            _connString = DataAccessHelper.GetConnectionString();
        }

        public string GetConnectionString()
        {
            return _connString;
        }

        public void Insert(JadwalTglModel jadwalTgl)
        {
            string sSql = @"
                INSERT INTO ta_jadwal_tgl 
                            (
                                fs_kd_dokter, fs_kd_layanan, fd_tgl_jadwal,
                                fs_jam, fn_durasi, fn_max, fn_booked
                            )
                VALUES          
                            (
                                @KodeDokter, @KodeLayanan, @TglJadwal,
                                @Jam, @Durasi, @Max, @Booked
                            )";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                cmd.Parameters.AddWithValue("@KodeDokter", jadwalTgl.KodeDokter);
                cmd.Parameters.AddWithValue("@KodeLayanan", jadwalTgl.KodeLayanan);
                cmd.Parameters.AddWithValue("@TglJadwal", jadwalTgl.TglJadwal);
                cmd.Parameters.AddWithValue("@Jam", jadwalTgl.Jam);
                cmd.Parameters.AddWithValue("@Durasi", jadwalTgl.Durasi);
                cmd.Parameters.AddWithValue("@Max", jadwalTgl.Max);
                cmd.Parameters.AddWithValue("@Booked", jadwalTgl.Booked);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(string kodeDokter, string tanggal, string jam)
        {
            string sSql = @"
                DELETE  ta_jadwal_tgl
                WHERE   fs_kd_dokter = @KodeDokter
                    AND fd_tgl_jadwal = @tanggal 
                    AND fs_jam = @Jam ";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                cmd.Parameters.AddWithValue("@KodeDokter", kodeDokter);
                cmd.Parameters.AddWithValue("@Tanggal", tanggal);
                cmd.Parameters.AddWithValue("@Jam", jam);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(JadwalTglModel jadwalTgl)
        {
            string sSql = @"
                UPDATE  ta_jadwal_tgl 
                
                SET     fn_durasi = @Durasi, 
                        fn_max = @Max, 
                        fn_booked = @Booked

                WHERE   fs_kd_dokter = @KodeDokter
                    AND fs_kd_layanan = @KodeLayanan
                    AND fd_tgl_jadwal = @TglJadwal
                    AND fs_jam = @Jam ";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                cmd.Parameters.AddWithValue("@Durasi", jadwalTgl.Durasi);
                cmd.Parameters.AddWithValue("@Max", jadwalTgl.Max);
                cmd.Parameters.AddWithValue("@Booked", jadwalTgl.Booked);
                cmd.Parameters.AddWithValue("@KodeDokter", jadwalTgl.KodeDokter);
                cmd.Parameters.AddWithValue("@KodeLayanan", jadwalTgl.KodeLayanan);
                cmd.Parameters.AddWithValue("@TglJadwal", jadwalTgl.TglJadwal);
                cmd.Parameters.AddWithValue("@Jam", jadwalTgl.Jam);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public bool IsExist(string kodeDokter, string tanggal, string jam)
        {
            var dummy = GetData(kodeDokter, tanggal, jam);

            if (dummy == null) return false;
            else return true;
        }

        public JadwalTglModel GetData(string kodeDokter, string tanggal, string jam)
        {
            JadwalTglModel retVal = null;

            string sSql = @"
                SELECT      aa.fn_durasi, aa.fn_max, aa.fn_booked,
                            aa.fs_kd_layanan, 
                            ISNULL(bb.fs_nm_dokter, ' ') fs_nm_dokter, 
                            ISNULL(cc.fs_nm_layanan, ' ') fs_nm_layanan
                FROM        ta_jadwal_tgl aa
                LEFT JOIN   ta_dokter bb ON aa.fs_kd_dokter = bb.fs_kd_dokter
                LEFT JOIN   ta_layanan cc ON aa.fs_kd_layanan = cc.fs_kd_layanan
                WHERE       aa.fs_kd_dokter  = @KodeDokter
                        AND aa.fd_tgl_jadwal = @TglJadwal
                        AND aa.fs_jam = @Jam ";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                cmd.Parameters.AddWithValue("@KodeDokter", kodeDokter);
                cmd.Parameters.AddWithValue("@TglJadwal", tanggal);
                cmd.Parameters.AddWithValue("@Jam", jam);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    retVal = new JadwalTglModel();
                    retVal.KodeDokter = kodeDokter;
                    retVal.TglJadwal = tanggal;
                    retVal.Jam = jam;
                    retVal.NamaDokter = dr["fs_nm_dokter"].ToString();
                    retVal.KodeLayanan = dr["fs_kd_layanan"].ToString();
                    retVal.NamaLayanan = dr["fs_nm_layanan"].ToString();
                    retVal.Durasi = Convert.ToInt16(dr["fn_durasi"].ToString());
                    retVal.Max = Convert.ToInt16(dr["fn_max"].ToString());
                    retVal.Booked = Convert.ToInt16(dr["fn_booked"].ToString());
                }
            }
            return retVal;
        }
        
        public List<JadwalTglModel> ListData(string tanggal, LayananModel layanan)
        {
            List<JadwalTglModel> retVal = null;

            string sSql = @"
                SELECT      aa.fs_kd_dokter, aa.fs_jam, 
                            aa.fn_durasi, aa.fn_max, aa.fn_booked,
                            ISNULL(bb.fs_nm_dokter, ' ') fs_nm_dokter, 
                            ISNULL(cc.fs_nm_layanan, ' ') fs_nm_layanan
                FROM        ta_jadwal_tgl aa
                LEFT JOIN   ta_dokter bb ON aa.fs_kd_dokter = bb.fs_kd_dokter
                LEFT JOIN   ta_layanan cc ON aa.fs_kd_layanan = cc.fs_kd_layanan
                WHERE       aa.fs_kd_layanan = @KodeLayanan
                        AND aa.fd_tgl_jadwal = @TglJadwal ";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                cmd.Parameters.AddWithValue("@KodeLayanan", layanan.Kode);
                cmd.Parameters.AddWithValue("@TglJadwal", tanggal);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    retVal = new List<JadwalTglModel>();
                    while (dr.Read())
                    {
                        var item = new JadwalTglModel();
                        item.KodeDokter = dr["fs_kd_dokter"].ToString();
                        item.KodeLayanan = layanan.Kode;
                        item.TglJadwal = tanggal;
                        item.Jam = dr["fs_jam"].ToString();
                        item.NamaDokter = dr["fs_nm_dokter"].ToString();
                        item.NamaLayanan = dr["fs_nm_layanan"].ToString();
                        item.Durasi = Convert.ToInt16(dr["fn_durasi"].ToString());
                        item.Max = Convert.ToInt16(dr["fn_max"].ToString());
                        item.Booked = Convert.ToInt16(dr["fn_booked"].ToString());
                        retVal.Add(item);
                    }
                }
            }
            return retVal;
        }

        public List<JadwalTglModel> ListData(string tanggal, DokterModel dokter)
        {
            List<JadwalTglModel> retVal = null;

            string sSql = @"
                SELECT      aa.fs_kd_dokter, aa.fs_jam, aa.fs_kd_layanan, 
                            aa.fn_durasi, aa.fn_max, aa.fn_booked,
                            ISNULL(bb.fs_nm_dokter, ' ') fs_nm_dokter, 
                            ISNULL(cc.fs_nm_layanan, ' ') fs_nm_layanan
                FROM        ta_jadwal_tgl aa
                LEFT JOIN   ta_dokter bb ON aa.fs_kd_dokter = bb.fs_kd_dokter
                LEFT JOIN   ta_layanan cc ON aa.fs_kd_layanan = cc.fs_kd_layanan
                WHERE       aa.fs_kd_dokter = @KodeDokter
                        AND aa.fd_tgl_jadwal = @TglJadwal ";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                cmd.Parameters.AddWithValue("@KodeDokter", dokter.Kode);
                cmd.Parameters.AddWithValue("@TglJadwal", tanggal);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    retVal = new List<JadwalTglModel>();
                    while (dr.Read())
                    {
                        var item = new JadwalTglModel();
                        item.KodeDokter = dr["fs_kd_dokter"].ToString();
                        item.KodeLayanan = dr["fs_kd_layanan"].ToString();
                        item.TglJadwal = tanggal;
                        item.Jam = dr["fs_jam"].ToString();
                        item.NamaDokter = dr["fs_nm_dokter"].ToString();
                        item.NamaLayanan = dr["fs_nm_layanan"].ToString();
                        item.Durasi = Convert.ToInt16(dr["fn_durasi"].ToString());
                        item.Max = Convert.ToInt16(dr["fn_max"].ToString());
                        item.Booked = Convert.ToInt16(dr["fn_booked"].ToString());
                        retVal.Add(item);
                    }
                }
            }
            return retVal;
        }

        public List<JadwalTglModel> ListData(string tanggal)
        {
            List<JadwalTglModel> retVal = null;

            string sSql = @"
                SELECT      aa.fs_kd_dokter, aa.fs_jam, aa.fs_kd_layanan, 
                            aa.fn_durasi, aa.fn_max, aa.fn_booked,
                            ISNULL(bb.fs_nm_dokter, ' ') fs_nm_dokter, 
                            ISNULL(cc.fs_nm_layanan, ' ') fs_nm_layanan
                FROM        ta_jadwal_tgl aa
                LEFT JOIN   ta_dokter bb ON aa.fs_kd_dokter = bb.fs_kd_dokter
                LEFT JOIN   ta_layanan cc ON aa.fs_kd_layanan = cc.fs_kd_layanan
                WHERE       aa.fd_tgl_jadwal = @TglJadwal ";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                cmd.Parameters.AddWithValue("@TglJadwal", tanggal);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    retVal = new List<JadwalTglModel>();
                    while (dr.Read())
                    {
                        var item = new JadwalTglModel();
                        item.KodeDokter = dr["fs_kd_dokter"].ToString();
                        item.KodeLayanan = dr["fs_kd_layanan"].ToString();
                        item.TglJadwal = tanggal;
                        item.Jam = dr["fs_jam"].ToString();
                        item.NamaDokter = dr["fs_nm_dokter"].ToString();
                        item.NamaLayanan = dr["fs_nm_layanan"].ToString();
                        item.Durasi = Convert.ToInt16(dr["fn_durasi"].ToString());
                        item.Max = Convert.ToInt16(dr["fn_max"].ToString());
                        item.Booked = Convert.ToInt16(dr["fn_booked"].ToString());
                        retVal.Add(item);
                    }
                }
            }
            return retVal;
        }

        private List<JadwalTglModel> ListByTanggalLayanan_Backup(string tanggal, string kodeLayanan)
        {
            List<JadwalTglModel> retVal = new List<JadwalTglModel>();
            JadwalTglModel item = new JadwalTglModel();
            item.KodeDokter = "DK001";
            item.NamaDokter = "Agus Supartoto, Sp.M";
            item.FilePhoto = "AgusSupartoto.jpg";
            item.KodeLayanan = "LY001";
            item.NamaLayanan = "Poli Mata";
            item.TglJadwal = "10-09-2017";
            item.Jam = "08:00";
            item.Durasi = 60;
            item.Max = 4;
            item.Booked = 4;
            retVal.Add(item);

            item = new JadwalTglModel();
            item.KodeDokter = "DK001";
            item.NamaDokter = "Agus Supartoto, Sp.M";
            item.FilePhoto = "AgusSupartoto.jpg";
            item.KodeLayanan = "LY001";
            item.NamaLayanan = "Poli Mata";
            item.TglJadwal = "10-09-2017";
            item.Jam = "09:00";
            item.Durasi = 60;
            item.Max = 4;
            item.Booked = 2;
            retVal.Add(item);

            item = new JadwalTglModel();
            item.KodeDokter = "DK001";
            item.NamaDokter = "Agus Supartoto, Sp.M";
            item.FilePhoto = "AgusSupartoto.jpg";
            item.KodeLayanan = "LY001";
            item.NamaLayanan = "Poli Mata";
            item.TglJadwal = "10-09-2017";
            item.Jam = "10:00";
            item.Durasi = 30;
            item.Max = 4;
            item.Booked = 0;
            retVal.Add(item);

            item = new JadwalTglModel();
            item.KodeDokter = "DK001";
            item.NamaDokter = "Agus Supartoto, Sp.M";
            item.FilePhoto = "AgusSupartoto.jpg";
            item.KodeLayanan = "LY001";
            item.NamaLayanan = "Poli Mata";
            item.TglJadwal = "11-09-2017";
            item.Jam = "08:00";
            item.Durasi = 60;
            item.Max = 4;
            item.Booked = 1;
            retVal.Add(item);

            item = new JadwalTglModel();
            item.KodeDokter = "DK001";
            item.NamaDokter = "Agus Supartoto, Sp.M";
            item.FilePhoto = "AgusSupartoto.jpg";
            item.KodeLayanan = "LY001";
            item.NamaLayanan = "Poli Mata";
            item.TglJadwal = "11-09-2017";
            item.Jam = "09:00";
            item.Durasi = 60;
            item.Max = 4;
            item.Booked = 0;
            retVal.Add(item);

            item = new JadwalTglModel();
            item.KodeDokter = "DK001";
            item.NamaDokter = "Agus Supartoto, Sp.M";
            item.FilePhoto = "AgusSupartoto.jpg";
            item.KodeLayanan = "LY001";
            item.NamaLayanan = "Poli Mata";
            item.TglJadwal = "11-09-2017";
            item.Jam = "10:00";
            item.Durasi = 30;
            item.Max = 4;
            item.Booked = 0;
            retVal.Add(item);


            //-----------------------
            item = new JadwalTglModel();
            item.KodeDokter = "DK002";
            item.NamaDokter = "Priskila Dwi Erika, Sp.M";
            item.FilePhoto = "PriskilaDwiErika.jpg";
            item.KodeLayanan = "LY001";
            item.NamaLayanan = "Poli Mata";
            item.TglJadwal = "10-09-2017";
            item.Jam = "09:00";
            item.Durasi = 60;
            item.Max = 4;
            item.Booked = 1;
            retVal.Add(item);

            item = new JadwalTglModel();
            item.KodeDokter = "DK002";
            item.NamaDokter = "Priskila Dwi Erika, Sp.M";
            item.FilePhoto = "PriskilaDwiErika.jpg";
            item.KodeLayanan = "LY001";
            item.NamaLayanan = "Poli Mata";
            item.TglJadwal = "10-09-2017";
            item.Jam = "10:00";
            item.Durasi = 30;
            item.Max = 4;
            item.Booked = 0;
            retVal.Add(item);

            item = new JadwalTglModel();
            item.KodeDokter = "DK002";
            item.NamaDokter = "Priskila Dwi Erika, Sp.M";
            item.FilePhoto = "PriskilaDwiErika.jpg";
            item.KodeLayanan = "LY001";
            item.NamaLayanan = "Poli Mata";
            item.TglJadwal = "10-09-2017";
            item.Jam = "11:00";
            item.Durasi = 60;
            item.Max = 4;
            item.Booked = 0;
            retVal.Add(item);

            item = new JadwalTglModel();
            item.KodeDokter = "DK002";
            item.NamaDokter = "Priskila Dwi Erika, Sp.M";
            item.FilePhoto = "PriskilaDwiErika.jpg";
            item.KodeLayanan = "LY001";
            item.NamaLayanan = "Poli Mata";
            item.TglJadwal = "11-09-2017";
            item.Jam = "09:00";
            item.Durasi = 60;
            item.Max = 4;
            item.Booked = 0;
            retVal.Add(item);

            item = new JadwalTglModel();
            item.KodeDokter = "DK002";
            item.NamaDokter = "Priskila Dwi Erika, Sp.M";
            item.FilePhoto = "PriskilaDwiErika.jpg";
            item.KodeLayanan = "LY001";
            item.NamaLayanan = "Poli Mata";
            item.TglJadwal = "11-09-2017";
            item.Jam = "10:00";
            item.Durasi = 30;
            item.Max = 4;
            item.Booked = 0;
            retVal.Add(item);

            item = new JadwalTglModel();
            item.KodeDokter = "DK002";
            item.NamaDokter = "Priskila Dwi Erika, Sp.M";
            item.FilePhoto = "PriskilaDwiErika.jpg";
            item.KodeLayanan = "LY001";
            item.NamaLayanan = "Poli Mata";
            item.TglJadwal = "11-09-2017";
            item.Jam = "11:00";
            item.Durasi = 60;
            item.Max = 4;
            item.Booked = 0;
            retVal.Add(item);

            return retVal;
        }
    }
}