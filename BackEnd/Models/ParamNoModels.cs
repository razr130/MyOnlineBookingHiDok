using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEnd.Models
{
    public interface IParamNo
    {
        string KodeParam { get; }
        long Value { get; set; }
        string FormatedKodeBaru { get; }
    }

    public class ParamNoJadwal : IParamNo
    {
        public string KodeParam { get { return "JD"; } }
        public long Value { get; set; }
        public string FormatedKodeBaru
        {
            get
            {
                string retVal = Value.ToString().Trim();
                retVal = KodeParam + retVal.PadLeft(8, '0');
                return retVal;
            }
        }
    }
}