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
    public class JadwalTglBLTests
    {
        private IJadwalTglDal _jadwalTglDal;
        private IJadwalTglBL _jadwalTglBL;
        private IDokterBL _dokterBL;
        private ILayananBL _layananBL;
        private IJadwalHariBL _jadwalHariBL;

        [TestInitialize]
        public void TestInit()
        {
            _jadwalTglDal = MockRepository.GenerateStub<IJadwalTglDal>();
            _dokterBL = MockRepository.GenerateStub<IDokterBL>();
            _layananBL = MockRepository.GenerateStub<ILayananBL>();
            _jadwalHariBL = MockRepository.GenerateStub<IJadwalHariBL>();
            _jadwalTglBL = new JadwalTglBL(_jadwalTglDal, _jadwalHariBL,
                                           _dokterBL, _layananBL);
        }

        [TestCleanup]
        public void TestCleanUp()
        {

        }

        /// <summary>
        ///     Save(); *new*
        ///     DataValid
        ///     => Expect Succeed
        /// </summary>
        [TestMethod()]
        public void JadwalTglBL_Save_Succeed_Test()
        {
            //  arrange
            var jadwal = new JadwalTglModel
            {
                KodeDokter = "A",
                KodeLayanan = "B",
                TglJadwal = "01-01-2017",
                Jam = "09:00",
                Max = 4,
                Booked = 1
            };
            _layananBL.Stub(x => x.GetById("A")).Return(new LayananModel());
            _dokterBL.Stub(x => x.GetById("B")).Return(new DokterModel());
            _jadwalTglDal.Stub(x => x.IsExist("A", "01-01-2017", "09:00")).Return(true);
            //  act 
            _jadwalTglBL.Save(jadwal);

            //  assert
            _jadwalTglDal.AssertWasCalled(x => x.Insert(Arg<JadwalTglModel>.Is.Anything));
        }

        /// <summary>
        ///     Delete();
        ///     Data Valid
        ///     => Expect Succeed
        /// </summary>
        [TestMethod()]
        public void JadwalTglBL_Delete_Succeed_Test()
        {
            //  arrange
            var dummy = new List<JadwalTglModel>();
            dummy.Add(new JadwalTglModel
            {
                KodeDokter = "A",
                KodeLayanan = "B",
                TglJadwal = "01-01-2017",
                Jam = "09:00",
                Durasi = 60,
                Max = 4,
                Booked = 1
            });

            _jadwalTglDal.Stub(x => x.ListData(
                Arg<string>.Is.Equal("01-01-2017"),
                Arg<DokterModel>.Matches(y => y.Kode == "A")))
                .Return(dummy);

            //  act
            _jadwalTglBL.Delete("A", "01-01-2017");

            //  assert
            _jadwalTglDal.AssertWasCalled(
                x => x.Delete("A", "01-01-2017", "09:00"));

        }

        /// <summary>
        ///     GetData();
        ///     Expect Call Dal.GetData()
        /// </summary>
        [TestMethod()]
        public void JadwalTglBL_GetData_Succeed_Test()
        {
            //  arrange
            _jadwalTglDal.Stub(x => x.GetData("A", "01-01-2017", "09:00")).Return(null);

            //  act
            _jadwalTglBL.GetData("A", "01-01-2017", "09:00");

            //  assert
            _jadwalTglDal.AssertWasCalled(x => x.GetData("A", "01-01-2017", "09:00"));
        }

        /// <summary>
        ///     ListData(layanan, tgl)
        ///     Expect Call Dal.ListData(tgl, dokter)
        /// </summary>
        [TestMethod()]
        public void JadwalTglBL_ListData_Succeed_Test()
        {
            //  arrange
            var dokter = new DokterModel { Kode = "A" };

            //  act
            var dummy = _jadwalTglBL.ListData(dokter, "01-01-2017");

            //  assert
            _jadwalTglDal.AssertWasCalled(x => x.ListData(
                Arg<string>.Is.Equal("01-01-2017"),
                Arg<DokterModel>.Matches(y => y.Kode == "A")));

        }

        /// <summary>
        ///     ListData(layanan, tgl)
        ///     Expect Call Dal.ListData(tgl, layanan)
        /// </summary>
        [TestMethod()]
        public void JadwalTglBL_ListData2_Succeed_Test()
        {
            //  arrange
            var layanan = new LayananModel { Kode = "A" };

            //  act
            var dummy = _jadwalTglBL.ListData(layanan, "01-01-2017");

            //  assert
            _jadwalTglDal.AssertWasCalled(x => x.ListData(
                Arg<string>.Is.Equal("01-01-2017"),
                Arg<LayananModel>.Matches(y => y.Kode == "A")));

        }

        /// <summary>
        ///     Generate();
        ///     Data JadwalHari Valid
        ///     Expect Succeed
        /// </summary>
        [TestMethod()]
        public void JadwalTglBL_Generate_Succeed_Test()
        {
            //  arrange
            var jadwals = new List<JadwalHariModel>
            {
                new JadwalHariModel
                {
                    Kode = "JD001",
                    KodeDokter = "A1",
                    KodeLayanan = "B1",
                    Hari = 1,
                    JadwalPerJams = new List<JadwalHariPerJamModel>
                    {
                        new JadwalHariPerJamModel
                        {
                            Jam = "09:00",
                            Durasi = 60, Max = 4, Booked = 1
                        }
                    }
                },
                new JadwalHariModel
                {
                    Kode = "JD001",
                    KodeDokter = "A1",
                    KodeLayanan = "B1",
                    Hari = 2,
                    JadwalPerJams = new List<JadwalHariPerJamModel>
                    {
                        new JadwalHariPerJamModel
                        {
                            Jam = "09:00",
                            Durasi = 60, Max = 4, Booked = 1
                        }
                    }
                },
                new JadwalHariModel
                {
                    Kode = "JD002",
                    KodeDokter = "A1",
                    KodeLayanan = "B1",
                    Hari = 1,
                    JadwalPerJams = new List<JadwalHariPerJamModel>
                    {
                        new JadwalHariPerJamModel
                        {
                            Jam = "09:00",
                            Durasi = 60, Max = 4, Booked = 1
                        }
                    }
                }
            };

            _jadwalHariBL.Stub(x => x.ListData(
                Arg<DokterModel>.Matches(y=>y.Kode=="A1")))
                .Return(jadwals);

            //  act
            //var dummy = _jadwalTglBL.Generate("A1", "25-09-2017");

            //  assert
            //Assert.IsTrue(dummy.Count == 2);

        }
    }
}