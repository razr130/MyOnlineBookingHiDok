using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BackEnd.Models;
using BackEnd.Dal;
using BackEnd.BL;

namespace BackEnd.Controllers
{
    public class TimeSlotJadwalController : ApiController
    {
        JadwalTglDal _timeSlotJadwalHariDal;

        public TimeSlotJadwalController()
        {
            _timeSlotJadwalHariDal = new JadwalTglDal();
        }

        [HttpGet]
        public List<BookingSumHeaderDetailModel> GetTimeSlot(string kodeLayanan, string tanggal)
        {

            List<BookingSumHeaderDetailModel> retVal = null;
            if (kodeLayanan == "RJ007")
            {
                SampleData samp = new SampleData();
                retVal = samp.GetData();
            }
            else
            {
                retVal = GetData(kodeLayanan);
            }
            return retVal;            
            
        }

        private List<BookingSumHeaderDetailModel> GetData(string kodeLayanan)
        {
            LayananBL layananBL = new LayananBL();
            DokterBL dokterBL = new DokterBL();
            LayananModel layanan = layananBL.GetById(kodeLayanan);
            List<DokterModel> dokters = dokterBL.ListData(kodeLayanan);

            List<BookingSumHeaderDetailModel> retVal = new List<BookingSumHeaderDetailModel>();

            foreach (var item in dokters)
            {
                var item2 = new BookingSumHeaderDetailModel
                {
                    KodeDokter = item.Kode,
                    NamaDokter = item.Nama,
                    FilePhoto = item.PhotoFileName,
                    KodeLayanan = kodeLayanan,
                    NamaLayanan = layanan.Nama,
                    Details = new List<BookingSumHeaderDetil2Model>()
                };
                retVal.Add(item2);

            }
            return retVal;
        }
    }
}
