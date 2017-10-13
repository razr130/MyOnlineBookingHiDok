using Microsoft.VisualStudio.TestTools.UnitTesting;
using BackEnd.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Dal;
using Rhino.Mocks;
using BackEnd.Models;

namespace BackEnd.BL.Tests
{
    [TestClass()]
    public class LayananBLTests
    {
        ILayananDal _layananDal;
        ILayananBL _layananBL;

        [TestInitialize]
        public void TestInit()
        {
            _layananDal = MockRepository.GenerateStub<ILayananDal>();
            _layananBL = new LayananBL(_layananDal);
        }

        [TestCleanup]
        public void TestCleanUp()
        {

        }

        /// <summary>
        ///     IsExist()
        ///     => Expect Succeed
        /// </summary>
        [TestMethod()]
        public void LayananBL_IsExist_Test()
        {
            // arrange
            _layananDal.Stub(x => x.GetById("A")).Return(new Models.LayananModel());

            //  act
            var dummy = _layananBL.IsExist("A");

            //  assert
            Assert.IsTrue(dummy);
        }

        /// <summary>
        ///     Save();
        ///     => Expect Succeed
        /// </summary>
        [TestMethod()]
        public void LayananBL_Save_Succeed_Test()
        {
            //  arrange
            var layanan = new LayananModel
            {
                Kode = "A",
                Nama = "Data1"
            };
            _layananDal.Stub(x => x.GetById("A")).Return(null);

            //  act
            _layananBL.Save(layanan);

            //  assert
            _layananDal.AssertWasCalled(x => x.Insert(Arg<LayananModel>.Is.Anything));
        }

        /// <summary>
        ///     Save();
        ///     => Expect "KODE LAYANAN kosong"
        /// </summary>
        [TestMethod()]
        public void LayananBL_Save_KodeLayananKosong_Test()
        {
            //  arrange
            var layanan = new LayananModel
            {
                Kode = "",
                Nama = "Data1"
            };

            //  act
            string errorMsg = "";
            try
            {
                _layananBL.Save(layanan);
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }

            //  assert
            Assert.IsTrue(errorMsg == "KODE LAYANAN kosong");
        }

        /// <summary>
        ///     Save();
        ///     => Expect "NAMA LAYANAN kosong"
        /// </summary>
        [TestMethod()]
        public void LayananBL_Save_NamaLayananKosong_Test()
        {
            //  arrange
            var layanan = new LayananModel
            {
                Kode = "A",
                Nama = ""
            };

            //  act
            string errorMsg = "";
            try
            {
                _layananBL.Save(layanan);
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }

            //  assert
            Assert.IsTrue(errorMsg == "NAMA LAYANAN kosong");
        }

        /// <summary>
        ///     Deelete();
        ///     => Expect Succeed
        /// </summary>
        [TestMethod()]
        public void LayananBL_Delete_Test()
        {
            //  arrange

            //  act
            _layananBL.Delete("A");

            //  assert
            _layananDal.AssertWasCalled(x => x.Delete("A"));
        }

        /// <summary>
        ///     GetById();
        ///     => Expect Succeed
        /// </summary>
        [TestMethod()]
        public void LayananBL_GetById_Test()
        {
            //  arrange

            //  act
            var dummy = _layananBL.GetById("A");

            //  assert
            _layananDal.AssertWasCalled(x => x.GetById("A"));
        }

        /// <summary>
        ///     ListData();
        ///     => Expect Succeed
        /// </summary>
        [TestMethod()]
        public void LayananBL_ListData_Test()
        {
            //  arrange

            //  act
            var dummy = _layananBL.ListData(LayananListDataType.All);

            //  assert
            _layananDal.AssertWasCalled(x => x.ListData(LayananListDataType.All));
        }


    }
}