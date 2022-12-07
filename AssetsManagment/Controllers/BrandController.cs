using AssetsManagment.Models;
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
        public ActionResult Index()
        {
           //dddd
            return PartialView("BrandPartialView");
        }

        public ActionResult create(Brand regObject)
        {

            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }

                try
                {
                    gcnMain.Open();
                    SqlDataReader dr;

                    string strSQL;

                    SqlCommand cmdInsert = new SqlCommand();

                    SqlTransaction myTrans;

                    myTrans = gcnMain.BeginTransaction();

                    cmdInsert.Connection = gcnMain;
                    cmdInsert.Transaction = myTrans;

                    strSQL = "INSERT INTO BRAND(BRAND_NAME,";
                    strSQL = strSQL + "STATUS) ";

                    strSQL = strSQL + "VALUES('" + regObject.BRAND_NAME.Replace("'", "''") + "', ";
                    strSQL = strSQL + "" + 1 + " ";

                    strSQL = strSQL + ")";
                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();


                    cmdInsert.Transaction.Commit();
                    gcnMain.Close();

                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json("Save fail !!", JsonRequestBehavior.AllowGet);
                }
                finally
                {
                    gcnMain.Close();
                }
            }
        }

        [HttpPost]
        public ActionResult showGrid()
        {

            Response response = new Response();


            var datas = mFillBrandList("");

            //response.data = datas;

            return Json(datas, JsonRequestBehavior.AllowGet);
        }

        public BrandResponse mFillBrandList(string strDeComID)
        {
            string strSQL;
            SqlDataReader drGetGroup;
            List<Brand> oogrp = new List<Brand>();
            BrandResponse sp = new BrandResponse();
            Response response = new Response();



            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            strSQL = "SELECT BRAND_ID,BRAND_NAME,STATUS FROM BRAND ";
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }





                gcnMain.Open();

                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);

                drGetGroup = cmd.ExecuteReader();
                while (drGetGroup.Read())
                {

                    Brand ogrp = new Brand();
                    ogrp.intBrandID = Convert.ToInt64(drGetGroup["BRAND_ID"].ToString());
                    ogrp.BRAND_NAME = drGetGroup["BRAND_NAME"].ToString();
                    ogrp.B_STATUS = Convert.ToInt32(drGetGroup["STATUS"].ToString());
                    oogrp.Add(ogrp);

                }
                drGetGroup.Close();
                gcnMain.Dispose();
                sp.data = oogrp;



                return sp;

            }
        }
        public ActionResult updateData(Brand regObject)
        {
            string strSQL;
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }

                try
                {
                    gcnMain.Open();
                    SqlDataReader dr;
                    //strBranchId = Utility.gstrGetBranchID(strDeComID, vstrBranch);
                    //regObject.strBranch = "0001";
                    SqlCommand cmdInsert = new SqlCommand();
                    SqlTransaction myTrans;
                    myTrans = gcnMain.BeginTransaction();
                    cmdInsert.Connection = gcnMain;
                    cmdInsert.Transaction = myTrans;



                    //strSQL = "INSERT INTO BRAND(BRAND_NAME,";
                    //strSQL = strSQL + "STATUS) ";
                    strSQL = "UPDATE BRAND SET BRAND_NAME = '" + regObject.BRAND_NAME + "',";
                    strSQL = strSQL + "STATUS = " + regObject.B_STATUS + " ";
                    strSQL = strSQL + "WHERE BRAND_ID = " + regObject.intBrandID + " ";
                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();


                    cmdInsert.Transaction.Commit();
                    gcnMain.Close();
                    return Json("Ok", JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json("Update fail !!", JsonRequestBehavior.AllowGet);
                }
                finally
                {
                    gcnMain.Close();
                }
            }


        }


        [HttpPost]
        public ActionResult DeleteB(int id)
        {
            string strSQL;
            string strGroupName = "", strResponse = "", strDefaultName = "";
            bool blnDelete = false;
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                try
                {
                    gcnMain.Open();

                    SqlDataReader rsGet;
                    SqlCommand cmdDelete = new SqlCommand();
                    SqlTransaction myTrans;
                    myTrans = gcnMain.BeginTransaction();
                    cmdDelete.Connection = gcnMain;
                    cmdDelete.Transaction = myTrans;


                    strSQL = "DELETE FROM BRAND ";
                    strSQL = strSQL + "WHERE BRAND_ID = " + id + "";
                    cmdDelete.CommandText = strSQL;
                    cmdDelete.ExecuteNonQuery();
                    strResponse = "Deleted...";

                    cmdDelete.Transaction.Commit();
                    gcnMain.Close();

                    return Json("OK", JsonRequestBehavior.AllowGet);
                }

                catch (Exception ex)
                {
                    strResponse = "Transaction Found Cannot Delete...";
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }
                finally
                {
                    gcnMain.Close();
                }

            }




        }
    }
}