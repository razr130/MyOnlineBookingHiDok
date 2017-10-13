using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrontEnd.ViewModels;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Xml.Linq;

namespace FrontEnd.Controllers
{
    public class DummyKonfirmasiBookingController : Controller
    {
        // GET: DummyKonfirmasiBooking
        public ActionResult Index(string jam, string tanggal, string namadokter, string namalayanan, string kodedokter, string kodelayanan, string foto)
        {
            ViewBag.jam = jam;
            ViewBag.tanggal = tanggal;
            ViewBag.namadokter = namadokter;
            ViewBag.namalayanan = namalayanan;
            ViewBag.kodedokter = kodedokter;
            ViewBag.kodelayanan = kodelayanan;
            ViewBag.foto = foto;
            
            return View();
        }
        public ActionResult Selesai()
        {
            ViewBag.kodetrsr = TempData["kodetrsr"].ToString();
            ViewBag.tgltrsr = TempData["tgltrsr"].ToString();
            ViewBag.jamtrsr = TempData["jamtrsr"].ToString();
            ViewBag.kodedokterr = TempData["kodedokterr"].ToString();
            ViewBag.namadokterr = TempData["namadokterr"].ToString();
            ViewBag.kodelayananr = TempData["kodelayananr"].ToString();
            ViewBag.namalayananr = TempData["namalayananr"].ToString();
            ViewBag.tgljadwalr = TempData["tgljadwalr"].ToString();
            ViewBag.jamjadwalr = TempData["jamjadwalr"].ToString();
            ViewBag.kodemrr = TempData["kodemrr"].ToString();
            ViewBag.namapasienr = TempData["namapasienr"].ToString();
            ViewBag.tgllahirr = TempData["tgllahirr"].ToString();
            ViewBag.notelpr = TempData["notelpr"].ToString();
            ViewBag.emailr = TempData["emailr"].ToString();
            ViewBag.bookingcoder = TempData["bookingcoder"].ToString();


            return View();
        }

        [Authorize]
        public ActionResult CreateBooking(string jam,string namadokter, string tanggal, string kodedokter, string kodelayanan, string namalayanan)
        {
           
            TempData["jam"] = jam;
            TempData["tanggal"] = tanggal;
            TempData["kodedokter"] = kodedokter;
            TempData["kodelayanan"] = kodelayanan;
            TempData["namadokter"] = namadokter;
            TempData["namalayanan"] = namalayanan;
            return View();
           
        }

        [HttpPost]
        
        [ValidateAntiForgeryToken]
        public ActionResult CreateBooking(KonfirmasiBookingViewModels booking, string tanggal)
        {
            booking.KodeTrs = "";
            booking.TglTrs = DateTime.Now.Date.ToString("dd-MM-yyyy");
            booking.JamTrs = DateTime.Now.ToString("HH:mm:ss");
            booking.KodeDokter = TempData["kodedokter"].ToString();
            booking.NamaDokter = TempData["namadokter"].ToString();
            booking.KodeLayanan = TempData["kodelayanan"].ToString();
            booking.NamaLayanan = TempData["namalayanan"].ToString();
            booking.TglJadwal = TempData["tanggal"].ToString();
            booking.JamJadwal = TempData["jam"].ToString();
            booking.KodeMR = "";
            booking.TglLahir = tanggal;
            booking.NoTelp = "098098";
            if (Session["username"] == null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    Session["username"] = User.Identity.Name;
                }
                else
                {
                    
                    Response.Redirect("~/Login.aspx");
                }
            }
            booking.Email = Session["username"].ToString();


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://dev.smart-ics.com:212/MyOnlineBooking/Api/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<KonfirmasiBookingViewModels>("Booking/Save", booking);
                postTask.Wait();

                var result = postTask.Result;
                var jsonTask = result.Content.ReadAsAsync<KonfirmasiBookingViewModels>();
                if (result.IsSuccessStatusCode)
                {
                    TempData["kodetrsr"] = jsonTask.Result.KodeTrs;
                    TempData["tgltrsr"] = jsonTask.Result.TglTrs;
                    TempData["jamtrsr"] = jsonTask.Result.JamTrs;
                    TempData["kodedokterr"] = jsonTask.Result.KodeDokter;
                    TempData["namadokterr"] = jsonTask.Result.NamaDokter;
                    TempData["kodelayananr"] = jsonTask.Result.KodeLayanan;
                    TempData["namalayananr"] = jsonTask.Result.NamaLayanan;
                    TempData["tgljadwalr"] = jsonTask.Result.TglJadwal;
                    TempData["jamjadwalr"] = jsonTask.Result.JamJadwal;
                    TempData["kodemrr"] = jsonTask.Result.KodeMR;
                    TempData["namapasienr"] = jsonTask.Result.NamaPasien;
                    TempData["tgllahirr"] = jsonTask.Result.TglLahir;
                    TempData["notelpr"] = jsonTask.Result.NoTelp;
                    TempData["emailr"] = jsonTask.Result.Email;
                    TempData["bookingcoder"] = jsonTask.Result.BookingCode;

                    return RedirectToAction("Selesai", "DummyKonfirmasiBooking", new { area = "" });
                }
            }

            ModelState.AddModelError(string.Empty, "error");

            return View(booking);
        }

            
        }
    }
