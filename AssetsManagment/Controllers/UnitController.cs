using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetsManagment.Controllers
{
    public class UnitController : Controller
    {
        //
        // GET: /Unit/
        public ActionResult Index()
        {
            return PartialView("UnitPartialView");
        }
	}
}