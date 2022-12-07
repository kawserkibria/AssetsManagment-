using AssetsManagment.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace AssetsManagment.Controllers.setting
{
    public class DepartmentController : Controller
    {
        //
        // GET: /Department/
        public ActionResult Index()
        {
            return PartialView("departmentView");
        }


        public ActionResult create(Department regObject)
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

                    strSQL = "INSERT INTO HRS_DEPARTMENT(DEPARTMENT_NAME,";
                    strSQL = strSQL + "DEP_STATUS) ";

                    strSQL = strSQL + "VALUES('" + regObject.department_name.Replace("'", "''") + "', ";
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

        public ActionResult updateDepartment(Department regObject)
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
                    strSQL = "UPDATE HRS_DEPARTMENT SET DEPARTMENT_NAME = '" + regObject.department_name + "',";
                    strSQL = strSQL + "DEP_STATUS = '1' ";
                    strSQL = strSQL + "WHERE DEPARTMENT_CODE = " + regObject.department_code + " ";
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
        public ActionResult DeleteDepartment(int id)
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


                    strSQL = "DELETE FROM HRS_DEPARTMENT ";
                    strSQL = strSQL + "WHERE DEPARTMENT_CODE = " + id + "";
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


        public JsonResult showGrid()
        {

            string strSQL;
            SqlDataReader drGetGroup;
            List<Department> obList = new List<Department>();
            LocationResponse response = new LocationResponse();
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            strSQL = "SELECT DEPARTMENT_NAME,DEP_STATUS,DEPARTMENT_CODE FROM HRS_DEPARTMENT ";
         
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();

                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                //drGetGroup = cmd.ExecuteReader();
                //while (drGetGroup.Read())
                //{
                //    Department objects = new Department();
                //    objects.department_name = drGetGroup["DEPARTMENT_NAME"].ToString();
                //    objects.department_code = Convert.ToInt16(drGetGroup["DEPARTMENT_CODE"].ToString());
                //    objects.dep_status = Convert.ToInt16(drGetGroup["DEP_STATUS"].ToString());


                //    obList.Add(objects);
                //}
                //drGetGroup.Close();
                gcnMain.Dispose();
                //response.data = obList;
                return Json(obList, JsonRequestBehavior.AllowGet);
            }

        }
	}
}