using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Models
{
    public partial class DummyLayananModels
    {
        [Required]
        public string Kode { get; set; }

        [Required]
        public string Nama { get; set; }
        public bool IsPopular { get; set; }
        public IEnumerable<SelectListItem> Namas { get; set; }
    }
}