using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrontEnd.ViewModels;
using System.Net.Http;
using FrontEnd.Models;
using System.Threading;
using System.Globalization;

namespace FrontEnd.Controllers
{
    public class DummyJadwalDokterController : Controller
    {
        // GET: DummyJadwalDokter
        List<SelectListItem> items = new List<SelectListItem>();

        public ActionResult Index(string namalayanan, string kodelayanan, string tanggal)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("id-ID");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("id-ID");
            IEnumerable<JadwalViewModels> layanans = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://dev.smart-ics.com:212/MyOnlineBooking/api/");
                //HTTP GET
                var responseTask = client.GetAsync("TimeSlotJadwal/GetTimeSlot?kodeLayanan=" + kodelayanan +"&tanggal=" + tanggal);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<JadwalViewModels>>();
                    readTask.Wait();

                    layanans = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    layanans = Enumerable.Empty<JadwalViewModels>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            ViewBag.NamaLayanan = namalayanan;
            return View(layanans);
        }
    }
}