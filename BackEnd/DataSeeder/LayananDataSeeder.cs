using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BackEnd.Models;
using BackEnd.BL;

namespace BackEnd.DataSeeder
{
    public class LayananDataSeeder
    {
        public void Seed()
        {
            var layananBL = new LayananBL();
            layananBL.Clear();
            //
            var layanan = new LayananModel
            {
                Kode = "RJ001",
                Nama = "Umum",
                IsPopular = true
            };
            layananBL.Save(layanan);
            //
            layanan = new LayananModel
            {
                Kode = "RJ003",
                Nama = "Anak",
                IsPopular = true
            };
            layananBL.Save(layanan);
            //
            layanan = new LayananModel
            {
                Kode = "RJ002",
                Nama = "Kandungan Kebidanan",
                IsPopular = true
            };
            layananBL.Save(layanan);
            //
            layanan = new LayananModel
            {
                Kode = "RJ004",
                Nama = "Penyakit Dalam",
                IsPopular = true
            };
            layananBL.Save(layanan);
            //
            layanan = new LayananModel
            {
                Kode = "RJ005",
                Nama = "THT",
                IsPopular = false
            };
            layananBL.Save(layanan);
            //
            layanan = new LayananModel
            {
                Kode = "RJ006",
                Nama = "Kulit Kelamin",
                IsPopular = false
            };
            layananBL.Save(layanan);
            //
            layanan = new LayananModel
            {
                Kode = "RJ007",
                Nama = "Mata",
                IsPopular = false
            };
            layananBL.Save(layanan);
            //
            layanan = new LayananModel
            {
                Kode = "RJ008",
                Nama = "Jantung",
                IsPopular = false
            };
            layananBL.Save(layanan);
            //
            layanan = new LayananModel
            {
                Kode = "RJ009",
                Nama = "Saraf",
                IsPopular = false
            };
            layananBL.Save(layanan);
            //
            layanan = new LayananModel
            {
                Kode = "RJ010",
                Nama = "Bedah",
                IsPopular = true
            };
            layananBL.Save(layanan);
        }
    }
}