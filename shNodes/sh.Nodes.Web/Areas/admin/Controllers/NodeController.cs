using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace sh.Nodes.Web.Areas.admin.Controllers
{
    public class NodeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}