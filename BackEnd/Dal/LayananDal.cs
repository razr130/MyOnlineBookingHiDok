using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BackEnd.Models;
using System.Configuration;
using System.Data.SqlClient;
using MyOnlineBooking.Helpers;
namespace BackEnd.Dal
{
    public interface ILayananDal
    {
        void Insert(LayananModel layanan);
        void Update(LayananModel layanan);
        void Delete(string id);
        LayananModel GetById(string id);
        List<LayananModel> ListData(LayananListDataType listType);
        void Clear();
        string GetConnectionString();

    }

    public class LayananDal : ILayananDal
    {
        string _connString;

        public LayananDal()
        {
            _connString = DataAccessHelper.GetConnectionString();
        }
        public string GetConnectionString()
        {
            return _connString; 
        }

        public void Insert(LayananModel layanan)
        {
            string sSql = @"
                INSERT INTO     ta_layanan 
                                (fs_kd_layanan, fs_nm_layanan, fb_popular)
                VALUES          (@KodeLayanan, @NamaLayanan, @IsPopular)";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                cmd.Parameters.AddWithValue("@KodeLayanan", layanan.Kode);
                cmd.Parameters.AddWithValue("@NamaLayanan", layanan.Nama);
                cmd.Parameters.AddWithValue("@IsPopular", layanan.IsPopular ? 1 : 0);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(LayananModel layanan)
        {
            string sSql = @"
                UPDATE      ta_layanan 
                SET         fs_nm_layanan= @NamaLayanan,
                            fb_popular = @IsPopular
                WHERE       fs_kd_layanan = @KodeLayanan";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                cmd.Parameters.AddWithValue("@KodeLayanan", layanan.Kode);
                cmd.Parameters.AddWithValue("@NamaLayanan", layanan.Nama);
                cmd.Parameters.AddWithValue("@IsPopular", layanan.IsPopular ? 1 : 0);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(string id)
        {
            string sSql = @"
                DELETE      ta_layanan 
                WHERE       fs_kd_layanan = @KodeLayanan";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                cmd.Parameters.AddWithValue("@KodeLayanan", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public LayananModel GetById(string id)
        {
            LayananModel retVal = null;
            string sSql = @"
                SELECT      fs_kd_layanan, fs_nm_layanan, fb_popular
                FROM        ta_layanan 
                WHERE       fs_kd_layanan  = @Kode ";
            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                cmd.Parameters.AddWithValue("@Kode", id);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    retVal = new LayananModel();
                    retVal.Kode = dr["fs_kd_layanan"].ToString();
                    retVal.Nama = dr["fs_nm_layanan"].ToString();
                    retVal.IsPopular = dr["fb_popular"].ToString() == "1" ? true : false;
                }
            }
            return retVal;
        }

        public List<LayananModel> ListData(LayananListDataType listType)
        {
            List<LayananModel> retVal = null;
            string sSql = @"
                    SELECT      fs_kd_layanan, fs_nm_layanan, fb_popular
                    FROM        ta_layanan";

            switch (listType)
            {
                case LayananListDataType.All:
                    break;
                case LayananListDataType.Popular:
                    sSql += @" 
                        WHERE   fb_popular = 1";
                    break;
                case LayananListDataType.NotPopular:
                    sSql += @" 
                        WHERE   fb_popular = 0";
                    break;
                default:
                    break;
            }

            sSql += @"
                ORDER BY    fs_nm_layanan ";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    retVal = new List<LayananModel>();
                    while (dr.Read())
                    {
                        LayananModel item = new LayananModel();
                        item.Kode = dr["fs_kd_layanan"].ToString();
                        item.Nama = dr["fs_nm_layanan"].ToString();
                        item.IsPopular = dr.GetBoolean(dr.GetOrdinal("fb_popular")); 
                        retVal.Add(item);
                    }
                }
            }
            return retVal;
        }

        public void Clear()
        {
            string sSql = @"
                DELETE      ta_layanan  ";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}