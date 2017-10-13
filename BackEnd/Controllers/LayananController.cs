using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BackEnd.Models;
using BackEnd.BL;
using System.Web.Http.Results;

namespace BackEnd.Controllers
{
    public class LayananController : ApiController
    {
        ILayananBL _layananBL;

        public LayananController()
        {
            _layananBL = new LayananBL();
        }

        public LayananController(ILayananBL injLayananBl)
        {
            _layananBL = injLayananBl;
        }

        [HttpGet]
        public LayananModel GetById(string id)
        {
            LayananModel retVal = _layananBL.GetById(id);
            return retVal;
        }

        [HttpGet]
        public List<LayananModel> ListData(LayananListDataType listType)
        {
            List<LayananModel> retVal = _layananBL.ListData(listType);
            return retVal;
        }

        [HttpPost]
        public IHttpActionResult Save(LayananModel layanan)
        {
            try
            {
                _layananBL.Save(layanan);
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
                _layananBL.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
