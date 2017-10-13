using Microsoft.VisualStudio.TestTools.UnitTesting;
using BackEnd.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BackEnd.Models;

namespace BackEnd.Dal.Tests
{
    [TestClass()]
    public class JadwalTglDalTests
    {
        JadwalTglDal _jadwalTglDal;

        public JadwalTglDalTests()
        {
            _jadwalTglDal = new JadwalTglDal();
        }

        [TestInitialize]
        public void TestInit()
        {
            string sSql = @" DELETE ta_jadwal_tgl";
            using (SqlConnection conn = new SqlConnection(_jadwalTglDal.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            string sSql = @" DELETE ta_jadwal_tgl";
            using (SqlConnection conn = new SqlConnection(_jadwalTglDal.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        [TestMethod()]
        public void JadwalTglDal_Insert_Test()
        {
            //  arrange
            JadwalTglModel jadwalTgl = new JadwalTglModel
            {
                KodeDokter = "A",
                KodeLayanan = "B",
                TglJadwal = "2017-09-01",
                Jam = "08:00",
                Durasi = 60,
                Max = 4,
                Booked = 1
            };

            //  act
            _jadwalTglDal.Insert(jadwalTgl);
        }

        [TestMethod()]
        public void JadwalTglDal_Update_Test()
        {
            //  arrange
            JadwalTglModel jadwalTgl = new JadwalTglModel
            {
                KodeDokter = "A",
                KodeLayanan = "B",
                TglJadwal = "2017-09-01",
                Jam = "08:00",
                Durasi = 60,
                Max = 4,
                Booked = 1
            };
            _jadwalTglDal.Update(jadwalTgl);
            jadwalTgl.Max = 5;

            //  act
            _jadwalTglDal.Update(jadwalTgl);
        }

        [TestMethod()]
        public void JadwalTglDal_GetData_Test()
        {
            //  arrange
            JadwalTglModel jadwalTgl = new JadwalTglModel
            {
                KodeDokter = "A",
                KodeLayanan = "B",
                TglJadwal = "2017-09-01",
                Jam = "08:00",
                Durasi = 60,
                Max = 4,
                Booked = 1
            };
            _jadwalTglDal.Insert(jadwalTgl);

            //  act
            var dummy = _jadwalTglDal.GetData("A", "2017-09-01", "08:00");

        }

        [TestMethod()]
        public void JadwalTglDal_ListDataLayanan_Test()
        {
            //  arrange
            JadwalTglModel jadwalTgl = new JadwalTglModel
            {
                KodeDokter = "A",
                KodeLayanan = "B",
                TglJadwal = "2017-09-01",
                Jam = "08:00",
                Durasi = 60,
                Max = 4,
                Booked = 1
            };
            _jadwalTglDal.Insert(jadwalTgl);

            //  act
            var dummy = _jadwalTglDal.ListData("2017-09-01", 
                new LayananModel { Kode = "B" });
        }

        [TestMethod()]
        public void JadwalTglDal_ListDataDokter_Test()
        {
            //  arrange
            JadwalTglModel jadwalTgl = new JadwalTglModel
            {
                KodeDokter = "A",
                KodeLayanan = "B",
                TglJadwal = "2017-09-01",
                Jam = "08:00",
                Durasi = 60,
                Max = 4,
                Booked = 1
            };
            _jadwalTglDal.Insert(jadwalTgl);

            //  act
            var dummy = _jadwalTglDal.ListData("2017-09-01",
                new DokterModel { Kode = "A" });
        }

        [TestMethod()]
        public void JadwalTglDal_ListDataAll_Test()
        {
            //  arrange
            JadwalTglModel jadwalTgl = new JadwalTglModel
            {
                KodeDokter = "A",
                KodeLayanan = "B",
                TglJadwal = "2017-09-01",
                Jam = "08:00",
                Durasi = 60,
                Max = 4,
                Booked = 1
            };
            _jadwalTglDal.Insert(jadwalTgl);

            //  act
            var dummy = _jadwalTglDal.ListData("2017-09-01");
        }


        [TestMethod()]
        public void JadwalTglDal_Delete_Test()
        {
            _jadwalTglDal.Delete("A", "20-09-2017", "09:00");
        }

        /// <summary>
        ///     IsExist();
        ///     Kondisi found
        ///     => expect True
        /// </summary>
        [TestMethod()]
        public void JadwalTglDal_IsExist_Found_Test()
        {
            //  arrange
            var dummy = new JadwalTglModel
            {
                KodeDokter = "A",
                NamaDokter = "Data1",
                KodeLayanan = "B",
                NamaLayanan = "Data2",
                TglJadwal = "2017-01-01",
                FilePhoto = "Data3.jpg",
                Jam = "09:00",
                Durasi = 60,
                Max = 4,
                Booked = 1
            };
            _jadwalTglDal.Insert(dummy);

            //  act
            var actual = _jadwalTglDal.IsExist("A", "2017-01-01", "09:00");

            //  assert
            Assert.IsTrue(actual);
        }

        /// <summary>
        ///     IsExist();
        ///     Kondisi Not found
        ///     => expect False
        /// </summary>
        [TestMethod()]
        public void JadwalTglDal_IsExist_NotFound_Test()
        {
            //  arrange
            var dummy = new JadwalTglModel
            {
                KodeDokter = "A",
                NamaDokter = "Data1",
                KodeLayanan = "B",
                NamaLayanan = "Data2",
                TglJadwal = "2017-01-01",
                FilePhoto = "Data3.jpg",
                Jam = "09:00",
                Durasi = 60,
                Max = 4,
                Booked = 1
            };
            _jadwalTglDal.Insert(dummy);

            //  act
            var actual = _jadwalTglDal.IsExist("A", "2017-01-01", "10:00");

            //  assert
            Assert.IsFalse(actual);
        }

    }
}