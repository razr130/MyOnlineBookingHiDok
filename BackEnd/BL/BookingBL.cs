using BackEnd.Dal;
using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEnd.BL
{
    public interface IBookingBL
    {
        void Save(BookingModel booking);
        void Delete(string id);
        BookingModel GetById(string id);
        List<BookingModel> ListData();
    }

    public class BookingBL : IBookingBL
    {
        private IBookingDal _bookingDal;

        public BookingBL()
        {
            _bookingDal = new BookingDal();
        }

        public BookingBL(IBookingDal injBookingDal)
        {
            _bookingDal = injBookingDal;
        }

        public void Delete(string id)
        {
            _bookingDal.Delete(id);
        }

        public BookingModel GetById(string id)
        {
            return _bookingDal.GetById(id);
        }

        public List<BookingModel> ListData()
        {
            return _bookingDal.ListData();
        }

        public void Save(BookingModel booking)
        {
            // validasi
        }
    }
}