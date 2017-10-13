using BackEnd.BL;
using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEnd.DataSeeder
{
    public class DokterSeeder
    {
        public void Seed()
        {
            var dokterBL = new DokterBL();
            dokterBL.Clear();

            var dokter = new DokterModel
            {
                Kode = "DK001",
                Nama = "Agus Sumadi, dr.",
                KodeLayanan = "RJ001",
                PhotoFileName = "AgusSumadi.jpg"
            };
            dokterBL.Save(dokter);

            dokter = new DokterModel
            {
                Kode = "DK002",
                Nama = "Budi Sekartono, dr.",
                KodeLayanan = "RJ001",
                PhotoFileName = "BudiSekartono.jpg"
            };
            dokterBL.Save(dokter);

            dokter = new DokterModel
            {
                Kode = "DK003",
                Nama = "Citra Rini Ghazali, dr.",
                KodeLayanan = "RJ001",
                PhotoFileName = "CitraRiniGhazali.jpg"
            };
            dokterBL.Save(dokter);

            dokter = new DokterModel
            {
                Kode = "DK004",
                Nama = "Darwin Saswono, Sp.OG",
                KodeLayanan = "RJ002",
                PhotoFileName = "DarwinSaswono.jpg"
            };
            dokterBL.Save(dokter);

            dokter = new DokterModel
            {
                Kode = "DK005",
                Nama = "Etik Jazuli, Sp.OG",
                KodeLayanan = "RJ002",
                PhotoFileName = "EtikJazuli.jpg"
            };
            dokterBL.Save(dokter);

            dokter = new DokterModel
            {
                Kode = "DK006",
                Nama = "Firdaus Allahudin, Sp.OG",
                KodeLayanan = "RJ002",
                PhotoFileName = "FirdausAllahudin.jpg"
            };
            dokterBL.Save(dokter);

            dokter = new DokterModel
            {
                Kode = "DK007",
                Nama = "Gerick Schuldze, Sp.OG",
                KodeLayanan = "RJ002",
                PhotoFileName = "GerickSchuldze.jpg"
            };
            dokterBL.Save(dokter);

            dokter = new DokterModel
            {
                Kode = "DK008",
                Nama = "Husoro Getadiono, Sp.A",
                KodeLayanan = "RJ003",
                PhotoFileName = "HusoroGetadiono.jpg"
            };
            dokterBL.Save(dokter);

            dokter = new DokterModel
            {
                Kode = "DK009",
                Nama = "Indah Prisesa, Sp.A",
                KodeLayanan = "RJ003",
                PhotoFileName = "IndahPrisesa.jpg"
            };
            dokterBL.Save(dokter);

            dokter = new DokterModel
            {
                Kode = "DK010",
                Nama = "Joko Putranto, Sp.PD",
                KodeLayanan = "RJ004",
                PhotoFileName = "JokoPutranto.jpg"
            };
            dokterBL.Save(dokter);

            dokter = new DokterModel
            {
                Kode = "DK011",
                Nama = "Kumala Andriningsih, Sp.PD",
                KodeLayanan = "RJ004",
                PhotoFileName = "KumalaAndriningsih.jpg"
            };
            dokterBL.Save(dokter);

            dokter = new DokterModel
            {
                Kode = "DK012",
                Nama = "Lalu Kirmanto, Sp.THT-KL",
                KodeLayanan = "RJ005",
                PhotoFileName = "LaluKirmanto.jpg"
            };
            dokterBL.Save(dokter);

            dokter = new DokterModel
            {
                Kode = "DK013",
                Nama = "Mima Florence, Sp.THT-KL",
                KodeLayanan = "RJ005",
                PhotoFileName = "MimaFlorence.jpg"
            };
            dokterBL.Save(dokter);

            dokter = new DokterModel
            {
                Kode = "DK014",
                Nama = "Nihaya Simanjuntak, Sp.KK",
                KodeLayanan = "RJ006",
                PhotoFileName = "NihayaSimanjuntak.jpg"
            };
            dokterBL.Save(dokter);

            dokter = new DokterModel
            {
                Kode = "DK015",
                Nama = "Ogura Kagawa, Sp.M",
                KodeLayanan = "RJ007",
                PhotoFileName = "OguraKagawa.jpg"
            };
            dokterBL.Save(dokter);

            dokter = new DokterModel
            {
                Kode = "DK016",
                Nama = "Pangeran A. Peluu, Sp.JP",
                KodeLayanan = "RJ008",
                PhotoFileName = "PangeranAPeluu.jpg"
            };
            dokterBL.Save(dokter);

            dokter = new DokterModel
            {
                Kode = "DK017",
                Nama = "Quraish Muchlis, Sp.S",
                KodeLayanan = "RJ009",
                PhotoFileName = "AbdulMuchlis.jpg"
            };
            dokterBL.Save(dokter);

            dokter = new DokterModel
            {
                Kode = "DK018",
                Nama = "Rahmat Bajang, Sp.B",
                KodeLayanan = "RJ010",
                PhotoFileName = "RahmatBajang.jpg"
            };
            dokterBL.Save(dokter);
        }

    }
}