using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEnd.Models
{
    public class JadwalHariModel
    {
        public string Kode {get;set;}
        public string KodeDokter { get; set; }
        public string NamaDokter { get; set; }
        public string KodeLayanan { get; set; }
        public string NamaLayanan { get; set; }
        public int Hari { get; set; }
        public string JamMulai { get; set; }
        public string JamSelesai { get; set; }
        
        public List<JadwalHariPerJamModel> JadwalPerJams { get; set; }
    }

    public class JadwalHariPerJamModel
    {
        public string Jam { get; set; }
        public int Durasi { get; set; }
        public int Max { get; set; }
        public int Booked { get; set; }
    }
}