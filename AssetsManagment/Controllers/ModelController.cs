using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssetsManagment.Controllers
{
    public class ModelController : Controller
    {
        //
        // GET: /Model/
        public ActionResult Index()
        {
            return PartialView("ModalPartialView");
        }
	}
}