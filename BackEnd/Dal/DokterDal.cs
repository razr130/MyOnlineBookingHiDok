using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BackEnd.Dal
{
    public interface IDokterDal
    {
        void Insert(DokterModel dokter);
        void Update(DokterModel dokter);
        void Delete(string id);
        DokterModel GetById(string id);
        List<DokterModel> ListData();
        List<DokterModel> ListData(string kodeLayanan);
        string GetConnectionString();
        void Clear();
    }

    public class DokterDal : IDokterDal
    {
        string _connString;

        public DokterDal()
        {
            _connString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }

        public string GetConnectionString()
        {
            return _connString;
        }

        public void Insert(DokterModel dokter)
        {
            string sSql = @"
                INSERT INTO ta_dokter 
                    (   
                        fs_kd_dokter, fs_nm_dokter, fs_kd_layanan,
                        fs_photo_filename 
                    )
                VALUES  
                    (   
                        @KodeDokter, @NamaDokter, @KodeLayanan, 
                        @PhotoFileName 
                    )";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                cmd.Parameters.AddWithValue("@KodeDokter", dokter.Kode);
                cmd.Parameters.AddWithValue("@NamaDokter", dokter.Nama);
                cmd.Parameters.AddWithValue("@KodeLayanan", dokter.KodeLayanan);
                cmd.Parameters.AddWithValue("@PhotoFileName", dokter.PhotoFileName);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(DokterModel dokter)
        {
            string sSql = @"
                UPDATE  ta_dokter 
                SET     fs_nm_dokter = @NamaDokter,
                        fs_kd_layanan = @KodeLayanan,
                        fs_photo_filename = @PhotoFileName
                WHERE   fs_kd_dokter = @KodeDokter";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                cmd.Parameters.AddWithValue("@KodeDokter", dokter.Kode);
                cmd.Parameters.AddWithValue("@NamaDokter", dokter.Nama);
                cmd.Parameters.AddWithValue("@KodeLayanan", dokter.KodeLayanan);
                cmd.Parameters.AddWithValue("@PhotoFileName", dokter.PhotoFileName);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(string id)
        {
            string sSql = @"
                DELETE  ta_dokter 
                WHERE   fs_kd_dokter = @KodeDokter";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                cmd.Parameters.AddWithValue("@KodeDokter", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Clear()
        {
            string sSql = @"
                DELETE  ta_dokter ";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public DokterModel GetById(string id)
        {
            DokterModel retVal = null;
            string sSql = @"
                SELECT      aa.fs_kd_dokter, aa.fs_nm_dokter, aa.fs_kd_layanan,
                            aa.fs_photo_filename, 
                            ISNULL(bb.fs_nm_layanan, ' ') fs_nm_layanan
                FROM        ta_dokter aa
                LEFT JOIN   ta_layanan bb ON aa.fs_kd_layanan = bb.fs_kd_layanan
                WHERE       fs_kd_dokter  = @Kode ";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                cmd.Parameters.AddWithValue("@Kode", id);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    retVal = new DokterModel();
                    retVal.Kode = dr["fs_kd_dokter"].ToString();
                    retVal.Nama = dr["fs_nm_dokter"].ToString();
                    retVal.KodeLayanan = dr["fs_kd_layanan"].ToString();
                    retVal.NamaLayanan = dr["fs_nm_layanan"].ToString();
                    retVal.PhotoFileName = dr["fs_photo_filename"].ToString();
                }
            }
            return retVal;
        }

        public List<DokterModel> ListData()
        {
            List<DokterModel> retVal = null;
            string sSql = @"
                SELECT      aa.fs_kd_dokter, aa.fs_nm_dokter, aa.fs_kd_layanan,
                            aa.fs_photo_filename, 
                            ISNULL(bb.fs_nm_layanan, ' ') fs_nm_layanan
                FROM        ta_dokter aa
                LEFT JOIN   ta_layanan bb ON aa.fs_kd_layanan = bb.fs_kd_layanan
                ORDER BY    fs_nm_dokter ";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    retVal = new List<DokterModel>();
                    while (dr.Read())
                    {
                        DokterModel item = new DokterModel();
                        item.Kode = dr["fs_kd_dokter"].ToString();
                        item.Nama = dr["fs_nm_dokter"].ToString();
                        item.KodeLayanan = dr["fs_kd_layanan"].ToString();
                        item.NamaLayanan = dr["fs_nm_layanan"].ToString();
                        item.PhotoFileName = dr["fs_photo_filename"].ToString();
                        retVal.Add(item);
                    }
                }
            }
            return retVal;
        }

        public List<DokterModel> ListData(string kodeLayanan)
        {
            List<DokterModel> retVal = null;
            string sSql = @"
                SELECT      aa.fs_kd_dokter, aa.fs_nm_dokter, aa.fs_kd_layanan,
                            aa.fs_photo_filename, 
                            ISNULL(bb.fs_nm_layanan, ' ') fs_nm_layanan
                FROM        ta_dokter aa
                LEFT JOIN   ta_layanan bb ON aa.fs_kd_layanan = bb.fs_kd_layanan
                WHERE       aa.fs_kd_layanan = @KodeLayanan
                ORDER BY    fs_nm_dokter ";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                cmd.Parameters.AddWithValue("@KodeLayanan", kodeLayanan);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    retVal = new List<DokterModel>();
                    while (dr.Read())
                    {
                        DokterModel item = new DokterModel();
                        item.Kode = dr["fs_kd_dokter"].ToString();
                        item.Nama = dr["fs_nm_dokter"].ToString();
                        item.KodeLayanan = dr["fs_kd_layanan"].ToString();
                        item.NamaLayanan = dr["fs_nm_layanan"].ToString();
                        item.PhotoFileName = dr["fs_photo_filename"].ToString();
                        retVal.Add(item);
                    }
                }
            }
            return retVal;
        }
    }
}