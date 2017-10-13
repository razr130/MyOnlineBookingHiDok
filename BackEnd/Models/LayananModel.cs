using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEnd.Models
{
    public enum LayananListDataType
    {
        All, Popular, NotPopular
    }

    public class LayananModel
    {
        public string Kode { get; set; }
        public string Nama { get; set; }
        public bool IsPopular { get; set; }
    }
}