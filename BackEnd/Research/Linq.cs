using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEnd.Research
{
    public class Mahasiswa
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }

    public class MataKuliah
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }

    public class Nilai
    {
        public string MahasiswaID { get; set; }
        public string MataKuliahID { get; set; }
        public string NilaiHuruf { get; set; }
    }

    public class Linq
    {
        public void SimpleLinqSample()
        {
            var mahasiswas = new List<Mahasiswa>
            {
                new Mahasiswa{ID = "A01", Name = "Agus Setyawan"},
                new Mahasiswa{ID = "B01", Name = "Budi Yohanes" },
                new Mahasiswa{ID = "C01", Name = "Charles Simatupang"}
            };

            var mataKuliahs = new List<MataKuliah>
            {
                new MataKuliah{ID = "KAL", Name = "Kalkulus"},
                new MataKuliah{ID = "ENG", Name = "English"},
                new MataKuliah{ID = "ALG", Name = "Algoritma" },
                new MataKuliah{ID = "OOP", Name = "Object Oriented Programming"}
            };

            var nilais = new List<Nilai>
            {
                new Nilai{MahasiswaID = "A01", MataKuliahID="KAL",NilaiHuruf = "C"},
                new Nilai{MahasiswaID = "A01", MataKuliahID="ENG",NilaiHuruf = "D"},
                new Nilai{MahasiswaID = "A01", MataKuliahID="ALG",NilaiHuruf = "B"},
                new Nilai{MahasiswaID = "A01", MataKuliahID="OOP",NilaiHuruf = "C"},

                new Nilai{MahasiswaID = "B01", MataKuliahID="KAL",NilaiHuruf = "B"},
                new Nilai{MahasiswaID = "B01", MataKuliahID="ENG",NilaiHuruf = "B"},
                new Nilai{MahasiswaID = "B01", MataKuliahID="ALG",NilaiHuruf = "D"},
                new Nilai{MahasiswaID = "B01", MataKuliahID="OOP",NilaiHuruf = "C"},

                new Nilai{MahasiswaID = "C01", MataKuliahID="KAL",NilaiHuruf = "A"},
                new Nilai{MahasiswaID = "C01", MataKuliahID="ENG",NilaiHuruf = "B"},
                new Nilai{MahasiswaID = "C01", MataKuliahID="ALG",NilaiHuruf = "B"},
                new Nilai{MahasiswaID = "C01", MataKuliahID="OOP",NilaiHuruf = "A"}
            };

            var report =
                from aa in nilais
                join bb in mataKuliahs on aa.MataKuliahID equals bb.ID
                join cc in mahasiswas on aa.MahasiswaID equals cc.ID
                select new
                {
                    MahasiswawID = aa.MahasiswaID,
                    MahasiswaName = cc.Name,
                    MataKuliahID = aa.MataKuliahID,
                    MataKuliahName = bb.Name,
                    Nilai = aa.NilaiHuruf
                };

            foreach(var item in report)
            {
                Console.WriteLine(item.MahasiswawID, 
                    '|', item.MahasiswaName, '|', item.MataKuliahID, 
                    '|', item.MataKuliahName, '|', item.Nilai);
            }
        }
    }
}