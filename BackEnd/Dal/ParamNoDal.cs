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
    public interface IParamNoDal
    {
        void Insert(ParamNoModel paramNo);
        void Update(ParamNoModel paramnNo);
        void Delete(string id);
        ParamNoModel GetById(string id);
        string GetConnectionString();
    }

    public class ParamNoDal : IParamNoDal
    {
        string _connString;

        public ParamNoDal()
        {
            _connString = DataAccessHelper.GetConnectionString();
        }

        public string GetConnectionString()
        {
            return _connString;
        }

        public void Insert(ParamNoModel paramNo)
        {
            string sSql = @"
                INSERT INTO tz_param_no
                            (fs_prefix, fn_value)
                VALUES      (@Prefix, @Value) ";

            using(SqlConnection conn = new SqlConnection(_connString))
            using(SqlCommand cmd = new SqlCommand(sSql, conn)) 
            {
                cmd.Parameters.AddWithValue("@Prefix", paramNo.Prefix);
                cmd.Parameters.AddWithValue("@Value", paramNo.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(ParamNoModel paramNo)
        {
            string sSql = @"
                UPDATE      tz_param_no
                SET         fn_value = @Value
                WHERE       fs_prefix = @Prefix ";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                cmd.Parameters.AddWithValue("@Prefix", paramNo.Prefix);
                cmd.Parameters.AddWithValue("@Value", paramNo.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(string id)
        {
            string sSql = @"
                DELETE      tz_param_no
                WHERE       fs_prefix = @Kode ";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                cmd.Parameters.AddWithValue("@Kode", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public ParamNoModel GetById(string id)
        {
            ParamNoModel retVal = null;

            string sSql = @"
                SELECT      fs_prefix, fn_value
                FROM        tz_param_no
                WHERE       fs_prefix = @Prefix ";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                cmd.Parameters.AddWithValue("@Prefix", id);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    retVal = new ParamNoModel();
                    retVal.Prefix = dr["fs_prefix"].ToString();
                    retVal.Value = Convert.ToInt64(dr["fn_value"].ToString());
                }
            }
            return retVal;
        }

    }
}