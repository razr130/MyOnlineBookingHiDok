using BackEnd.BL;
using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BackEnd.Controllers
{
    public class DokterController : ApiController
    {
        IDokterBL _dokterBL;

        public DokterController()
        {
            _dokterBL = new DokterBL();
        }

        public DokterController(IDokterBL injDokterBl)
        {
            _dokterBL = injDokterBl;
        }

        [HttpGet]
        public DokterModel GetById(string id)
        {
            DokterModel retVal = _dokterBL.GetById(id);
            return retVal;
        }

        [HttpGet]
        public List<DokterModel> ListData()
        {
            List<DokterModel> retVal = _dokterBL.ListData();
            return retVal;
        }

        [HttpGet]
        public List<DokterModel> ListData(string kodeLayanan)
        {
            List<DokterModel> retVal = _dokterBL.ListData(kodeLayanan);
            return retVal;
        }

        [HttpPost]
        public IHttpActionResult Save(DokterModel layanan)
        {
            try
            {
                _dokterBL.Save(layanan);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(string id)
        {
            try
            {
                _dokterBL.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }    }
}
