using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Transactions;
using System.Data.SqlClient;
using MyOnlineBooking.Helpers;

namespace BackEnd.Dal
{
    public interface IJadwalHariDal
    {
        void Insert(JadwalHariModel jadwal);
        void Update(JadwalHariModel jadwal);
        void Delete(string id);
        JadwalHariModel GetById(string id);
        string GetId(string kodeDokter, int hari, string jamMulai);
        List<JadwalHariModel> ListData(DokterModel dokter);
        List<JadwalHariModel> ListData(LayananModel layanan);
        List<JadwalHariModel> ListData(int hari);
        List<JadwalHariModel> ListData();
        string GetConnectionString();
        void Clear();

        List<JadwalHariPerJamModel> GetDetil(string kodeDokter, int hari, string jamMulai);
    }

    public class JadwalHariDal : IJadwalHariDal
    {
        string _connString;

        public JadwalHariDal()
        {
            _connString = DataAccessHelper.GetConnectionString();
        }

        public string GetConnectionString() => _connString;

        public void Insert(JadwalHariModel jadwal)
        {
            string sSql = @"
                INSERT INTO ta_jadwal_hari 
                    (
                        fs_kd_jadwal, fs_kd_dokter, fs_kd_layanan,
                        fn_hari, fs_jam_mulai, fs_jam_selesai 
                    )
                VALUES  
                    (
                        @KodeJadwal, @KodeDokter, @KodeLayanan,
                        @Hari, @JamMulai, @JamSelesai
                    ) ";
            TransactionOptions tranOpt = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            };
            using (TransactionScope tran = new TransactionScope(TransactionScopeOption.Required, tranOpt))
            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                cmd.Parameters.AddWithValue("@KodeJadwal", jadwal.Kode);
                cmd.Parameters.AddWithValue("@KodeDokter", jadwal.KodeDokter);
                cmd.Parameters.AddWithValue("@KodeLayanan", jadwal.KodeLayanan);
                cmd.Parameters.AddWithValue("@Hari", jadwal.Hari);
                cmd.Parameters.AddWithValue("@JamMulai", jadwal.JamMulai);
                cmd.Parameters.AddWithValue("@JamSelesai", jadwal.JamSelesai);
                conn.Open();
                cmd.ExecuteNonQuery();

                //  hapus detil-nya
                sSql = @"
                    DELETE      ta_jadwal_hari_jam 
                    WHERE       fs_kd_jadwal = @Kode";
                cmd.CommandText = sSql;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Kode", jadwal.Kode);
                cmd.ExecuteNonQuery();

