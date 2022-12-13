using AssetsManagment.Models;
using AssetsManagment.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace AssetsManagment.Controllers
{
    public class BrandController : Controller
    {
        //
        // GET: /Brand/

        RepositoryAsst objRepositoryAsst = new RepositoryAsst();
        public ActionResult Index()
        {
           //dddd
            Branch obj = new Branch();
            obj.strBranchID = "0001";
            ViewBag.groupList = objRepositoryAsst.groupList(obj);
            return PartialView("BrandPartialView");
        }
        public ActionResult InsertBrand(Brand obj)
        {
            string oogrp = objRepositoryAsst.InsertBrand(obj);
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
        public ActionResult UpdateBrand(Brand obj)
        {
            string oogrp = objRepositoryAsst.UpdateData(obj);
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
        
        public ActionResult mFillBrandList()
        {
            List<Brand> oogrp = new List<Brand>();
            oogrp = objRepositoryAsst.mFillBrandList("0001");
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

        public ActionResult DeleteBrand(Brand obj)
        {
            string oogrp = objRepositoryAsst.DeleteBrand(obj);
            return new JsonResult() { Data = oogrp, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }
     

    }
}