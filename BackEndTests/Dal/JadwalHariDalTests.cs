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
    public class JadwalHariDalTests
    {
        JadwalHariDal _jadwalHariDal;

        public JadwalHariDalTests()
        {
            _jadwalHariDal = new JadwalHariDal();
        }

        [TestInitialize]
        public void TestInit()
        {
            string sSql = @" DELETE ta_jadwal_hari ";
            using (SqlConnection conn = new SqlConnection(_jadwalHariDal.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            string sSql = @" DELETE ta_jadwal_hari ";
            using (SqlConnection conn = new SqlConnection(_jadwalHariDal.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        [TestMethod()]
        public void JadwalHariDal_Insert_Test()
        {
            //  arrange
            JadwalHariModel jadwal = new JadwalHariModel
            {
                Kode = "A",
                KodeDokter = "A1",
                KodeLayanan = "B1",
                Hari = 1,
                JamMulai = "10:00",
                JamSelesai = "12:00",
                JadwalPerJams = new List<JadwalHariPerJamModel>
                {
                    new JadwalHariPerJamModel{ Jam = "10:00", Max = 4, Booked = 1},
                    new JadwalHariPerJamModel{ Jam = "11:00", Max = 4, Booked = 0}
                }
            };

            //  act
            _jadwalHariDal.Insert(jadwal);
        }

        [TestMethod()]
        public void JadwalHariDal_Update_Test()
        {
            //  arrange
            JadwalHariModel jadwal = new JadwalHariModel
            {
                Kode = "A",
                KodeDokter = "A1",
                KodeLayanan = "B1",
                Hari = 1,
                JamMulai = "10:00",
                JamSelesai = "12:00",
                JadwalPerJams = new List<JadwalHariPerJamModel>
                {
                    new JadwalHariPerJamModel{ Jam = "10:00", Max = 4, Booked = 1},
                    new JadwalHariPerJamModel{ Jam = "11:00", Max = 4, Booked = 0}
                }
            };

            //  act
            _jadwalHariDal.Update(jadwal);
        }

        [TestMethod()]
        public void JadwalHariDal_Delete_Test()
        {
            //  arrange
            JadwalHariModel jadwal = new JadwalHariModel
            {
                Kode = "A",
                KodeDokter = "A1",
                KodeLayanan = "B1",
                Hari = 1,
                JamMulai = "10:00",
                JamSelesai = "12:00",
                JadwalPerJams = new List<JadwalHariPerJamModel>
                {
                    new JadwalHariPerJamModel{ Jam = "10:00", Max = 4, Booked = 1},
                    new JadwalHariPerJamModel{ Jam = "11:00", Max = 4, Booked = 0}
                }
            };
            _jadwalHariDal.Update(jadwal);

            //  act
            _jadwalHariDal.Delete("A");
        }

        [TestMethod()]
        public void JadwalHariDal_GetById_Test()
        {
            //  arrange
            JadwalHariModel jadwal = new JadwalHariModel
            {
                Kode = "A",
                KodeDokter = "A1",
                KodeLayanan = "B1",
                Hari = 1,
                JamMulai = "10:00",
                JamSelesai = "12:00",
                JadwalPerJams = new List<JadwalHariPerJamModel>
                {
                    new JadwalHariPerJamModel{ Jam = "10:00", Max = 4, Booked = 1},
                    new JadwalHariPerJamModel{ Jam = "11:00", Max = 4, Booked = 0}
                }
            };
            _jadwalHariDal.Insert(jadwal);

            //  act
            _jadwalHariDal.GetById("A");
        }

        [TestMethod()]
        public void JadwalHariDal_GetId_Test()
        {
            //  arrange
            JadwalHariModel jadwal = new JadwalHariModel
            {
                Kode = "A",
                KodeDokter = "A1",
                KodeLayanan = "B1",
                Hari = 1,
                JamMulai = "10:00",
                JamSelesai = "12:00",
                JadwalPerJams = new List<JadwalHariPerJamModel>
                {
                    new JadwalHariPerJamModel{ Jam = "10:00", Max = 4, Booked = 1},
                    new JadwalHariPerJamModel{ Jam = "11:00", Max = 4, Booked = 0}
                }
            };
            _jadwalHariDal.Insert(jadwal);

            //  act
            string id = _jadwalHariDal.GetId("A1",1, "10:00");
        }

        [TestMethod()]
        public void JadwalHariDal_ListData_Test()
        {
            //  arrange
            JadwalHariModel jadwal = new JadwalHariModel
            {
                Kode = "A",
                KodeDokter = "A1",
                KodeLayanan = "B1",
                Hari = 1,
                JamMulai = "10:00",
                JamSelesai = "12:00",
                JadwalPerJams = new List<JadwalHariPerJamModel>
                {
                    new JadwalHariPerJamModel{ Jam = "10:00", Max = 4, Booked = 1},
                    new JadwalHariPerJamModel{ Jam = "11:00", Max = 4, Booked = 0}
                }
            };
            _jadwalHariDal.Insert(jadwal);

            //  act
            var dummy = _jadwalHariDal.ListData(new LayananModel { Kode = "A1" });
        }
    }
}