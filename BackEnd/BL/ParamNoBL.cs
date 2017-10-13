using BackEnd.Dal;
using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace BackEnd.BL
{
    public interface IParamNoBL
    {
        ParamNoModel GetById(string id);
        string FormatKode(ParamNoModel paramNo, int length, string suffix);
    }

    public class ParamNoBL : IParamNoBL
    {
        IParamNoDal _paramNoDal;

        //  default constructor
        public ParamNoBL()
        {
            _paramNoDal = new ParamNoDal();
        }

        //  injected Dal constructor
        public ParamNoBL(IParamNoDal injParamNoDal)
        {
            _paramNoDal = injParamNoDal;
        }

        public string FormatKode(ParamNoModel paramNo, int length, string suffix)
        {
            string strValue = Convert.ToString(paramNo.Value).Trim();
            int appliedLength = length - paramNo.Prefix.Length - suffix.Length;
            if(appliedLength >0)
                return paramNo.Prefix + strValue.PadLeft(appliedLength, '0') + suffix;
            else
                return paramNo.Prefix + strValue + suffix;
        }

        public ParamNoModel GetById(string id)
        {
            //  ambil dari parameter database
            TransactionOptions tranOption = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            };

            ParamNoModel retVal = null;

            using (TransactionScope tran = new TransactionScope(TransactionScopeOption.Required, tranOption))
            {
                retVal = _paramNoDal.GetById(id);

                //  jika belum ada param tsb
                if (retVal == null)
                {
                    retVal = new ParamNoModel
                    {
                        Prefix = id,
                        Value = 1
                    };
                    _paramNoDal.Insert(retVal);
                }
                retVal.Value++;
                _paramNoDal.Update(retVal);
                tran.Complete();
            }

            return retVal;
        }
    }
}