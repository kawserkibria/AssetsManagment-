using AssetsManagment.Models;
using Dutility;
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
    public class TreeController : Controller
    {
        private const string mcGROUP_PREFIX = "G_";
        private const string mcLEDGER_PREFIX = "L_";

        List<StockNode> StockNodelist = new List<StockNode>();
        #region "Not Used"
        public DataSet mFillStockTreeGroupLevelNew()
        {
            string strSQL = "";
            SqlCommand cmdInsert = new SqlCommand();
            DataSet ds = new DataSet();
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == System.Data.ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                try
                {
                    gcnMain.Open();
                    strSQL = "SELECT STOCKGROUP_NAME FROM INV_STOCKGROUP ";
                    strSQL = strSQL + "WHERE STOCKGROUP_LEVEL = 1";
                    strSQL = strSQL + "ORDER BY STOCKGROUP_SERIAL ";
                    SqlDataAdapter da = new SqlDataAdapter(strSQL, gcnMain);
                    da.Fill(ds);
                    gcnMain.Close();
                    return ds;
                }
                catch (Exception ex)
                {
                    return ds;
                }
            }
        }
        public List<StockItem> mFillStockTreeGroupLevel(string strDeComID)
        {
            string strSQL;
            SqlDataReader drGetGroup;
            List<StockItem> oogrp = new List<StockItem>();
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            strSQL = "SELECT STOCKGROUP_NAME FROM INV_STOCKGROUP ";
            strSQL = strSQL + "WHERE STOCKGROUP_LEVEL = 1";
            strSQL = strSQL + "ORDER BY STOCKGROUP_SERIAL ";
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
                    StockItem ogrp = new StockItem();
                    ogrp.strItemGroup = drGetGroup["STOCKGROUP_NAME"].ToString();
                    oogrp.Add(ogrp);
                }
                drGetGroup.Close();
                gcnMain.Dispose();
                return oogrp;

            }
        }
        #endregion
        public List<Treenode> mloadAddStockItem(string vstrRoot)
        {
            string strSQL;
            SqlDataReader drGetGroup;
          
            List<Treenode> oogrp = new List<Treenode>();
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            strSQL = "SELECT STOCKITEM_NAME,STOCKITEM_ALIAS FROM INV_STOCKITEM ";
            strSQL = strSQL + "WHERE STOCKGROUP_NAME = '" + vstrRoot + "' ";

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
                    Treenode ogrp = new Treenode();
                    ogrp.id = drGetGroup["STOCKITEM_NAME"].ToString();
                    ogrp.text = drGetGroup["STOCKITEM_NAME"].ToString();
                    ogrp.type = "second";
                    oogrp.Add(ogrp);
                }
                drGetGroup.Close();
                gcnMain.Dispose();
                return oogrp;

            }
        }

       
        public List<Treenode> ShowTreeNode(String Parent)
        {

            List<Treenode> menuList = new List<Treenode>();
            string strSQL;
            SqlDataReader drGetGroup;
            List<Treenode> oogrp = new List<Treenode>();
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();
                if (Parent == "")
                {
                    strSQL = "SELECT STOCKGROUP_NAME FROM INV_STOCKGROUP ";
                    strSQL = strSQL + "WHERE STOCKGROUP_LEVEL = 1";
                    strSQL = strSQL + "ORDER BY STOCKGROUP_SERIAL ";
                }
                else
                {
                    strSQL = "SELECT STOCKGROUP_NAME FROM INV_STOCKGROUP ";
                    strSQL = strSQL + " WHERE STOCKGROUP_PARENT = '" + Parent + "'";
                    strSQL = strSQL + " and STOCKGROUP_LEVEL <> 1 ";
                    strSQL = strSQL + " ORDER BY STOCKGROUP_SERIAL";

                }
                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                drGetGroup = cmd.ExecuteReader();
                while (drGetGroup.Read())
                {
                    Treenode ogrp = new Treenode();
                    ogrp.id = drGetGroup["STOCKGROUP_NAME"].ToString();
                    ogrp.text = drGetGroup["STOCKGROUP_NAME"].ToString();
                    ogrp.type = "root";
                    menuList.Add(ogrp);
                }
                drGetGroup.Close();
                gcnMain.Dispose();
                if (menuList.Count > 0)
                {
                    for (int i = 0; i < menuList.Count; i++)
                    {
                        menuList[i].children = ShowTreeNode(menuList[i].text);
                    }
                }

                else
                {
                    if (Parent != "")
                    {
                        List<StockNode> itemList = new List<StockNode>();
                        menuList = mloadAddStockItem(Parent);
                    }
                }

                return menuList;

            }


        }
     
        public ActionResult Index()
        {
            return PartialView();
        }
            

        public JsonResult  GetJsTree3Data()
        {
            List<Treenode> list = ShowTreeNode("");
            return Json(list, JsonRequestBehavior.AllowGet);
        
        }

    }



    




}
