using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetsManagment.Controllers
{
    public class SupplierInfoController : Controller
    {
        //
        // GET: /SupplierInfo/
        public ActionResult Index()
        {
            return PartialView("SupplierPartitalView");
        }
	}
}