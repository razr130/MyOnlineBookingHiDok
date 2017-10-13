using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEnd.Models
{
    public class DokterModel
    {
        public string Kode { get; set; }
        public string Nama { get; set; }
        public string KodeLayanan { get; set; }
        public string NamaLayanan { get; set; }
        public string PhotoFileName { get; set; }
    }
}