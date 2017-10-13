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
    public class ParamNoBLTests
    {
        IParamNoDal _paramNoDal;
        IParamNoBL _paramNoBL;

        [TestInitialize]
        public void TestInit()
        {
            _paramNoDal = MockRepository.GenerateStub<IParamNoDal>();
            _paramNoBL = new ParamNoBL(_paramNoDal);
        }

        /// <summary>
        ///     FormatKode();
        ///     Kondisi Normal
        ///     => Expect Succeed
        /// </summary>
        [TestMethod()]
        public void ParamNoBL_FormatKode_Test()
        {
            //  arrange
            ParamNoModel paramNo = new ParamNoModel
            {
                Prefix = "AA",
                Value = 2
            };

            //  act
            var dummy = _paramNoBL.FormatKode(paramNo, 5, "A");

            //  assert
            Assert.IsTrue(dummy == "AA02A");
        }

        /// <summary>
        ///     FormatKode();
        ///     Length di parameter lebih kecil dari Prefix + Suffic
        ///     => Expect Succeed
        /// </summary>        [TestMethod()]
        public void ParamNoBL_FormatKode2_Test()
        {
            //  arrange
            ParamNoModel paramNo = new ParamNoModel
            {
                Prefix = "AA",
                Value = 112
            };

            //  act
            var dummy = _paramNoBL.FormatKode(paramNo, 5, "A");

            //  assert
            Assert.IsTrue(dummy == "AA112A");
        }

        /// <summary>
        ///     GetById();
        ///     Saat ambil data maka harus langsung 
        ///     nambah otomatis value-nya (auto increament)
        /// </summary>
        [TestMethod()]
        public void ParamNoBL_GetById_Test()
        {
            //  arrange
            ParamNoModel paramNo = new ParamNoModel
            {
                Prefix = "AA",
                Value = 2
            };
            _paramNoDal.Stub(x => x.GetById("AA")).Return(paramNo);

            //  act
            var value = _paramNoBL.GetById("AA");

            //  assert
            _paramNoDal.AssertWasCalled(x => x.Update(Arg<ParamNoModel>
                .Matches(y => y.Prefix == "AA" && y.Value == 3)));
        }
    }
}