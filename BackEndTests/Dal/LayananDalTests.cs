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
    public class LayananDalTests
    {
        LayananDal layananDal;

        public LayananDalTests()
        {
            layananDal = new LayananDal();
        }

        [TestInitialize]
        public void TestInit()
        {
            string sSql = @" DELETE ta_layanan";
            using (SqlConnection conn = new SqlConnection(layananDal.GetConnectionString()))
            using(SqlCommand cmd = new SqlCommand(sSql,conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            string sSql = @" DELETE ta_layanan";
            using (SqlConnection conn = new SqlConnection(layananDal.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        [TestMethod()]
        public void LayananDal_Insert_Succeed_Test()
        {
            LayananModel layanan = new LayananModel
            {
                Kode = "A",
                Nama = "Data1"
            };

            layananDal.Insert(layanan);
        }

        [TestMethod()]
        public void LayananDal_Update_Succeed_Test()
        {
            LayananModel layanan = new LayananModel
            {
                Kode = "A",
                Nama = "Data1"
            };

            layananDal.Update(layanan);
        }

        [TestMethod()]
        public void LayananDal_Delete_Succeed_Test()
        {
            layananDal.Delete("A");
        }

        [TestMethod()]
        public void LayananDal_GetById_Test()
        {
            //  arrange
            LayananModel layanan = new LayananModel
            {
                Kode = "A",
                Nama = "Data1",
                IsPopular = true
            };
            layananDal.Insert(layanan);

            //  act
            layananDal.Delete("A");
        }

        [TestMethod()]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        public void LayananDal_ListData_Suucced_Test(LayananListDataType type)
        {
            List<LayananModel> dummy = layananDal.ListData(type);
        }
    }
}