                //  insert detil
                foreach (JadwalHariPerJamModel item in jadwal.JadwalPerJams)
                {
                    sSql = @"
                        INSERT INTO ta_jadwal_hari_jam 
                                (fs_kd_jadwal, fs_jam, fn_durasi, fn_max, fn_booked)
                        VALUES  (@Kode, @Jam, @Durasi, @Max, @Booked) ";
                    cmd.CommandText = sSql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Kode", jadwal.Kode);
                    cmd.Parameters.AddWithValue("@Jam", item.Jam);
                    cmd.Parameters.AddWithValue("@Durasi", item.Durasi);
                    cmd.Parameters.AddWithValue("@Max", item.Max);
                    cmd.Parameters.AddWithValue("@Booked", item.Booked);
                    cmd.ExecuteNonQuery();
                }
                tran.Complete();
            }
        }

        public void Update(JadwalHariModel jadwal)
        {
            {
                string sSql = @"
                    UPDATE      ta_jadwal_hari 
                    SET         fs_kd_dokter = @KodeDokter,
                                fs_kd_layanan = @KodeLayanan,
                                fn_hari = @Hari,
                                fs_jam_mulai = @JamMulai,
                                fs_jam_selesai = @JamSelesai
                    WHERE       fs_kd_jadwal = @KodeJadwal ";

                TransactionOptions tranOpt = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted
                };
                using (TransactionScope tran = new TransactionScope(TransactionScopeOption.Required, tranOpt))
                using (SqlConnection conn = new SqlConnection(_connString))
                using (SqlCommand cmd = new SqlCommand(sSql, conn))
                {
                    cmd.Parameters.AddWithValue("@KodeJadwal", jadwal.Kode);
                    cmd.Parameters.AddWithValue("@KodeDokter", jadwal.KodeDokter);
                    cmd.Parameters.AddWithValue("@KodeLayanan", jadwal.KodeLayanan);
                    cmd.Parameters.AddWithValue("@Hari", jadwal.Hari);
                    cmd.Parameters.AddWithValue("@JamMulai", jadwal.JamMulai);
                    cmd.Parameters.AddWithValue("@JamSelesai", jadwal.JamSelesai);
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    //  hapus detil-nya
                    sSql = @"
                        DELETE      ta_jadwal_hari_jam 
                        WHERE       fs_kd_jadwal = @Kode";
                    cmd.CommandText = sSql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Kode", jadwal.Kode);
                    cmd.ExecuteNonQuery();

                    //  insert detil
                    foreach (JadwalHariPerJamModel item in jadwal.JadwalPerJams)
                    {
                        sSql = @"
                        INSERT INTO ta_jadwal_hari_jam 
                                    (fs_kd_jadwal, fs_jam, fn_durasi, fn_max, fn_booked)
                        VALUES      (@Kode, @Jam, @Durasi, @Max, @Booked) ";
                        cmd.CommandText = sSql;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Kode", jadwal.Kode);
                        cmd.Parameters.AddWithValue("@Jam", item.Jam);
                        cmd.Parameters.AddWithValue("@Durasi", item.Durasi);
                        cmd.Parameters.AddWithValue("@Max", item.Max);
                        cmd.Parameters.AddWithValue("@Booked", item.Booked);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Delete(string id)
        {
            string sSql = @"
                DELETE      ta_jadwal_hari_jam 
                WHERE       fs_kd_jadwal = @Kode ";

            TransactionOptions tranOpt = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadUncommitted
            }; using (TransactionScope tran = new TransactionScope(TransactionScopeOption.Required, tranOpt))
            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                cmd.Parameters.AddWithValue("@Kode", id);
                conn.Open();
                cmd.ExecuteNonQuery();

                sSql = @"
                    DELETE      ta_jadwal_hari 
                    WHERE       fs_kd_jadwal = @Kode ";
                cmd.CommandText = sSql;
                cmd.ExecuteNonQuery();
            }
        }

        public void Clear()
        {
            string sSql = @"
                DELETE      ta_jadwal_hari_jam ";

            TransactionOptions tranOpt = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadUncommitted
            }; using (TransactionScope tran = new TransactionScope(TransactionScopeOption.Required, tranOpt))
            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();

                sSql = @"
                    DELETE      ta_jadwal_hari ";
                cmd.CommandText = sSql;
                cmd.ExecuteNonQuery();
            }
        }

        public JadwalHariModel GetById(string id)
        {
            JadwalHariModel retVal = null;

            string sSql = @" 
                SELECT      aa.fs_kd_dokter, aa.fs_kd_layanan, aa.fn_hari, 
                            aa.fs_jam_mulai, aa.fs_jam_selesai,
                            ISNULL(bb.fs_nm_dokter, ' ') fs_nm_dokter, 
                            ISNULL(cc.fs_nm_layanan, ' ') fs_nm_layanan
                FROM        ta_jadwal_hari aa
                LEFT JOIN   ta_dokter bb ON aa.fs_kd_dokter = bb.fs_kd_dokter 
                LEFT JOIN   ta_layanan cc ON aa.fs_kd_layanan = cc.fs_kd_layanan
                WHERE       fs_kd_jadwal = @Kode ";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@Kode", id);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    retVal = new JadwalHariModel();
                    retVal.Kode = id;
                    retVal.KodeDokter = dr["fs_kd_dokter"].ToString();
                    retVal.NamaDokter = dr["fs_nm_dokter"].ToString();
                    retVal.KodeLayanan = dr["fs_kd_layanan"].ToString();
                    retVal.NamaLayanan = dr["fs_nm_layanan"].ToString();
                    retVal.Hari = Convert.ToInt16(dr["fn_hari"].ToString());
                    retVal.JamMulai = dr["fs_jam_mulai"].ToString();
                    retVal.JamSelesai = dr["fs_jam_selesai"].ToString();
                }
                dr.Close();

                //  query detil
                sSql = @"
                    SELECT  fs_jam, fn_durasi, fn_max, fn_booked
                    FROM    ta_jadwal_hari_jam
                    WHERE   fs_kd_jadwal = @Kode ";
                cmd.CommandText = sSql;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Kode", id);
                SqlDataReader dr2 = cmd.ExecuteReader();
                if (dr2.HasRows)
                {
                    retVal.JadwalPerJams = new List<JadwalHariPerJamModel>();
                    while (dr2.Read())
                    {
                        JadwalHariPerJamModel item = new JadwalHariPerJamModel();
                        item.Jam = dr2["fs_jam"].ToString();
                        item.Durasi = Convert.ToInt16(dr2["fn_durasi"].ToString());
                        item.Max = Convert.ToInt16(dr2["fn_max"].ToString());
                        item.Booked = Convert.ToInt16(dr2["fn_booked"].ToString());
                        retVal.JadwalPerJams.Add(item);
                    }
                }
            }

            return retVal;
        }

        public string GetId(string kodeDokter, int hari, string jamMulai)
        {
            string retVal = null;

            string sSql = @"
                SELECT      fs_kd_jadwal 
                FROM        ta_jadwal_hari 
                WHERE       fs_kd_dokter = @KodeDokter
                        AND fn_hari = @Hari
                        AND fs_jam_mulai = @JamMulai";
            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                cmd.Parameters.AddWithValue("@KodeDokter", kodeDokter);
                cmd.Parameters.AddWithValue("@JamMulai", jamMulai);
                cmd.Parameters.AddWithValue("@Hari", hari);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    retVal = dr["fs_kd_jadwal"].ToString();
                }
            }
            return retVal;
        }

        public List<JadwalHariModel> ListData(LayananModel layanan)
        {
            List<JadwalHariModel> retVal = null;

            string sSql = @"
                SELECT      aa.fs_kd_dokter, aa.fs_kd_layanan, aa.fn_hari, 
                            aa.fs_jam_mulai, aa.fs_jam_selesai,
                            ISNULL(bb.fs_nm_dokter, ' ') fs_nm_dokter,
                            ISNULL(cc.fs_nm_layanan, ' ') fs_nm_layanan
                FROM        ta_jadwal_hari aa
                LEFT JOIN   ta_dokter bb ON aa.fs_kd_dokter = bb.fs_kd_dokter 
                LEFT JOIN   ta_layanan cc ON aa.fs_kd_layanan = cc.fs_kd_layanan
                WHERE       aa.fs_kd_layanan = @KodeLayanan
                ORDER BY    cc.fs_nm_layanan, bb.fs_nm_dokter ";


            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                cmd.Parameters.AddWithValue("@KodeLayanan", layanan.Kode);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    retVal = new List<JadwalHariModel>();
                    while (dr.Read())
                    {
                        JadwalHariModel item = new JadwalHariModel();
                        item.KodeDokter = dr["fs_kd_dokter"].ToString();
                        item.NamaDokter = dr["fs_nm_dokter"].ToString();
                        item.KodeLayanan = dr["fs_kd_layanan"].ToString();
                        item.NamaLayanan = dr["fs_nm_layanan"].ToString();
                        item.Hari = Convert.ToInt16(dr["fn_hari"].ToString());
                        item.JamMulai = dr["fs_jam_mulai"].ToString();
                        item.JamSelesai = dr["fs_jam_selesai"].ToString();
                        retVal.Add(item);
                    }
                }
            }
            return retVal;
        }

        public List<JadwalHariModel> ListData(DokterModel dokter)
        {
            List<JadwalHariModel> retVal = null;

            string sSql = @"
                SELECT      aa.fs_kd_dokter, aa.fs_kd_layanan, aa.fn_hari, 
                            aa.fs_jam_mulai, aa.fs_jam_selesai,
                            ISNULL(bb.fs_nm_dokter, ' ') fs_nm_dokter,
                            ISNULL(cc.fs_nm_layanan, ' ') fs_nm_layanan
                FROM        ta_jadwal_hari aa
                LEFT JOIN   ta_dokter bb ON aa.fs_kd_dokter = bb.fs_kd_dokter 
                LEFT JOIN   ta_layanan cc ON aa.fs_kd_layanan = cc.fs_kd_layanan
                WHERE       aa.fs_kd_dokter = @KodeDokter
                ORDER BY    cc.fs_nm_layanan, bb.fs_nm_dokter ";


            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                cmd.Parameters.AddWithValue("@KodeDokter", dokter.Kode);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    retVal = new List<JadwalHariModel>();
                    while (dr.Read())
                    {
                        JadwalHariModel item = new JadwalHariModel();
                        item.KodeDokter = dr["fs_kd_dokter"].ToString();
                        item.NamaDokter = dr["fs_nm_dokter"].ToString();
                        item.KodeLayanan = dr["fs_kd_layanan"].ToString();
                        item.NamaLayanan = dr["fs_nm_layanan"].ToString();
                        item.Hari = Convert.ToInt16(dr["fn_hari"].ToString());
                        item.JamMulai = dr["fs_jam_mulai"].ToString();
                        item.JamSelesai = dr["fs_jam_selesai"].ToString();
                        retVal.Add(item);
                    }
                }
            }
            return retVal;
        }

        public List<JadwalHariModel> ListData(int hari)
        {
            List<JadwalHariModel> retVal = null;

            string sSql = @"
                SELECT      aa.fs_kd_dokter, aa.fs_kd_layanan, aa.fn_hari, 
                            aa.fs_jam_mulai, aa.fs_jam_selesai,
                            ISNULL(bb.fs_nm_dokter, ' ') fs_nm_dokter,
                            ISNULL(cc.fs_nm_layanan, ' ') fs_nm_layanan
                FROM        ta_jadwal_hari aa
                LEFT JOIN   ta_dokter bb ON aa.fs_kd_dokter = bb.fs_kd_dokter 
                LEFT JOIN   ta_layanan cc ON aa.fs_kd_layanan = cc.fs_kd_layanan
                WHERE       aa.fn_hari = @Hari
                ORDER BY    cc.fs_nm_layanan, bb.fs_nm_dokter ";


            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                cmd.Parameters.AddWithValue("@KodeDokter", hari);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    retVal = new List<JadwalHariModel>();
                    while (dr.Read())
                    {
                        JadwalHariModel item = new JadwalHariModel();
                        item.KodeDokter = dr["fs_kd_dokter"].ToString();
                        item.NamaDokter = dr["fs_nm_dokter"].ToString();
                        item.KodeLayanan = dr["fs_kd_layanan"].ToString();
                        item.NamaLayanan = dr["fs_nm_layanan"].ToString();
                        item.Hari = Convert.ToInt16(dr["fn_hari"].ToString());
                        item.JamMulai = dr["fs_jam_mulai"].ToString();
                        item.JamSelesai = dr["fs_jam_selesai"].ToString();
                        retVal.Add(item);
                    }
                }
            }
            return retVal;
        }


        public List<JadwalHariModel> ListData()
        {
            List<JadwalHariModel> retVal = null;

            string sSql = @"
                SELECT      aa.fs_kd_dokter, aa.fs_kd_layanan, aa.fn_hari, 
                            aa.fs_jam_mulai, aa.fs_jam_selesai,
                            ISNULL(bb.fs_nm_dokter, ' ') fs_nm_dokter,
                            ISNULL(cc.fs_nm_layanan, ' ') fs_nm_layanan
                FROM        ta_jadwal_hari aa
                LEFT JOIN   ta_dokter bb ON aa.fs_kd_dokter = bb.fs_kd_dokter 
                LEFT JOIN   ta_layanan cc ON aa.fs_kd_layanan = cc.fs_kd_layanan
                ORDER BY    cc.fs_nm_layanan, bb.fs_nm_dokter ";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    retVal = new List<JadwalHariModel>();
                    while (dr.Read())
                    {
                        JadwalHariModel item = new JadwalHariModel();
                        item.KodeDokter = dr["fs_kd_dokter"].ToString();
                        item.NamaDokter = dr["fs_nm_dokter"].ToString();
                        item.KodeLayanan = dr["fs_kd_layanan"].ToString();
                        item.NamaLayanan = dr["fs_nm_layanan"].ToString();
                        item.Hari = Convert.ToInt16(dr["fn_hari"].ToString());
                        item.JamMulai = dr["fs_jam_mulai"].ToString();
                        item.JamSelesai = dr["fs_jam_selesai"].ToString();
                        retVal.Add(item);
                    }
                }
            }
            return retVal;
        }

        public List<JadwalHariPerJamModel> GetDetil(string kodeDokter, int hari, string jamMulai)
        {
            List<JadwalHariPerJamModel> retVal = null;

            string kodeJadwal = GetId(kodeDokter, hari, jamMulai);

            string sSql = @"
                SELECT      aa.fs_jam, aa.fn_durasi, aa.fn_max, 
                            aa.fn_booked
                FROM        ta_jadwal_hari_per_jam aa
                WHERE       fs_kd_jadwal = @KodeJadwal
                ORDER BY    cc.fs_nm_layanan, bb.fs_nm_dokter ";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@KodeJadwal", kodeJadwal);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    retVal = new List<JadwalHariPerJamModel>();
                    while (dr.Read())
                    {
                        var item = new JadwalHariPerJamModel();
                        item.Jam = dr["fs_jam"].ToString();
                        item.Durasi = Convert.ToInt16(dr["fn_durasi"].ToString());
                        item.Max = Convert.ToInt16(dr["fn_max"].ToString());
                        item.Booked= Convert.ToInt16(dr["fn_booked"].ToString());
                        retVal.Add(item);
                    }
                }
            }
            return retVal;
        }
    }
}