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
    public class DokterBLTests
    {
        IDokterBL _dokterBL;
        IDokterDal _dokterDal;
        ILayananBL _layananBL;

        [TestInitialize]
        public void TestInit()
        {
            _dokterDal = MockRepository.GenerateStub<IDokterDal>();
            _layananBL = MockRepository.GenerateStub<ILayananBL>();
            _dokterBL = new DokterBL(_dokterDal, _layananBL);
        }


        /// <summary>
        ///     IsExist()
        ///     => Expect Succeed
        /// </summary>
        [TestMethod()]
        public void DokterBL_IsExist_Test()
        {
            //  arrange
            _dokterDal.Stub(x => x.GetById("A")).Return(new DokterModel());

            //  act
            var dummy = _dokterBL.IsExist("A");

            //  assert
            Assert.IsTrue(dummy);
        }

        /// <summary>
        ///     Save() *New*
        ///     => Expect Succeed
        /// </summary>
        [TestMethod()]
        public void DokterBL_Save_New_Succeed_Test()
        {
            //  arrange
            var dokter = new DokterModel
            {
                Kode = "A",
                Nama = "Data1",
                KodeLayanan = "B",
                NamaLayanan = "Data2",
                PhotoFileName = "Data3"
            };
            _dokterDal.Stub(x => x.GetById("A")).Return(null);
            _layananBL.Stub(x => x.GetById("B")).Return(new LayananModel
            {
                Kode = "B",
                Nama = "Data2"
            });

            //  act
            _dokterBL.Save(dokter);

            //  assert
            _dokterDal.AssertWasCalled(x => x.Insert(Arg<DokterModel>.Is.Anything));
        }

        /// <summary>
        ///     Save() *Edit*
        ///     => Expect Succeed
        /// </summary>
        [TestMethod()]
        public void DokterBL_Save_Edit_Succeed_Test()
        {
            //  arrange
            var dokter = new DokterModel
            {
                Kode = "A",
                Nama = "Data1",
                KodeLayanan = "B",
                NamaLayanan = "Data2",
                PhotoFileName = "Data3"
            };
            _dokterDal.Stub(x => x.GetById("A")).Return(new DokterModel());
            _layananBL.Stub(x => x.GetById("B")).Return(new LayananModel
            {
                Kode = "B",
                Nama = "Data2"
            });

            //  act
            _dokterBL.Save(dokter);

            //  assert
            _dokterDal.AssertWasCalled(x => x.Update(Arg<DokterModel>.Is.Anything));
        }

        /// <summary>
        ///     Save()
        ///     Kode Layanan di-kosongkan
        ///     => Expect Succeed
        /// </summary>
        [TestMethod()]
        public void DokterBL_Save_OptionalLayanan_Succeed_Test()
        {
            //  arrange
            var dokter = new DokterModel
            {
                Kode = "A",
                Nama = "Data1",
                KodeLayanan = "",
                NamaLayanan = "",
                PhotoFileName = "Data3"
            };
            _dokterDal.Stub(x => x.GetById("A")).Return(null);

            //  act
            _dokterBL.Save(dokter);

            //  assert
            _dokterDal.AssertWasCalled(x => x.Insert(Arg<DokterModel>.Is.Anything));
        }

        /// <summary>
        ///     Save()
        ///     Kode Dokter dikosongkan
        ///     => Expect exception "KODE DOKTER kosong"
        /// </summary>
        [TestMethod()]
        public void DokterBL_Save_KodeKosong_Test()
        {
            //  arrange
            var dokter = new DokterModel
            {
                Kode = "",
                Nama = "Data1",
                KodeLayanan = "B",
                NamaLayanan = "Data2",
                PhotoFileName = "Data3"
            };
            _dokterDal.Stub(x => x.GetById("A")).Return(null);

            //  act
            string errMsg;
            try
            {
                _dokterBL.Save(dokter);
                errMsg = "";
            }
            catch (ArgumentException ex)
            {
                errMsg = ex.Message;
            }

            //  assert
            Assert.IsTrue(errMsg == "KODE DOKTER kosong");
        }

        /// <summary>
        ///     Save()
        ///     Nama Dokter dikosongkan
        ///     => Expect Exception "NAMA DOKTER kosong"
        /// </summary>
        [TestMethod()]
        public void DokterBL_Save_NamaKosong_Test()
        {
            //  arrange
            var dokter = new DokterModel
            {
                Kode = "A",
                Nama = "",
                KodeLayanan = "B",
                NamaLayanan = "Data2",
                PhotoFileName = "Data3"
            };
            _dokterDal.Stub(x => x.GetById("A")).Return(null);

            //  act
            string errMsg;
            try
            {
                _dokterBL.Save(dokter);
                errMsg = "";
            }
            catch (ArgumentException ex)
            {
                errMsg = ex.Message;
            }

            //  assert
            Assert.IsTrue(errMsg == "NAMA DOKTER kosong");
        }

        /// <summary>
        ///     Save()
        ///     KODE LAYANAN invalid
        ///     => Expect Exception "KODE LAYANAN invalid"
        /// </summary>
        [TestMethod()]
        public void DokterBL_Save_LayananInvalid_Test()
        {
            //  arrange
            var dokter = new DokterModel
            {
                Kode = "A",
                Nama = "Data1",
                KodeLayanan = "B",
                NamaLayanan = "Data2",
                PhotoFileName = "Data3"
            };
            _dokterDal.Stub(x => x.GetById("A")).Return(null);
            _layananBL.Stub(x => x.GetById("B")).Return(null);

            //  act
            string errMsg;
            try
            {
                _dokterBL.Save(dokter);
                errMsg = "";
            }
            catch (ArgumentException ex)
            {
                errMsg = ex.Message;
            }

            //  assert
            Assert.IsTrue(errMsg == "KODE LAYANAN invalid");
        }


        /// <summary>
        ///     Delete()
        ///     => Expect No Error
        /// </summary>
        [TestMethod()]
        public void DokterBL_Delete_Test()
        {
            //  arrange
            _dokterDal.Stub(x => x.GetById("A")).Return(new DokterModel());

            //  act
            _dokterBL.Delete("A");

            //  assert
            _dokterDal.AssertWasCalled(x => x.Delete("A"));
        }

        /// <summary>
        ///     GetById()
        ///     => Expect No Error
        /// </summary>
        [TestMethod()]
        public void DokterBL_GetById_Test()
        {
            //  arrange
            _dokterDal.Stub(x => x.GetById("A")).Return(new DokterModel());

            //  act
            var dummy = _dokterBL.GetById("A");

            //  assert
            _dokterDal.AssertWasCalled(x => x.GetById("A"));
        }

        /// <summary>
        ///     ListData()
        ///     => Expect No Error
        /// </summary>
        [TestMethod()]
        public void DokterBL_ListData_Test()
        {
            //  arrange
            _dokterDal.Stub(x => x.ListData()).Return(new List<DokterModel>());

            //  act
            var dummy = _dokterBL.ListData();

            //  assert
            _dokterDal.AssertWasCalled(x => x.ListData());
        }


        /// <summary>
        ///     ListData() Filter Layanan
        ///     => Expect No Error
        /// </summary>
        [TestMethod()]
        public void DokterBL_ListData2_Test()
        {
            //  arrange
            _dokterDal.Stub(x => x.ListData("A")).Return(new List<DokterModel>());

            //  act
            var dummy = _dokterBL.ListData("A");

            //  assert
            _dokterDal.AssertWasCalled(x => x.ListData("A"));
        }

    }
}