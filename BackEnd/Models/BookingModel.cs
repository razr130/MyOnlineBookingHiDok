using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEnd.Models
{
    public class BookingModel
    {
        public string KodeTrs { get; set; }
        public string TglTrs { get; set; }
        public string JamTrs { get; set; }
        public string KodeDokter { get; set; }
        public string NamaDokter { get; set; }
        public string KodeLayanan { get; set; }
        public string NamaLayanan { get; set; }
        public string TglJadwal { get; set; }
        public string JamJadwal { get; set; }
        public string KodeMR { get; set; }
        public string NamaPasien { get; set; }
        public string TglLahir { get; set; }
        public string NoTelp { get; set; }
        public string Email { get; set; }
        public string BookingCode { get { return KodeTrs; } }
    }
}