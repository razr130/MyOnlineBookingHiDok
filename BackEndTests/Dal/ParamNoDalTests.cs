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
    public class ParamNoDalTests
    {
        ParamNoDal paramNoDal;

        public ParamNoDalTests()
        {
            paramNoDal = new ParamNoDal();
        }

        [TestInitialize]
        public void TestInit()
        {
            string sSql = @" DELETE tz_param_no";
            using (SqlConnection conn = new SqlConnection(paramNoDal.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            string sSql = @" DELETE ta_layanan";
            using (SqlConnection conn = new SqlConnection(paramNoDal.GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand(sSql, conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        [TestMethod()]
        public void ParamNoDal_Insert_Test()
        {
            ParamNoModel paramNo = new ParamNoModel
            {
                Prefix = "AA",
                Value = 1
            };

            paramNoDal.Insert(paramNo);
        }

        [TestMethod()]
        public void ParamNoDal_Update_Test()
        {
            ParamNoModel paramNo = new ParamNoModel
            {
                Prefix = "AA",
                Value = 1
            };

            paramNoDal.Update(paramNo);
        }

        [TestMethod()]
        public void ParamNoDal_Delete_Test()
        {
            //  arrange
            ParamNoModel paramNo = new ParamNoModel
            {
                Prefix = "AA",
                Value = 1
            };
            paramNoDal.Insert(paramNo);

            //  act
            paramNoDal.Delete("AA");
        }

        [TestMethod()]
        public void ParamNoDal_GetById_Test()
        {
            //  arrange
            ParamNoModel paramNo = new ParamNoModel
            {
                Prefix = "AA",
                Value = 1
            };
            paramNoDal.Insert(paramNo);

            //  act
            var dummy = paramNoDal.GetById("AA");
        }

    }
}