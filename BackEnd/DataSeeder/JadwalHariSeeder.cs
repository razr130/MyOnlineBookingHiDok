using BackEnd.BL;
using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEnd.DataSeeder
{
    public class JadwalHariSeeder
    {
        public void Seed()
        {
            var jadwalHariBL = new JadwalHariBL();
            jadwalHariBL.Clear();

            var sesion0800_1100 = new List<JadwalHariPerJamModel>
            {
                new JadwalHariPerJamModel
                {
                    Jam = "08:00",
                    Durasi = 60,
                    Max = 4,
                    Booked = 0
                },
                new JadwalHariPerJamModel
                {
                    Jam = "09:00",
                    Durasi = 60,
                    Max = 4,
                    Booked = 0
                },
                new JadwalHariPerJamModel
                {
                    Jam = "10:00",
                    Durasi = 60,
                    Max = 4,
                    Booked = 0
                },
            };
            var sesion1200_1430 = new List<JadwalHariPerJamModel>
            {
                new JadwalHariPerJamModel
                {
                    Jam = "12:00",
                    Durasi = 60,
                    Max = 4,
                    Booked = 0
                },
                new JadwalHariPerJamModel
                {
                    Jam = "13:00",
                    Durasi = 60,
                    Max = 4,
                    Booked = 0
                },
                new JadwalHariPerJamModel
                {
                    Jam = "14:00",
                    Durasi = 30,
                    Max = 2,
                    Booked = 0
                },
            };
            var sesion1600_1800 = new List<JadwalHariPerJamModel>
            {
                new JadwalHariPerJamModel
                {
                    Jam = "16:00",
                    Durasi = 60,
                    Max = 4,
                    Booked = 0
                },
                new JadwalHariPerJamModel
                {
                    Jam = "17:00",
                    Durasi = 60,
                    Max = 4,
                    Booked = 0
                }
            };
            var sesion1900_2130 = new List<JadwalHariPerJamModel>
            {
                new JadwalHariPerJamModel
                {
                    Jam = "19:00",
                    Durasi = 60,
                    Max = 4,
                    Booked = 0
                },
                new JadwalHariPerJamModel
                {
                    Jam = "20:00",
                    Durasi = 60,
                    Max = 4,
                    Booked = 0
                },
                new JadwalHariPerJamModel
                {
                    Jam = "21:00",
                    Durasi = 30,
                    Max = 2,
                    Booked = 0
                }
            };

            //  AgusSumadi
            var jadwalHari = new JadwalHariModel
            {
                Kode = "",
                KodeDokter = "DK001",
                KodeLayanan = "RJ001",
                Hari = 1,
                JamMulai = "08:00",
                JamSelesai = "11:00",
                JadwalPerJams = sesion0800_1100
            };
            jadwalHariBL.Save(jadwalHari);

            jadwalHari.Kode = "";
            jadwalHari.Hari = 2;
            jadwalHariBL.Save(jadwalHari);

            jadwalHari.Kode = "";
            jadwalHari.Hari = 3;
            jadwalHari.JamMulai = "16:00";
            jadwalHari.JamSelesai = "18:00";
            jadwalHari.JadwalPerJams = sesion1600_1800;
            jadwalHariBL.Save(jadwalHari);

            // Budi SUkartono
            jadwalHari = new JadwalHariModel
            {
                Kode = "",
                KodeDokter = "DK002",
                KodeLayanan = "RJ001",
                Hari = 2,
                JamMulai = "06:00",
                JamSelesai = "18:00",
                JadwalPerJams = sesion1600_1800
            };
            jadwalHariBL.Save(jadwalHari);
            jadwalHari.Kode = "";
            jadwalHari.Hari = 3;
            jadwalHariBL.Save(jadwalHari);
            jadwalHari.Kode = "";
            jadwalHari.Hari = 6;
            jadwalHariBL.Save(jadwalHari);

            // Citra Rini Ghazali
            jadwalHari = new JadwalHariModel
            {
                Kode = "",
                KodeDokter = "DK003",
                KodeLayanan = "RJ001",
                Hari = 1,
                JamMulai = "12:00",
                JamSelesai = "14:30",
                JadwalPerJams = sesion1200_1430
            };
            jadwalHariBL.Save(jadwalHari);
            jadwalHari.Kode = "";
            jadwalHari.Hari = 2;
            jadwalHariBL.Save(jadwalHari);
            jadwalHari.Kode = "";
            jadwalHari.Hari = 5;
            jadwalHariBL.Save(jadwalHari);

            // Darwin Saswono
            jadwalHari = new JadwalHariModel
            {
                Kode = "",
                KodeDokter = "DK004",
                KodeLayanan = "RJ002",
                Hari = 3,
                JamMulai = "08:00",
                JamSelesai = "11:00",
                JadwalPerJams = sesion0800_1100
            };
            jadwalHariBL.Save(jadwalHari);
            jadwalHari.Kode = "";
            jadwalHari.Hari = 4;
            jadwalHariBL.Save(jadwalHari);
            jadwalHari.Kode = "";
            jadwalHari.Hari = 5;
            jadwalHariBL.Save(jadwalHari);
            jadwalHari.Kode = "";
            jadwalHari.Hari = 6;
            jadwalHariBL.Save(jadwalHari);

            // Etik Jazuli
            jadwalHari = new JadwalHariModel
            {
                Kode = "",
                KodeDokter = "DK005",
                KodeLayanan = "RJ002",
                Hari = 1,
                JamMulai = "19:00",
                JamSelesai = "21:30",
                JadwalPerJams = sesion1900_2130
            };
            jadwalHariBL.Save(jadwalHari);

            jadwalHari.Kode = "";
            jadwalHari.Hari = 2;
            jadwalHariBL.Save(jadwalHari);

            jadwalHari.Kode = "";
            jadwalHari.Hari = 4;
            jadwalHariBL.Save(jadwalHari);

            // Firdaus Allahudin
            jadwalHari = new JadwalHariModel
            {
                Kode = "",
                KodeDokter = "DK006",
                KodeLayanan = "RJ002",
                Hari = 2,
                JamMulai = "08:00",
                JamSelesai = "11:00",
                JadwalPerJams = sesion0800_1100
            };
            jadwalHariBL.Save(jadwalHari);

            jadwalHari.Kode = "";
            jadwalHari.Hari = 4;
            jadwalHariBL.Save(jadwalHari);

            jadwalHari.Kode = "";
            jadwalHari.Hari = 6;
            jadwalHari.JamMulai = "19:00";
            jadwalHari.JamSelesai = "21:30";
            jadwalHari.JadwalPerJams = sesion1900_2130;
            jadwalHariBL.Save(jadwalHari);

            // Gerick Schuldze
            jadwalHari = new JadwalHariModel
            {
                Kode = "",
                KodeDokter = "DK007",
                KodeLayanan = "RJ002",
                Hari = 1,
                JamMulai = "08:00",
                JamSelesai = "11:00",
                JadwalPerJams = sesion0800_1100
            };
            jadwalHariBL.Save(jadwalHari);

            jadwalHari.Kode = "";
            jadwalHari.Hari = 3;
            jadwalHari.JamMulai = "12:00";
            jadwalHari.JamSelesai = "14:30";
            jadwalHari.JadwalPerJams = sesion1200_1430;      
            jadwalHariBL.Save(jadwalHari);

            jadwalHari.Kode = "";
            jadwalHari.Hari = 5;
            jadwalHari.JamMulai = "19:00";
            jadwalHari.JamSelesai = "21:30";
            jadwalHari.JadwalPerJams = sesion1900_2130;
            jadwalHariBL.Save(jadwalHari);

            // Husoro Getadiono
            jadwalHari = new JadwalHariModel
            {
                Kode = "",
                KodeDokter = "DK008",
                KodeLayanan = "RJ003",
                Hari = 1,
                JamMulai = "08:00",
                JamSelesai = "11:00",
                JadwalPerJams = sesion0800_1100
            };
            jadwalHariBL.Save(jadwalHari);

            // Indah Prisesa
            jadwalHari = new JadwalHariModel
            {
                Kode = "",
                KodeDokter = "DK009",
                KodeLayanan = "RJ003",
                Hari = 1,
                JamMulai = "08:00",
                JamSelesai = "11:00",
                JadwalPerJams = sesion0800_1100
            };
            jadwalHariBL.Save(jadwalHari);

            jadwalHari.Kode = "";
            jadwalHari.Hari = 2;
            jadwalHariBL.Save(jadwalHari);

            jadwalHari.Kode = "";
            jadwalHari.Hari = 3;
            jadwalHariBL.Save(jadwalHari);

            jadwalHari.Kode = "";
            jadwalHari.Hari = 4;
            jadwalHari.JamMulai = "19:00";
            jadwalHari.JamSelesai = "21:30";
            jadwalHari.JadwalPerJams = sesion1900_2130;
            jadwalHariBL.Save(jadwalHari);

            jadwalHari.Kode = "";
            jadwalHari.Hari = 5;
            jadwalHari.JamMulai = "19:00";
            jadwalHari.JamSelesai = "21:30";
            jadwalHari.JadwalPerJams = sesion1900_2130;
            jadwalHariBL.Save(jadwalHari);

            jadwalHari.Kode = "";
            jadwalHari.Hari = 6;
            jadwalHari.JamMulai = "19:00";
            jadwalHari.JamSelesai = "21:30";
            jadwalHari.JadwalPerJams = sesion1900_2130;
            jadwalHariBL.Save(jadwalHari);

            // Joko Putranto
            jadwalHari = new JadwalHariModel
            {
                Kode = "",
                KodeDokter = "DK010",
                KodeLayanan = "RJ004",
                Hari = 4,
                JamMulai = "08:00",
                JamSelesai = "11:00",
                JadwalPerJams = sesion0800_1100
            };
            jadwalHariBL.Save(jadwalHari);

            jadwalHari.Kode = "";
            jadwalHari.Hari = 6;
            jadwalHari.JamMulai = "19:00";
            jadwalHari.JamSelesai = "21:30";
            jadwalHari.JadwalPerJams = sesion1900_2130;
            jadwalHariBL.Save(jadwalHari);

            // Kumala Andriningsih
            jadwalHari = new JadwalHariModel
            {
                Kode = "",
                KodeDokter = "DK011",
                KodeLayanan = "RJ004",
                Hari = 1,
                JamMulai = "08:00",
                JamSelesai = "11:00",
                JadwalPerJams = sesion0800_1100
            };
            jadwalHariBL.Save(jadwalHari);

            jadwalHari.Kode = "";
            jadwalHari.Hari = 2;
            jadwalHari.JamMulai = "12:00";
            jadwalHari.JamSelesai = "14:30";
            jadwalHari.JadwalPerJams = sesion1200_1430;
            jadwalHariBL.Save(jadwalHari);

            jadwalHari.Kode = "";
            jadwalHari.Hari = 3;
            jadwalHari.JamMulai = "16:00";
            jadwalHari.JamSelesai = "18:00";
            jadwalHari.JadwalPerJams = sesion1600_1800;
            jadwalHariBL.Save(jadwalHari);

            jadwalHari.Kode = "";
            jadwalHari.Hari = 4;
            jadwalHari.JamMulai = "19:00";
            jadwalHari.JamSelesai = "21:30";
            jadwalHari.JadwalPerJams = sesion1900_2130;
            jadwalHariBL.Save(jadwalHari);

            // Lalu Kirmanto
            jadwalHari = new JadwalHariModel
            {
                Kode = "",
                KodeDokter = "DK012",
                KodeLayanan = "RJ005",
                Hari = 1,
                JamMulai = "08:00",
                JamSelesai = "11:00",
                JadwalPerJams = sesion0800_1100
            };
            jadwalHariBL.Save(jadwalHari);

            jadwalHari.Kode = "";
            jadwalHari.Hari = 4;
            jadwalHari.JamMulai = "12:00";
            jadwalHari.JamSelesai = "14:30";
            jadwalHari.JadwalPerJams = sesion1200_1430;
            jadwalHariBL.Save(jadwalHari);

            // Mima Florence
            jadwalHari = new JadwalHariModel
            {
                Kode = "",
                KodeDokter = "DK013",
                KodeLayanan = "RJ005",
                Hari = 3,
                JamMulai = "08:00",
                JamSelesai = "11:00",
                JadwalPerJams = sesion0800_1100
            };
            jadwalHariBL.Save(jadwalHari);

            jadwalHari.Kode = "";
            jadwalHari.Hari = 4;
            jadwalHari.JamMulai = "08:00";
            jadwalHari.JamSelesai = "11:00";
            jadwalHari.JadwalPerJams = sesion0800_1100;
            jadwalHariBL.Save(jadwalHari);

            jadwalHari.Kode = "";
            jadwalHari.Hari = 5;
            jadwalHari.JamMulai = "12:00";
            jadwalHari.JamSelesai = "14:30";
            jadwalHari.JadwalPerJams = sesion1200_1430;
            jadwalHariBL.Save(jadwalHari);

            // Nihaya Simanjuntak
            jadwalHari = new JadwalHariModel
            {
                Kode = "",
                KodeDokter = "DK014",
                KodeLayanan = "RJ006",
                Hari = 1,
                JamMulai = "16:00",
                JamSelesai = "18:00",
                JadwalPerJams = sesion1600_1800
            };
            jadwalHariBL.Save(jadwalHari);

            jadwalHari.Kode = "";
            jadwalHari.Hari = 6;
            jadwalHari.JamMulai = "12:00";
            jadwalHari.JamSelesai = "14:30";
            jadwalHari.JadwalPerJams = sesion1200_1430;
            jadwalHariBL.Save(jadwalHari);

            // Ogura Kagawa
            jadwalHari = new JadwalHariModel
            {
                Kode = "",
                KodeDokter = "DK015",
                KodeLayanan = "RJ007",
                Hari = 1,
                JamMulai = "16:00",
                JamSelesai = "18:00",
                JadwalPerJams = sesion1600_1800
            };
            jadwalHariBL.Save(jadwalHari);

            jadwalHari.Kode = "";
            jadwalHari.Hari = 2;
            jadwalHariBL.Save(jadwalHari);

            jadwalHari.Kode = "";
            jadwalHari.Hari = 3;
            jadwalHari.JamMulai = "19:00";
            jadwalHari.JamSelesai = "21:30";
            jadwalHari.JadwalPerJams = sesion1900_2130;
            jadwalHariBL.Save(jadwalHari);

            jadwalHari.Kode = "";
            jadwalHari.Hari = 4;
            jadwalHari.JamMulai = "12:00";
            jadwalHari.JamSelesai = "14:30";
            jadwalHari.JadwalPerJams = sesion1200_1430;
            jadwalHariBL.Save(jadwalHari);

            jadwalHari.Kode = "";
            jadwalHari.Hari = 5;
            jadwalHari.JamMulai = "19:00";
            jadwalHari.JamSelesai = "21:30";
            jadwalHari.JadwalPerJams = sesion1900_2130;
            jadwalHariBL.Save(jadwalHari);

            // Pangeran A. Peluu
            jadwalHari = new JadwalHariModel
            {
                Kode = "",
                KodeDokter = "DK016",
                KodeLayanan = "RJ008",
                Hari = 1,
                JamMulai = "12:00",
                JamSelesai = "14:30",
                JadwalPerJams = sesion1200_1430
            };
            jadwalHariBL.Save(jadwalHari);

            jadwalHari.Kode = "";
            jadwalHari.Hari = 2;
            jadwalHari.JamMulai = "19:00";
            jadwalHari.JamSelesai = "21:30";
            jadwalHari.JadwalPerJams = sesion1900_2130;
            jadwalHariBL.Save(jadwalHari);

            jadwalHari.Kode = "";
            jadwalHari.Hari = 3;
            jadwalHariBL.Save(jadwalHari);

            // Quraish Muchlis
            jadwalHari = new JadwalHariModel
            {
                Kode = "",
                KodeDokter = "DK017",
                KodeLayanan = "RJ009",
                Hari = 4,
                JamMulai = "16:00",
                JamSelesai = "18:00",
                JadwalPerJams = sesion1600_1800
            };
            jadwalHariBL.Save(jadwalHari);

            jadwalHari.Kode = "";
            jadwalHari.Hari = 6;
            jadwalHari.JamMulai = "12:00";
            jadwalHari.JamSelesai = "14:30";
            jadwalHari.JadwalPerJams = sesion1200_1430;
            jadwalHariBL.Save(jadwalHari);

            // Rahmat Bajang
            jadwalHari = new JadwalHariModel
            {
                Kode = "",
                KodeDokter = "DK018",
                KodeLayanan = "RJ010",
                Hari = 1,
                JamMulai = "16:00",
                JamSelesai = "18:00",
                JadwalPerJams = sesion1600_1800
            };
            jadwalHariBL.Save(jadwalHari);

            jadwalHari.Kode = "";
            jadwalHari.Hari = 2;
            jadwalHariBL.Save(jadwalHari);

            jadwalHari.Kode = "";
            jadwalHari.Hari = 3;
            jadwalHari.JamMulai = "12:00";
            jadwalHari.JamSelesai = "14:30";
            jadwalHari.JadwalPerJams = sesion1200_1430;
            jadwalHariBL.Save(jadwalHari);

            jadwalHari.Kode = "";
            jadwalHari.Hari = 4;
            jadwalHariBL.Save(jadwalHari);

        }




    }
}