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
    public class JadwalHariBLTests
    {
        IJadwalHariDal _jadwalHariDal;
        IDokterBL _dokterBL;
        ILayananBL _layananBL;
        IParamNoBL _paramNoBL;
        IJadwalHariBL _jadwalHariBL;

        [TestInitialize]
        public void TestInit()
        {
            _jadwalHariDal = MockRepository.GenerateStub<IJadwalHariDal>();
            _dokterBL = MockRepository.GenerateStub<IDokterBL>();
            _layananBL = MockRepository.GenerateStub<ILayananBL>();
            _paramNoBL = MockRepository.GenerateStub<IParamNoBL>();
            _jadwalHariBL = new JadwalHariBL(_jadwalHariDal, _dokterBL, _layananBL, _paramNoBL);
        }

        [TestCleanup]
        public void TestCleanUp()
        {

        }

        /// <summary>
        ///     IsExist(string id)
        ///     => Expect Succeed
        /// </summary>
        [TestMethod]
        public void JadwalHariBL_IsExist1_Succeed()
        {
            //  arrange
            var jadwal = new JadwalHariModel
            {
                Kode = "A",
                KodeDokter = "B",
                KodeLayanan = "C",
                Hari = 1,
                JadwalPerJams = new List<JadwalHariPerJamModel>
                {
                    new JadwalHariPerJamModel{Jam = "08:00", Max = 4, Booked = 1},
                    new JadwalHariPerJamModel{Jam = "09:00", Max = 4, Booked = 0}
                }
            };
            var jadwals = new List<JadwalHariModel>();
            jadwals.Add(jadwal);
            _jadwalHariDal.Stub(x => x.GetById("A")).Return(jadwal);

            //  act
            var actual = _jadwalHariBL.IsExist("A");

            //  assert
            Assert.IsTrue(actual);
        }

        /// <summary>
        ///     IsExist(string id)
        ///     => Expect Failed
        /// </summary>
        [TestMethod]
        public void JadwalHariBL_IsExist1_Failed()
        {
            //  arrange
            _jadwalHariDal.Stub(x => x.GetById("A")).Return(null);

            //  act
            var actual = _jadwalHariBL.IsExist("A");

            //  assert
            Assert.IsFalse(actual);
        }

        /// <summary>
        ///     IsExist(string kodeDokter, int hari, string jam);
        ///     => Expect Succeed 
        /// </summary>
        [TestMethod()]
        public void JadwalHariBL_IsExist2_Succeed()
        {
            //  arrange
            var jadwal = new JadwalHariModel
            {
                Kode = "A",
                KodeDokter = "B",
                KodeLayanan = "C",
                Hari = 1,
                JadwalPerJams = new List<JadwalHariPerJamModel>
                {
                    new JadwalHariPerJamModel{Jam = "08:00", Max = 4, Booked = 1},
                    new JadwalHariPerJamModel{Jam = "09:00", Max = 4, Booked = 0}
                }
            };
            var jadwals = new List<JadwalHariModel>();
            jadwals.Add(jadwal);
            _jadwalHariDal.Stub(x => x.ListData(
                Arg<DokterModel>.Matches(y=>y.Kode == "B")))
                .Return(jadwals);

            //  act
            var actual = _jadwalHariBL.IsExist("B", 1, "09:00");

            //  assert
            Assert.IsTrue(actual);
        }

        /// <summary>
        ///     IsExist(string kodeDokter, int hari, string jam);
        ///     => Expect Failed
        /// </summary>
        [TestMethod()]
        public void JadwalHariBL_IsExist2_Failed_Test()
        {
            //  arrange
            var jadwal = new JadwalHariModel
            {
                Kode = "A",
                KodeDokter = "B",
                KodeLayanan = "C",
                Hari = 1,
                JadwalPerJams = new List<JadwalHariPerJamModel>
                {
                    new JadwalHariPerJamModel{Jam = "08:00", Max = 4, Booked = 1},
                    new JadwalHariPerJamModel{Jam = "10:00", Max = 4, Booked = 0}
                }
            }; var jadwals = new List<JadwalHariModel>();
            jadwals.Add(jadwal);
            _jadwalHariDal.Stub(x => x.ListData(
                new DokterModel { Kode = "A" }))
                .Return(jadwals);

            //  act
            var actual = _jadwalHariBL.IsExist("A", 1, "09:00");

            //  assert
            Assert.IsFalse(actual);
        }

        /// <summary>
        ///     IsExist(string kodeDokter, string tanggal, string jam);
        ///     => Expect => Succeed 
        /// </summary>
        [TestMethod()]
        public void JadwalHariBL_IsExist3_Succeed_Test()
        {
            //  arrange
            var jadwal = new JadwalHariModel
            {
                Kode = "A",
                KodeDokter = "B",
                KodeLayanan = "C",
                Hari = 1,
                JadwalPerJams = new List<JadwalHariPerJamModel>
                {
                    new JadwalHariPerJamModel{Jam = "08:00", Max = 4, Booked = 1},
                    new JadwalHariPerJamModel{Jam = "09:00", Max = 4, Booked = 0}
                }
            }; var jadwals = new List<JadwalHariModel>();
            jadwals.Add(jadwal);
            _jadwalHariDal.Stub(x => x.ListData(
                Arg<DokterModel>.Matches(y=>y.Kode == "A")))
                .Return(jadwals);

            //  act 
            var actual = _jadwalHariBL.IsExist("A", "2017-09-18", "09:00");

            //  assert
            Assert.IsTrue(actual);
        }

        /// <summary>
        ///     IsExist(string kodeDokter, string tanggal, string jam);
        ///     => Expect Failed
        /// </summary>
        [TestMethod()]
        public void JadwalHariBL_IsExist3_Failed_Test()
        {
            //  arrange
            var jadwal = new JadwalHariModel
            {
                Kode = "A",
                KodeDokter = "B",
                KodeLayanan = "C",
                Hari = 2,
                JadwalPerJams = new List<JadwalHariPerJamModel>
                {
                    new JadwalHariPerJamModel{Jam = "08:00", Max = 4, Booked = 1},
                    new JadwalHariPerJamModel{Jam = "09:00", Max = 4, Booked = 0}
                }
            };
            var jadwals = new List<JadwalHariModel>();
            jadwals.Add(jadwal);
            _jadwalHariDal.Stub(x => x.ListData(
                Arg<DokterModel>.Matches(y=>y.Kode == "A")))
                .Return(jadwals);

            //  act 
            var actual = _jadwalHariBL.IsExist("A", "2017-09-18", "09:00");

            //  assert
            Assert.IsFalse(actual);
        }

        /// <summary>
        ///     Save(); *New*
        ///     => Expect Succeed
        /// </summary>
        [TestMethod()]
        public void JadwalHariBL_Save_New_Succeed_Test()
        {
            //  arrange
            var jadwal = new JadwalHariModel
            {
                Kode = "",
                KodeDokter = "A",
                KodeLayanan = "A",
                Hari = 2,
                JamMulai = "08:00",
                JamSelesai = "10:00",
                JadwalPerJams = new List<JadwalHariPerJamModel>
                {
                    new JadwalHariPerJamModel{Jam = "08:00", Max = 4, Booked = 1},
                    new JadwalHariPerJamModel{Jam = "09:00", Max = 4, Booked = 0}
                }
            };
            _layananBL.Stub(x => x.GetById("A")).Return(new LayananModel { Kode = "A", Nama = "Data2" });
            _dokterBL.Stub(x => x.GetById("A")).Return(new DokterModel { Kode = "A", Nama = "Data2" });

            //  act
            _jadwalHariBL.Save(jadwal);

            //  assert
            _jadwalHariDal.AssertWasCalled(x => x.Insert(Arg<JadwalHariModel>.Is.Anything));
        }

        /// <summary>
        ///     Save() *Edit*;
        ///     => Expect Succeed
        /// </summary>
        [TestMethod()]
        public void JadwalHariBL_Save_Edit_Succeed_Test()
        {
            //  arrange
            var jadwal = new JadwalHariModel
            {
                Kode = "A",
                KodeDokter = "A",
                KodeLayanan = "A",
                Hari = 2,
                JamMulai = "08:00",
                JamSelesai = "10:00",
                JadwalPerJams = new List<JadwalHariPerJamModel>
                {
                    new JadwalHariPerJamModel{Jam = "08:00", Max = 4, Booked = 1},
                    new JadwalHariPerJamModel{Jam = "09:00", Max = 4, Booked = 0}
                }
            };
            _layananBL.Stub(x => x.GetById("A")).Return(new LayananModel { Kode = "A", Nama = "Data2" });
            _dokterBL.Stub(x => x.GetById("A")).Return(new DokterModel { Kode = "A", Nama = "Data2" });

            //  act
            _jadwalHariBL.Save(jadwal);

            //  assert
            _jadwalHariDal.AssertWasCalled(x => x.Update(Arg<JadwalHariModel>.Is.Anything));
        }

        /// <summary>
        ///     Save();
        ///     => Expect "Invalid DOKTER"
        /// </summary>
        [TestMethod]
        public void JadwalHariBL_Save_Invalid_Dokter_Test()
        {
            //  arrange
            var jadwal = new JadwalHariModel
            {
                Kode = "A",
                KodeDokter = "A",
                KodeLayanan = "A",
                Hari = 2,
                JamMulai = "08:00",
                JamSelesai = "10:00",
                JadwalPerJams = new List<JadwalHariPerJamModel>
                {
                    new JadwalHariPerJamModel{Jam = "08:00", Max = 4, Booked = 1},
                    new JadwalHariPerJamModel{Jam = "09:00", Max = 4, Booked = 0}
                }
            };
            _layananBL.Stub(x => x.GetById("A")).Return(new LayananModel { Kode = "A", Nama = "Data2" });
            _dokterBL.Stub(x => x.GetById("A")).Return(new DokterModel { Kode = "A", Nama = " " });

            //  act
            var errMsg = "";
            try
            {
                _jadwalHariBL.Save(jadwal);
                errMsg = "";
            }
            catch (ArgumentException ex)
            {
                errMsg = ex.Message;
            }

            //  assert
            Assert.IsTrue(errMsg == "Invalid DOKTER");
        }

        /// <summary>
        ///     Save();
        ///     => Expect "Invalid LAYANAN"
        /// </summary>
        [TestMethod]
        public void JadwalHariBL_Save_InvalidLayanan_Test()
        {
            //  arrange
            var jadwal = new JadwalHariModel
            {
                Kode = "A",
                KodeDokter = "A",
                KodeLayanan = "A",
                Hari = 2,
                JamMulai = "08:00",
                JamSelesai = "10:00",
                JadwalPerJams = new List<JadwalHariPerJamModel>
                {
                    new JadwalHariPerJamModel{Jam = "08:00", Max = 4, Booked = 1},
                    new JadwalHariPerJamModel{Jam = "09:00", Max = 4, Booked = 0}
                }
            };
            _layananBL.Stub(x => x.GetById("A")).Return(new LayananModel { Kode = "A", Nama = "" });
            _dokterBL.Stub(x => x.GetById("A")).Return(new DokterModel { Kode = "A", Nama = "Data1" });

            //  act
            var errMsg = "";
            try
            {
                _jadwalHariBL.Save(jadwal);
                errMsg = "";
            }
            catch (ArgumentException ex)
            {
                errMsg = ex.Message;
            }

            //  assert
            Assert.IsTrue(errMsg == "Invalid LAYANAN");
        }

        /// <summary>
        ///     Save();
        ///     => Expect "Invalid HARI"
        /// </summary>
        [TestMethod]
        public void JadwalHariBL_Save_InvalidHari_Test()
        {
            //  arrange
            var jadwal = new JadwalHariModel
            {
                Kode = "A",
                KodeDokter = "A",
                KodeLayanan = "A",
                Hari = 8,
                JamMulai = "08:00",
                JamSelesai = "10:00",
                JadwalPerJams = new List<JadwalHariPerJamModel>
                {
                    new JadwalHariPerJamModel{Jam = "08:00", Max = 4, Booked = 1},
                    new JadwalHariPerJamModel{Jam = "09:00", Max = 4, Booked = 0}
                }
            };
            _layananBL.Stub(x => x.GetById("A")).Return(new LayananModel { Kode = "A", Nama = "Data1" });
            _dokterBL.Stub(x => x.GetById("A")).Return(new DokterModel { Kode = "A", Nama = "Data1" });

            //  act
            var errMsg = "";
            try
            {
                _jadwalHariBL.Save(jadwal);
                errMsg = "";
            }
            catch (ArgumentException ex)
            {
                errMsg = ex.Message;
            }

            //  assert
            Assert.IsTrue(errMsg == "Invalid HARI");
        }

        /// <summary>
        ///     Save();
        ///     => Expect "Invalid JAM MULAI"
        /// </summary>
        [TestMethod]
        public void JadwalHariBL_Save_InvalidJamMulai_Test()
        {
            //  arrange
            var jadwal = new JadwalHariModel
            {
                Kode = "A",
                KodeDokter = "A",
                KodeLayanan = "A",
                Hari = 2,
                JamMulai = "25:00",
                JamSelesai = "10:00",
                JadwalPerJams = new List<JadwalHariPerJamModel>
                {
                    new JadwalHariPerJamModel{Jam = "08:00", Max = 4, Booked = 1},
                    new JadwalHariPerJamModel{Jam = "09:00", Max = 4, Booked = 0}
                }
            };
            _layananBL.Stub(x => x.GetById("A")).Return(new LayananModel { Kode = "A", Nama = "Data1" });
            _dokterBL.Stub(x => x.GetById("A")).Return(new DokterModel { Kode = "A", Nama = "Data1" });

            //  act
            var errMsg = "";
            try
            {
                _jadwalHariBL.Save(jadwal);
                errMsg = "";
            }
            catch (ArgumentException ex)
            {
                errMsg = ex.Message;
            }

            //  assert
            Assert.IsTrue(errMsg == "Invalid JAM MULAI");
        }

        /// <summary>
        ///     Save();
        ///     => Expect "Invalid JAM SELESAI"
        /// </summary>
        [TestMethod]
        public void JadwalHariBL_Save_InvalidJamSelesai_Test()
        {
            //  arrange
            var jadwal = new JadwalHariModel
            {
                Kode = "A",
                KodeDokter = "A",
                KodeLayanan = "A",
                Hari = 2,
                JamMulai = "10:00",
                JamSelesai = "25:00",
                JadwalPerJams = new List<JadwalHariPerJamModel>
                {
                    new JadwalHariPerJamModel{Jam = "08:00", Max = 4, Booked = 1},
                    new JadwalHariPerJamModel{Jam = "09:00", Max = 4, Booked = 0}
                }
            };
            _layananBL.Stub(x => x.GetById("A")).Return(new LayananModel { Kode = "A", Nama = "Data1" });
            _dokterBL.Stub(x => x.GetById("A")).Return(new DokterModel { Kode = "A", Nama = "Data1" });

            //  act
            var errMsg = "";
            try
            {
                _jadwalHariBL.Save(jadwal);
                errMsg = "";
            }
            catch (ArgumentException ex)
            {
                errMsg = ex.Message;
            }

            //  assert
            Assert.IsTrue(errMsg == "Invalid JAM SELESAI");
        }

        /// <summary>
        ///     Save();
        ///     => Expect "Invalid JAM MULAI-SELESAI"
        /// </summary>
        [TestMethod]
        public void JadwalHariBL_Save_InvalidJamMulaiSelesai_Test()
        {
            //  arrange
            var jadwal = new JadwalHariModel
            {
                Kode = "A",
                KodeDokter = "A",
                KodeLayanan = "A",
                Hari = 2,
                JamMulai = "10:00",
                JamSelesai = "09:00",
                JadwalPerJams = new List<JadwalHariPerJamModel>
                {
                    new JadwalHariPerJamModel{Jam = "08:00", Max = 4, Booked = 1},
                    new JadwalHariPerJamModel{Jam = "09:00", Max = 4, Booked = 0}
                }
            };
            _layananBL.Stub(x => x.GetById("A")).Return(new LayananModel { Kode = "A", Nama = "Data1" });
            _dokterBL.Stub(x => x.GetById("A")).Return(new DokterModel { Kode = "A", Nama = "Data1" });

            //  act
            var errMsg = "";
            try
            {
                _jadwalHariBL.Save(jadwal);
                errMsg = "";
            }
            catch (ArgumentException ex)
            {
                errMsg = ex.Message;
            }

            //  assert
            Assert.IsTrue(errMsg == "Invalid JAM MULAI-SELESAI");
        }

        /// <summary>
        ///     Save();
        ///     => Expect "Invalid Format Detil JAM"
        /// </summary>
        [TestMethod]
        public void JadwalHariBL_Save_InvalidFormatDetilJam_Test()
        {
            //  arrange
            var jadwal = new JadwalHariModel
            {
                Kode = "A",
                KodeDokter = "A",
                KodeLayanan = "A",
                Hari = 2,
                JamMulai = "09:00",
                JamSelesai = "10:00",
                JadwalPerJams = new List<JadwalHariPerJamModel>
                {
                    new JadwalHariPerJamModel{Jam = "08:70", Max = 4, Booked = 1},
                    new JadwalHariPerJamModel{Jam = "09:00", Max = 4, Booked = 0}
                }
            };
            _layananBL.Stub(x => x.GetById("A")).Return(new LayananModel { Kode = "A", Nama = "Data1" });
            _dokterBL.Stub(x => x.GetById("A")).Return(new DokterModel { Kode = "A", Nama = "Data1" });

            //  act
            var errMsg = "";
            try
            {
                _jadwalHariBL.Save(jadwal);
                errMsg = "";
            }
            catch (ArgumentException ex)
            {
                errMsg = ex.Message;
            }

            //  assert
            Assert.IsTrue(errMsg == "Invalid Format Detil JAM");
        }

        /// <summary>
        ///     Save();
        ///     => Expect "Invalid Range Detil JAM"
        /// </summary>
        [TestMethod]
        public void JadwalHariBL_Save_InvalidRangeDetilJam1_Test()
        {
            //  arrange
            var jadwal = new JadwalHariModel
            {
                Kode = "A",
                KodeDokter = "A",
                KodeLayanan = "A",
                Hari = 2,
                JamMulai = "09:00",
                JamSelesai = "10:00",
                JadwalPerJams = new List<JadwalHariPerJamModel>
                {
                    new JadwalHariPerJamModel{Jam = "07:00", Max = 4, Booked = 1},
                    new JadwalHariPerJamModel{Jam = "09:00", Max = 4, Booked = 0}
                }
            };
            _layananBL.Stub(x => x.GetById("A")).Return(new LayananModel { Kode = "A", Nama = "Data1" });
            _dokterBL.Stub(x => x.GetById("A")).Return(new DokterModel { Kode = "A", Nama = "Data1" });

            //  act
            var errMsg = "";
            try
            {
                _jadwalHariBL.Save(jadwal);
                errMsg = "";
            }
            catch (ArgumentException ex)
            {
                errMsg = ex.Message;
            }

            //  assert
            Assert.IsTrue(errMsg == "Invalid Range Detil JAM");
        }

        /// <summary>
        ///     Save();
        ///     => Expect "Invalid Range Detil JAM"
        /// </summary>
        [TestMethod]
        public void JadwalHariBL_Save_InvalidRangeDetilJam2_Test()
        {
            //  arrange
            var jadwal = new JadwalHariModel
            {
                Kode = "A",
                KodeDokter = "A",
                KodeLayanan = "A",
                Hari = 2,
                JamMulai = "09:00",
                JamSelesai = "10:00",
                JadwalPerJams = new List<JadwalHariPerJamModel>
                {
                    new JadwalHariPerJamModel{Jam = "10:00", Max = 4, Booked = 1},
                    new JadwalHariPerJamModel{Jam = "12:00", Max = 4, Booked = 0}
                }
            };
            _layananBL.Stub(x => x.GetById("A")).Return(new LayananModel { Kode = "A", Nama = "Data1" });
            _dokterBL.Stub(x => x.GetById("A")).Return(new DokterModel { Kode = "A", Nama = "Data1" });

            //  act
            var errMsg = "";
            try
            {
                _jadwalHariBL.Save(jadwal);
                errMsg = "";
            }
            catch (ArgumentException ex)
            {
                errMsg = ex.Message;
            }

            //  assert
            Assert.IsTrue(errMsg == "Invalid Range Detil JAM");
        }

        /// <summary>
        ///     Save();
        ///     => Expect "Invalid BOOKED QTY"
        /// </summary>
        [TestMethod]
        public void JadwalHariBL_Save_InvalidBookedQty_Test()
        {
            //  arrange
            var jadwal = new JadwalHariModel
            {
                Kode = "A",
                KodeDokter = "A",
                KodeLayanan = "A",
                Hari = 2,
                JamMulai = "09:00",
                JamSelesai = "11:00",
                JadwalPerJams = new List<JadwalHariPerJamModel>
                {
                    new JadwalHariPerJamModel{Jam = "09:00", Max = 4, Booked = 1},
                    new JadwalHariPerJamModel{Jam = "10:00", Max = 4, Booked = 5}
                }
            };
            _layananBL.Stub(x => x.GetById("A")).Return(new LayananModel { Kode = "A", Nama = "Data1" });
            _dokterBL.Stub(x => x.GetById("A")).Return(new DokterModel { Kode = "A", Nama = "Data1" });

            //  act
            var errMsg = "";
            try
            {
                _jadwalHariBL.Save(jadwal);
                errMsg = "";
            }
            catch (ArgumentException ex)
            {
                errMsg = ex.Message;
            }

            //  assert
            Assert.IsTrue(errMsg == "Invalid BOOKED QTY");
        }

        /// <summary>
        ///     Save();
        ///     => Expect "Duplicated JAM PRAKTEK"
        /// </summary>
        [TestMethod]
        public void JadwalHariBL_Save_DuplicatedJamPraktek_Test()
        {
            //  arrange
            var jadwal = new JadwalHariModel
            {
                Kode = "A",
                KodeDokter = "A",
                KodeLayanan = "A",
                Hari = 2,
                JamMulai = "09:00",
                JamSelesai = "11:00",
                JadwalPerJams = new List<JadwalHariPerJamModel>
                {
                    new JadwalHariPerJamModel{Jam = "09:00", Max = 4, Booked = 1},
                    new JadwalHariPerJamModel{Jam = "10:00", Max = 4, Booked = 0},
                    new JadwalHariPerJamModel{Jam = "11:00", Max = 4, Booked = 0},
                    new JadwalHariPerJamModel{Jam = "09:00", Max = 4, Booked = 0}
                }
            };
            var jadwals = new List<JadwalHariModel>();
            jadwals.Add(jadwal);

            _layananBL.Stub(x => x.GetById("A")).Return(new LayananModel { Kode = "A", Nama = "Data1" });
            _dokterBL.Stub(x => x.GetById("A")).Return(new DokterModel { Kode = "A", Nama = "Data1" });

            //  act
            var errMsg = "";
            try
            {
                _jadwalHariBL.Save(jadwal);
                errMsg = "";
            }
            catch (ArgumentException ex)
            {
                errMsg = ex.Message;
            }

            //  assert
            Assert.IsTrue(errMsg == "Duplicated JAM PRAKTEK");
        }


        /// <summary>
        ///     Delete(id);
        ///     => Expect Succeed
        /// </summary>
        [TestMethod]
        public void JadwalHariBL_Delete_Test()
        {
            //  arrange
            _jadwalHariDal.Stub(x => x.GetById("A")).Return(new JadwalHariModel());

            //  act
            _jadwalHariBL.Delete("A");

            //  assert
            _jadwalHariDal.AssertWasCalled(x => x.Delete("A"));
        }

        /// <summary>
        ///     Delete(kodeDokter, hari, jamMulai);
        ///     => Expect Succeed
        /// </summary>
        [TestMethod]
        public void JadwalHariBL_Delete2_Test()
        {
            //  arrange
            _jadwalHariDal.Stub(x => x.GetId("A", 1, "10:00")).Return("A");
            _jadwalHariDal.Stub(x => x.GetById("A")).Return(new JadwalHariModel());

            //  act
            _jadwalHariBL.Delete("A");

            //  assert
            _jadwalHariDal.AssertWasCalled(x => x.Delete("A"));
        }

        /// <summary>
        ///     GetById(id);
        ///     => Expect Succeed
        /// </summary>
        [TestMethod]
        public void JadwalHariBL_GetById_Test()
        {
            //  arrange
            _jadwalHariDal.Stub(x => x.GetById("A")).Return(new JadwalHariModel());

            //  act
            var dummy = _jadwalHariBL.GetById("A");

            //  assert
            _jadwalHariDal.AssertWasCalled(x => x.GetById("A"));
        }

        /// <summary>
        ///     GetId();
        ///     => Expect Succeed
        /// </summary>
        [TestMethod]
        public void JadwalHariBL_GetId_Test()
        {
            //  arrange
            _jadwalHariDal.Stub(x => x.GetId("A", 1, "10:00")).Return("A");

            //  act
            var dummy = _jadwalHariBL.GetId("A", 1, "10:00");

            //  assert
            Assert.IsTrue(dummy == "A");
        }

        /// <summary>
        ///     ListData();
        ///     => Expect Succeed
        /// </summary>
        [TestMethod]
        public void JadwalHariBL_ListData_Test()
        {
            //  arrange
            var jadwals = new List<JadwalHariModel>();

            _jadwalHariDal.Stub(x => x.ListData()).Return(jadwals);

            //  act
            var dummy = _jadwalHariBL.ListData();

            //  assert
            _jadwalHariDal.AssertWasCalled(x => x.ListData());
        }

        /// <summary>
        ///     ListData(layanan);
        ///     => Expect Succeed
        /// </summary>
        [TestMethod]
        public void JadwalHariBL_ListData2_Test()
        {
            //  arrange
            var jadwals = new List<JadwalHariModel>();

            _jadwalHariDal.Stub(x => x.ListData(
                new LayananModel { Kode = "A" }))
                .Return(jadwals);

            //  act
            var dummy = _jadwalHariBL.ListData(new LayananModel { Kode = "A" });

            //  assert
            _jadwalHariDal.AssertWasCalled(x => x.ListData(
                Arg<LayananModel>.Matches(y => y.Kode =="A")));
        }

        /// <summary>
        ///     ListData(dokter);
        ///     => Expect Succeed
        /// </summary>
        [TestMethod]
        public void JadwalHariBL_ListData3_Test()
        {
            //  arrange
            var jadwals = new List<JadwalHariModel>();

            _jadwalHariDal.Stub(x => x.ListData(
                new DokterModel { Kode = "A" }))
                .Return(jadwals);

            //  act
            var dummy = _jadwalHariBL.ListData(new DokterModel { Kode = "A" });

            //  assert
            _jadwalHariDal.AssertWasCalled(x => x.ListData(
                Arg<DokterModel>.Matches(y => y.Kode == "A")));
        }

    }
}