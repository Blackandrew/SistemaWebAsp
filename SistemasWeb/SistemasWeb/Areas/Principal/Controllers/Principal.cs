using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemasWeb.Areas.principal
{
    public class Principal : Controller
    {

        [Area("Principal")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
