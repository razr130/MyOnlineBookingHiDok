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
    public class DokterDalTests
    {
        DokterDal dokterDal;

        public DokterDalTests()
        {
            dokterDal = new DokterDal();
        }

        [TestInitialize]
        public void TestInit()
        {
            string sSql = @" DELETE ta_dokter";
            using (SqlConnection conn = new SqlConnection(dokterDal.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            string sSql = @" DELETE ta_dokter";
            using (SqlConnection conn = new SqlConnection(dokterDal.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        [TestMethod()]
        public void DokterDal_Insert_Succeed_Test()
        {
            //  arrange
            DokterModel dokter = new DokterModel
            {
                Kode = "A",
                Nama = "Data1",
                KodeLayanan = "B",
                PhotoFileName = "File_.jpg"
            };

            //  act
            dokterDal.Insert(dokter);
        }

        [TestMethod()]
        public void DokterDal_Update_Succeed_Test()
        {
            //  arrange
            DokterModel dokter = new DokterModel
            {
                Kode = "A",
                Nama = "Data1",
                KodeLayanan = "B",
                PhotoFileName = "File_.jpg"
            };

            //  act
            dokterDal.Update(dokter);
        }

        [TestMethod()]
        public void DokterDal_Delete_Succeed_Test()
        {
            //  act
            dokterDal.Delete("A");
        }

        [TestMethod()]
        public void DokterDal_GetById_Test()
        {
            //  arrange
            DokterModel dokter = new DokterModel
            {
                Kode = "A",
                Nama = "Data1",
                KodeLayanan = "A",
                PhotoFileName = "Photo.jpg"
            };
            dokterDal.Insert(dokter);

            //  act
            dokterDal.Delete("A");
        }

        [TestMethod()]
        public void DokterDal_ListData_Test()
        {
            //  arrange
            DokterModel dokter = new DokterModel
            {
                Kode = "A",
                Nama = "Data1",
                KodeLayanan = "A",
                PhotoFileName = "Photo.jpg"
            };
            dokterDal.Insert(dokter);
            //
            dokter = new DokterModel
            {
                Kode = "B",
                Nama = "Data2",
                KodeLayanan = "B",
                PhotoFileName = "Photo2.jpg"
            };
            dokterDal.Insert(dokter);

            //  act
            List<DokterModel> dummy = dokterDal.ListData();
        }

        [TestMethod()]
        public void DokterDal_ListData_Test1()
        {
            //  arrange
            DokterModel dokter = new DokterModel
            {
                Kode = "A",
                Nama = "Data1",
                KodeLayanan = "A",
                PhotoFileName = "Photo.jpg"
            };
            dokterDal.Insert(dokter);
            //
            dokter = new DokterModel
            {
                Kode = "B",
                Nama = "Data2",
                KodeLayanan = "B",
                PhotoFileName = "Photo2.jpg"
            };
            dokterDal.Insert(dokter);

            //  act
            List<DokterModel> dummy = dokterDal.ListData("B");
        }
    }
}