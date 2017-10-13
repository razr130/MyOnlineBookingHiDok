using BackEnd.Dal;
using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BackEnd.Controllers
{
    public class BookingResult : BookingModel
    {
        public string ErrorMsg { get; set; }
    }

    public class BookingController : ApiController
    {
        [HttpPost]
        public BookingModel Save(BookingModel booking)
        {
            if (booking.NamaPasien.Trim() == "")
                return new BookingResult { ErrorMsg = "Invalid Booking Data" };
            else
            {
                BookingDal bookingDal = new BookingDal();
                bookingDal.Insert(booking);
                List<BookingModel> bookings = bookingDal.ListData();
                BookingModel bookingRetVal = bookings.OrderByDescending(x => x.KodeTrs).First();
                //bookingRetVal.BookingCode = bookingRetVal.KodeTrs;
                bookingRetVal.NamaDokter = booking.NamaDokter;
                bookingRetVal.NamaLayanan = booking.NamaLayanan;
                return bookingRetVal;
            }
        }

        [HttpGet]
        public List<BookingModel> ListData()
        {
            BookingDal bookingDal = new BookingDal();
            return bookingDal.ListData();
        }
    }
}
