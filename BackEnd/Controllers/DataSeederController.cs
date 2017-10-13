using BackEnd.DataSeeder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BackEnd.Controllers
{
    public class DataSeederController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Seed()
        {
            var layananSeeder = new LayananDataSeeder();
            layananSeeder.Seed();

            var dokterSeeder = new DokterSeeder();
            dokterSeeder.Seed();

            var jadwalHariSeeder = new JadwalHariSeeder();
            jadwalHariSeeder.Seed();

            return Ok();
        }
    }
}
