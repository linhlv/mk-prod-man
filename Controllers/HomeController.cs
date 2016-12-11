using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kenrapid.CRM.Web.Data;
using StructureMap;
using Kenrapid.CRM.Web.Infrastructure;

namespace Kenrapid.CRM.Web.Controllers
{
    public class HomeController : KenrapidControllerBase
	{
		public HomeController()
		{
		}

		public ActionResult Index()
		{
		    return RedirectToAction("Index", "Product");
		}

        public ActionResult Root()
        {
            return View();
        }
        
        public ActionResult Nine()
        {
            return View();
        }
	}
}