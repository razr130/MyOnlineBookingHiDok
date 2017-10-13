using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEnd.Models
{
    public class BookingSumHeaderDetailModel
    {
        public string KodeDokter { get; set; }
        public string NamaDokter { get; set; }
        public string FilePhoto { get; set; }
        public string KodeLayanan { get; set; }
        public string NamaLayanan { get; set; }
        public List<BookingSumHeaderDetil2Model> Details { get; set; }
    }

    public class BookingSumHeaderDetil2Model
    {
        public string TglJadwal { get; set; }
        public string Jam { get; set; }
        public int Durasi { get; set; }
        public int Max { get; set; }
        public int Booked { get; set; }
    }

    public class SampleData
    {
        public List<BookingSumHeaderDetailModel> GetData()
        {
            List<BookingSumHeaderDetailModel> retVal = new List<BookingSumHeaderDetailModel>();

            var item = new BookingSumHeaderDetailModel
            {
                KodeDokter = "DK001",
                NamaDokter = "Agus Supartoto, Sp.M",
                FilePhoto = "AgusSupartoto.jpg",
                KodeLayanan = "RJ007",
                NamaLayanan = "Mata",
                Details = new List<BookingSumHeaderDetil2Model>()
            };
            BookingSumHeaderDetil2Model detil = new BookingSumHeaderDetil2Model
            {
                TglJadwal = "10-09-2017",
                Jam = "08:00",
                Durasi = 60,
                Max = 4,
                Booked = 4
            };
            item.Details.Add(detil);

            item.Details.Add(new BookingSumHeaderDetil2Model
            {
                TglJadwal = "10-09-2017",
                Jam = "09:00",
                Durasi = 60,
                Max = 4,
                Booked = 3
            });
            item.Details.Add(new BookingSumHeaderDetil2Model
            {
                TglJadwal = "10-09-2017",
                Jam = "10:00",
                Durasi = 60,
                Max = 4,
                Booked = 0
            });
            item.Details.Add(new BookingSumHeaderDetil2Model
            {
                TglJadwal = "10-09-2017",
                Jam = "11:00",
                Durasi = 30,
                Max = 4,
                Booked = 0
            });
            item.Details.Add(new BookingSumHeaderDetil2Model
            {
                TglJadwal = "11-09-2017",
                Jam = "08:00",
                Durasi = 60,
                Max = 4,
                Booked = 2
            });
            item.Details.Add(new BookingSumHeaderDetil2Model
            {
                TglJadwal = "11-09-2017",
                Jam = "09:00",
                Durasi = 60,
                Max = 4,
                Booked = 0
            });
            item.Details.Add(new BookingSumHeaderDetil2Model
            {
                TglJadwal = "11-09-2017",
                Jam = "10:00",
                Durasi = 60,
                Max = 4,
                Booked = 0
            });

            item.Details.Add(new BookingSumHeaderDetil2Model
            {
                TglJadwal = "12-09-2017",
                Jam = "08:00",
                Durasi = 60,
                Max = 4,
                Booked = 2
            });
            item.Details.Add(new BookingSumHeaderDetil2Model
            {
                TglJadwal = "12-09-2017",
                Jam = "09:00",
                Durasi = 60,
                Max = 4,
                Booked = 0
            });
            item.Details.Add(new BookingSumHeaderDetil2Model
            {
                TglJadwal = "12-09-2017",
                Jam = "10:00",
                Durasi = 60,
                Max = 4,
                Booked = 0
            });

            item.Details.Add(new BookingSumHeaderDetil2Model
            {
                TglJadwal = "14-09-2017",
                Jam = "08:00",
                Durasi = 60,
                Max = 4,
                Booked = 2
            });
            item.Details.Add(new BookingSumHeaderDetil2Model
            {
                TglJadwal = "14-09-2017",
                Jam = "09:00",
                Durasi = 60,
                Max = 4,
                Booked = 0
            });
            item.Details.Add(new BookingSumHeaderDetil2Model
            {
                TglJadwal = "14-09-2017",
                Jam = "10:00",
                Durasi = 60,
                Max = 4,
                Booked = 0
            });

            item.Details.Add(new BookingSumHeaderDetil2Model
            {
                TglJadwal = "15-09-2017",
                Jam = "08:00",
                Durasi = 60,
                Max = 4,
                Booked = 2
            });
            item.Details.Add(new BookingSumHeaderDetil2Model
            {
                TglJadwal = "15-09-2017",
                Jam = "09:00",
                Durasi = 60,
                Max = 4,
                Booked = 0
            });
            item.Details.Add(new BookingSumHeaderDetil2Model
            {
                TglJadwal = "15-09-2017",
                Jam = "10:00",
                Durasi = 60,
                Max = 4,
                Booked = 0
            });

            retVal.Add(item);

            item = new BookingSumHeaderDetailModel
            {
                KodeDokter = "DK002",
                NamaDokter = "Priskila Dwi Erika, Sp.M",
                FilePhoto = "PriskilaDwiErika.jpg",
                KodeLayanan = "LY001",
                NamaLayanan = "Poli Mata",
                Details = new List<BookingSumHeaderDetil2Model>()
            };
            item.Details.Add(new BookingSumHeaderDetil2Model
            {
                TglJadwal = "10-09-2017",
                Jam = "13:00",
                Durasi = 60,
                Max = 4,
                Booked = 0
            });
            item.Details.Add(new BookingSumHeaderDetil2Model
            {
                TglJadwal = "10-09-2017",
                Jam = "14:00",
                Durasi = 60,
                Max = 4,
                Booked = 1
            });
            item.Details.Add(new BookingSumHeaderDetil2Model
            {
                TglJadwal = "10-09-2017",
                Jam = "15:00",
                Durasi = 60,
                Max = 4,
                Booked = 0
            });
            item.Details.Add(new BookingSumHeaderDetil2Model
            {
                TglJadwal = "12-09-2017",
                Jam = "08:00",
                Durasi = 60,
                Max = 4,
                Booked = 0
            });
            item.Details.Add(new BookingSumHeaderDetil2Model
            {
                TglJadwal = "12-09-2017",
                Jam = "09:00",
                Durasi = 60,
                Max = 4,
                Booked = 2
            });
            item.Details.Add(new BookingSumHeaderDetil2Model
            {
                TglJadwal = "13-09-2017",
                Jam = "08:00",
                Durasi = 60,
                Max = 4,
                Booked = 0
            });
            item.Details.Add(new BookingSumHeaderDetil2Model
            {
                TglJadwal = "13-09-2017",
                Jam = "09:00",
                Durasi = 60,
                Max = 4,
                Booked = 0
            });
            retVal.Add(item);
            return retVal;
        }
    }
}