using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEnd.Models
{
    public class JadwalTglModel
    {
        public string KodeDokter { get; set; }
        public string NamaDokter { get; set; }
        public string FilePhoto { get; set; }
        public string KodeLayanan { get; set; }
        public string NamaLayanan { get; set; }
        public string TglJadwal { get; set; }
        public string Jam { get; set; }
        public int Durasi { get; set; }
        public int Max { get; set; }
        public int Booked { get; set; }
    }
}