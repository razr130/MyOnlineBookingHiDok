using BackEnd.Helpers;
using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BackEnd.Dal
{
    public interface IBookingDal
    {
        void Insert(BookingModel dokter);
        void Update(BookingModel dokter);
        void Delete(string id);
        BookingModel GetById(string id);
        List<BookingModel> ListData();
        List<BookingModel> ListData(string tglAwal, string tglAkhir);
        void Clear();
    }

    public class BookingDal : IBookingDal
    {
        string _connString;

        public BookingDal()
        {
            _connString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }

        public string GenNewID()
        {
            string retVal = null;
            long numb;

            string sSql = @"
                SELECT      MAX(fs_kd_trs) fs_kd_trs
                FROM        ta_trs_booking ";
            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    retVal = dr["fs_kd_trs"].ToString();
                    if (retVal.Trim() != "")
                    {
                        retVal = retVal.Substring(retVal.Length - 3);
                        numb = Convert.ToInt64(retVal);
                    }
                    else numb = 1;
                }
                else numb = 1;
            }
            numb++;
            retVal = numb.ToString().Trim();
            retVal = retVal.PadLeft(8, '0');
            retVal = "BK" + retVal;

            return retVal;
        }

        public void Insert(BookingModel booking)
        {
            booking.KodeTrs = GenNewID();
            string sSql = @"
                INSERT INTO ta_trs_booking 
                    (   
                        fs_kd_trs, fd_tgl_trs, fs_jam_trs,
                        fs_kd_dokter, fs_kd_layanan, fd_tgl_jadwal, 
                        fs_jam_jadwal, fs_mr, fs_nm_pasien, 
                        fd_tgl_lahir, fs_no_telp, fs_email
                    )
                VALUES  
                    (   
                        @KodeTrs, @TglTrs, @JamTrs,
                        @KodeDokter, @KodeLayanan, @TglJadwal,
                        @JamJadwal, @KodeMR, @NamaPasien,
                        @TglLahir, @NoTelp, @Email
                    )";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                cmd.Parameters.AddWithValue("@KodeTrs", booking.KodeTrs);
                cmd.Parameters.AddWithValue("@TglTrs", booking.TglTrs.ToTglYMD());
                cmd.Parameters.AddWithValue("@JamTrs", booking.JamTrs);
                cmd.Parameters.AddWithValue("@KodeDokter", booking.KodeDokter);
                cmd.Parameters.AddWithValue("@KodeLayanan", booking.KodeLayanan);
                cmd.Parameters.AddWithValue("@TglJadwal", booking.TglJadwal);
                cmd.Parameters.AddWithValue("@JamJadwal", booking.JamJadwal);
                cmd.Parameters.AddWithValue("@KodeMR", booking.KodeMR);
                cmd.Parameters.AddWithValue("@NamaPasien", booking.NamaPasien);
                cmd.Parameters.AddWithValue("@TglLahir", booking.TglLahir.ToTglYMD());
                cmd.Parameters.AddWithValue("@NoTelp", booking.NoTelp);
                cmd.Parameters.AddWithValue("@Email", booking.Email);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(BookingModel dokter)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public BookingModel GetById(string id)
        {
            throw new NotImplementedException();
        }

        public List<BookingModel> ListData()
        {
            List<BookingModel> retVal = null;

            string sSql = @"
                SELECT      aa.fs_kd_trs, aa.fd_tgl_trs, aa.fs_jam_trs,
                            aa.fs_kd_dokter, aa.fs_kd_layanan, aa.fd_tgl_jadwal, 
                            aa.fs_jam_jadwal, aa.fs_mr, aa.fs_nm_pasien, 
                            aa.fd_tgl_lahir, aa.fs_no_telp, aa.fs_email,
                            ISNULL(bb.fs_nm_layanan, ' ') fs_nm_layanan, 
                            ISNULL(cc.fs_nm_dokter, ' ') fs_nm_dokter
                FROM        ta_trs_booking aa
                LEFT JOIN   ta_layanan bb ON aa.fs_kd_layanan = bb.fs_kd_layanan
                LEFT JOIN   ta_dokter cc ON aa.fs_kd_dokter = cc.fs_kd_dokter
                ORDER BY    fs_kd_trs ";

            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    retVal = new List<BookingModel>();
                    while (dr.Read())
                    {
                        var item = new BookingModel
                        {
                            KodeTrs = dr["fs_kd_trs"].ToString(),
                            TglTrs = dr["fd_tgl_trs"].ToString().ToTglDMY(),
                            JamTrs = dr["fs_jam_trs"].ToString(),
                            KodeDokter = dr["fs_kd_dokter"].ToString(),
                            NamaDokter = dr["fs_nm_dokter"].ToString(),
                            KodeLayanan = dr["fs_kd_layanan"].ToString(),
                            NamaLayanan = dr["fs_nm_layanan"].ToString(),
                            TglJadwal = dr["fd_tgl_jadwal"].ToString().ToTglDMY(),
                            JamJadwal = dr["fs_jam_jadwal"].ToString(),
                            KodeMR = dr["fs_mr"].ToString(),
                            NamaPasien = dr["fs_nm_pasien"].ToString(),
                            TglLahir = dr["fd_tgl_lahir"].ToString().ToTglDMY(),
                            Email = dr["fs_email"].ToString(),
                            NoTelp = dr["fs_no_telp"].ToString()
                        };
                        retVal.Add(item);
                    }
                }
                return retVal;
            }
        }

        public List<BookingModel> ListData(string tglAwal, string tglAkhir)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

    }
}