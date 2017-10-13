using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrontEnd.Models;
using System.Net.Http;


namespace FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        List<SelectListItem> items = new List<SelectListItem>();


        public ActionResult Index()
        {
            


            IEnumerable<DummyLayananModels> layanans = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://dev.smart-ics.com:212/myonlinebooking/Api/");
                //HTTP GET
                var responseTask = client.GetAsync("Layanan/ListData?listType=0");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<DummyLayananModels>>();
                    readTask.Wait();

                    layanans = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    layanans = Enumerable.Empty<DummyLayananModels>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }   
            }
            return View(layanans);
        }

       

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}