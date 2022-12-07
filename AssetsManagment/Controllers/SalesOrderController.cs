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
    public class SalesOrderController : Controller
    {
        //
        // GET: /SalesOrder/
        public ActionResult Index()
        {

            ViewBag.BranchName = mGetBranch();
            return View();
        }



        public List<Branch> mGetBranch()
        {

            string strSQL;
            SqlDataReader drGetGroup;
            List<Branch> oogrp = new List<Branch>();
            BranchRespons response = new BranchRespons();
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            strSQL = "SELECT BRANCH_NAME from ACC_BRANCH ORDER BY BRANCH_NAME ";

            //if (searchTerm != null)
            //{
            //    strSQL = "SELECT GODOWNS_SERIAL,GODOWNS_NAME,BRANCH_ID,GODOWNS_DEFAULT,GODOWNS_PHONE,GODOWNS_ADDRESS1 FROM INV_GODOWNS WHERE GODOWNS_NAME=" + "'" + searchTerm + "'";
            //}
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
                    Branch ogrp = new Branch();
                    //ogrp.lngSlNo = Convert.ToInt64(drGetGroup["GODOWNS_SERIAL"].ToString());
                    //ogrp.strLocation = drGetGroup["GODOWNS_NAME"].ToString();
                    //ogrp.strPhone = drGetGroup["GODOWNS_PHONE"].ToString();
                    //ogrp.strAddres1 = drGetGroup["GODOWNS_ADDRESS1"].ToString();

                    if (drGetGroup["BRANCH_NAME"].ToString() != "")
                    {
                        //ogrp.strBranch = Utility.gstrGetBranchName("0001", drGetGroup["BRANCH_ID"].ToString());
                        ogrp.strBranch = drGetGroup["BRANCH_NAME"].ToString();
                    }
                    else
                    {
                        ogrp.strBranch = "";
                    }
                    //ogrp.lngDefault = Convert.ToInt32(drGetGroup["GODOWNS_DEFAULT"]);
                    oogrp.Add(ogrp);
                }
                drGetGroup.Close();
                gcnMain.Dispose();
                return oogrp;
            }

        }

        public JsonResult mGetLocation(string strBranchName)
        {

            string strSQL;
            SqlDataReader drGetGroup;
            List<Location> oogrp = new List<Location>();
            LocationResponse response = new LocationResponse();
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;


            strSQL = "SELECT GODOWNS_SERIAL,GODOWNS_NAME,BRANCH_ID,GODOWNS_DEFAULT,GODOWNS_PHONE,GODOWNS_ADDRESS1 FROM INV_GODOWNS  WHERE INV_GODOWNS.BRANCH_ID='0001'AND INV_GODOWNS.SECTION_STATUS=0 and GODOWNS_NAME IN('Main Location')  ORDER BY GODOWNS_NAME ";
        
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
                    Location ogrp = new Location();
                    ogrp.lngSlNo = Convert.ToInt64(drGetGroup["GODOWNS_SERIAL"].ToString());
                    ogrp.strLocation = drGetGroup["GODOWNS_NAME"].ToString();
                    ogrp.strPhone = drGetGroup["GODOWNS_PHONE"].ToString();
                    ogrp.strAddres1 = drGetGroup["GODOWNS_ADDRESS1"].ToString();

                    if (drGetGroup["BRANCH_ID"].ToString() != "")
                    {
                        ogrp.strBranch = Utility.gstrGetBranchName("0001", drGetGroup["BRANCH_ID"].ToString());
                    }
                    else
                    {
                        ogrp.strBranch = "";
                    }
                    ogrp.lngDefault = Convert.ToInt32(drGetGroup["GODOWNS_DEFAULT"]);
                    oogrp.Add(ogrp);
                }
                drGetGroup.Close();
                gcnMain.Dispose();
                response.data = oogrp;

                return Json(oogrp, JsonRequestBehavior.AllowGet);


            }

        }
     
        public JsonResult mGetMPO(string responseId)
        {

            string strSQL;
            SqlDataReader drGetGroup;
            List<Ledger> oogrp = new List<Ledger>();
            BranchRespons response = new BranchRespons();
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            strSQL = "select l.LEDGER_NAME_MERZE,l.LEDGER_NAME from ACC_LEDGER L where L.BRANCH_ID='0001' and l.LEDGER_STATUS=0 order by l.LEDGER_NAME_MERZE ";

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
                    Ledger ogrp = new Ledger();
                    //ogrp.lngSlNo = Convert.ToInt64(drGetGroup["LEDGER_NAME_MERZE"].ToString());
                    ogrp.strLedgerName = drGetGroup["LEDGER_NAME_MERZE"].ToString();
                    ogrp.strTC = drGetGroup["LEDGER_NAME"].ToString();
                    oogrp.Add(ogrp);
                }
                drGetGroup.Close();
                gcnMain.Dispose();
                response.Ledgerdata = oogrp;

                return Json(oogrp, JsonRequestBehavior.AllowGet);


            }

        }


        public JsonResult mGetCustomer(string strLedgerName)
        {
     
            string strSQL;
            SqlDataReader drGetGroup;
            List<Ledger> oogrp = new List<Ledger>();
            BranchRespons response = new BranchRespons();
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            strSQL = "SELECT L.LEDGER_NAME_MERZE,L.LEDGER_CODE,L.LEDGER_NAME from ACC_LEDGER L where l.LEDGER_GROUP=204 and l.LEDGER_REP_NAME= '" + strLedgerName + "' ORDER BY L.LEDGER_NAME_MERZE ";

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
                    Ledger ogrp = new Ledger();
                    //ogrp.lngSlNo = Convert.ToInt64(drGetGroup["LEDGER_NAME_MERZE"].ToString());
                    ogrp.strCustomerName = drGetGroup["LEDGER_NAME_MERZE"].ToString();
                    ogrp.strCustomerCode = drGetGroup["LEDGER_CODE"].ToString();
                    ogrp.strLedgerName = drGetGroup["LEDGER_NAME"].ToString();
                    oogrp.Add(ogrp);
                }
                drGetGroup.Close();
                gcnMain.Dispose();
                response.Ledgerdata = oogrp;

                return Json(oogrp, JsonRequestBehavior.AllowGet);


            }

        }


        public JsonResult mFillStockGroup(string strDeComID, string strPrefix, string vstrUserName)
        {
            string strSQL;
            SqlDataReader drGetGroup;
            List<StockItem> oogrp = new List<StockItem>();
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            strSQL = "SELECT DISTINCT INV_STOCKGROUP.STOCKGROUP_NAME  FROM INV_STOCKITEM ,INV_STOCKGROUP WHERE INV_STOCKITEM.STOCKGROUP_NAME=INV_STOCKGROUP.STOCKGROUP_NAME ";
            strSQL = strSQL + "and INV_STOCKGROUP.STOCKGROUP_PRIMARY='Finished Goods' ";
            if (vstrUserName != "")
            {
                strSQL = strSQL + "AND INV_STOCKGROUP.STOCKGROUP_NAME IN (SELECT STOCKGROUP_NAME  FROM USER_PRIVILEGES_STOCKGROUP WHERE USER_LOGIN_NAME='" + vstrUserName + "')";
            }
            if (strPrefix == "SI")
            {
                strSQL = strSQL + "and G_STATUS =0 ";
            }
            else if (strPrefix == "SIN")
            {
                strSQL = strSQL + "and G_STATUS IN (0,1) ";
            }
            else if (strPrefix == "SAMPLE")
            {
                strSQL = strSQL + "and G_STATUS IN (0,1) ";
                strSQL = strSQL + "and INV_STOCKGROUP.STOCKGROUP_NAME like '%Sample' ";
            }
            strSQL = strSQL + " Order By INV_STOCKGROUP.STOCKGROUP_NAME ASC ";
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
                //response.Ledgerdata = oogrp;

                return Json(oogrp, JsonRequestBehavior.AllowGet);
                //drGetGroup.Close();
                //gcnMain.Dispose();
                //return oogrp;

            }
        }


        public JsonResult gFillStockItemNew(string strDeComID, string vstrRoot, string vstrGodown)
        {
            string strSQL;
            SqlDataReader drGetGroup;
            List<StockItem> oogrp = new List<StockItem>();
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            strSQL = "SELECT INV_STOCKITEM.STOCKITEM_NAME,INV_STOCKITEM.STOCKITEM_BASEUNITS,SUM(INV_TRAN.INV_TRAN_QUANTITY) CLS";
            strSQL = strSQL + " FROM INV_TRAN ,INV_STOCKITEM WHERE INV_STOCKITEM.STOCKITEM_NAME =INV_TRAN.STOCKITEM_NAME ";
            if (vstrRoot != "")
            {
                strSQL = strSQL + " AND INV_STOCKITEM.STOCKGROUP_NAME = '" + vstrRoot + "' ";
            }
            strSQL = strSQL + " AND INV_TRAN.GODOWNS_NAME='" + vstrGodown + "' ";
            strSQL = strSQL + "AND INV_STOCKITEM.STOCKITEM_STATUS=0 ";
            strSQL = strSQL + "GROUP BY INV_STOCKITEM.STOCKITEM_NAME,INV_STOCKITEM.STOCKITEM_BASEUNITS  ORDER by INV_STOCKITEM.STOCKITEM_NAME ASc ";


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
                    ogrp.strItemName = drGetGroup["STOCKITEM_NAME"].ToString();
                    ogrp.strUnit = drGetGroup["STOCKITEM_BASEUNITS"].ToString();
                    ogrp.dblClsBalance = Convert.ToDouble(drGetGroup["CLS"].ToString());
                    oogrp.Add(ogrp);
                }
                drGetGroup.Close();
                gcnMain.Dispose();
                return Json(oogrp, JsonRequestBehavior.AllowGet);

            }
        }


        public JsonResult gFillStockItemRate(string strDeComID,string strItemGroup, string strItemName, string strOrderDate, int intQty,string strBranchName)
        {
            //string strSQL;
            //SqlDataReader drGetGroup;
            List<StockItem> oogrp = new List<StockItem>();
    

            double dblrate = 0;
            string strUOM = "", strPowerClass = "", strPackSize="",strSubGroup="";
            strSubGroup = Utility.mGetStockGroupFromItemGroup(strDeComID, strItemGroup);
            strUOM = Utility.gGetBaseUOM(strDeComID, strItemName);
            strPowerClass = Utility.mGetPowerClass(strDeComID, strItemName);
            strPackSize = Utility.mGetPackSize(strDeComID, strItemName);
            dblrate = Utility.gdblGetEnterpriseSalesPrice(strDeComID, strItemName, Convert.ToDateTime( strOrderDate).ToString("dd-MM-yyyy"), intQty, 0, "");
            string strBranchID = Utility.gstrGetBranchID(strDeComID, strBranchName);

            double dblbonus = Math.Round(Utility.mdblGetBonus(strDeComID, strItemName, strBranchID, intQty, Convert.ToDateTime(strOrderDate).ToString("dd-MM-yyyy")), 2);

            //double dblbonusQty = 4;
            StockItem ogrp = new StockItem();

            ogrp.strUnit = strUOM;
            ogrp.strPowerClass = strPowerClass;
            ogrp.strPackSize = strPackSize;
            ogrp.dblBranchRate = dblrate;
            ogrp.dblbonusQty = dblbonus;
            ogrp.strSubGroup_Name = strSubGroup;
            oogrp.Add(ogrp);

            return Json(ogrp, JsonRequestBehavior.AllowGet);

            //}
        }


        public JsonResult mShowMasterData()
        {
            string strUserName = "Deeplaid", strmySql = "", strFind = "", mdteVFromDate = "01-04-2021", strExpression = "", mdteVToDate = "01-08-2021", strDeComID = "0003";

            int mintVType=0;
            int intAreaStaus = 1, intSPJ = 0, intStatusCol=0;
            mintVType = (int)(Utility.VOUCHER_TYPE.vtSALES_ORDER);

            string strSQL = null;
            SqlDataReader dr;
            List<AccountsVoucher> ooAccLedger = new List<AccountsVoucher>();
            SqlCommand cmdInsert = new SqlCommand();
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();

                cmdInsert.Connection = gcnMain;
                if (strmySql == "")
                {
                    if (mintVType == 1)
                    {

                        strSQL = "SELECT ACC_COMPANY_VOUCHER.APPS_COMM_CAL,ACC_COMPANY_VOUCHER.COMP_REF_NO ,ACC_COMPANY_VOUCHER.COMP_VOUCHER_TYPE,ACC_COMPANY_VOUCHER.COMP_VOUCHER_DATE,ACC_LEDGER.LEDGER_NAME_MERZE ,ACC_LEDGER.LEDGER_NAME  ,";
                        strSQL = strSQL + "RV_VOUCHER_VIEW.LEDGER_NAME VOUCHER_REVERSE_LEDGER,  ACC_BRANCH.BRANCH_NAME, ACC_COMPANY_VOUCHER.COMP_VOUCHER_NET_AMOUNT, ACC_COMPANY_VOUCHER.COMP_VOUCHER_NET_AMOUNT, ";
                        strSQL = strSQL + " ACC_LEDGER.LEDGER_NAME_MERZE,ACC_COMPANY_VOUCHER.APP_STATUS,APPS_SI_RET,'' DIVISION,'' AREA,ACC_COMPANY_VOUCHER.COMP_VOUCHER_STATUS  ";
                        strSQL = strSQL + " FROM RV_VOUCHER_VIEW,ACC_BRANCH,ACC_LEDGER,ACC_COMPANY_VOUCHER  WHERE ACC_COMPANY_VOUCHER.COMP_REF_NO =RV_VOUCHER_VIEW.COMP_REF_NO AND ACC_LEDGER.LEDGER_NAME =ACC_COMPANY_VOUCHER.LEDGER_NAME  AND ";
                        strSQL = strSQL + " ACC_BRANCH.BRANCH_ID =ACC_COMPANY_VOUCHER.BRANCH_ID ";
                        strSQL = strSQL + " AND ACC_COMPANY_VOUCHER.COMP_VOUCHER_TYPE = " + mintVType + " ";
                        strSQL = strSQL + " AND ACC_COMPANY_VOUCHER.SP_JOURNAL= " + intSPJ + " ";
                        strSQL = strSQL + " AND ACC_COMPANY_VOUCHER.SAMPLE_STATUS=0 ";
                        if (strFind == "Voucher Number")
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER.COMP_REF_NO like '%" + strExpression + "%'";
                        }
                        else if (strFind == "Voucher Date")
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER.COMP_VOUCHER_DATE BETWEEN ";
                            strSQL = strSQL + Utility.cvtSQLDateString(mdteVFromDate) + " ";
                            strSQL = strSQL + "AND ";
                            strSQL = strSQL + " " + Utility.cvtSQLDateString(mdteVToDate) + " ";
                        }
                        else if (strFind == "Ledger Name")
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER.LEDGER_NAME = '" + strExpression + "'";
                        }
                        else if (strFind == "Branch Name")
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER.BRANCH_NAME =  '" + Utility.gstrGetBranchName(strDeComID, strExpression) + "'";
                        }
                        else if (strFind == "Amount")
                        {
                            if (strExpression != "")
                            {
                                strSQL = strSQL + "AND ACC_COMPANY_VOUCHER.COMP_VOUCHER_NET_AMOUNT like '%" + strExpression + "%'";
                            }
                        }
                        else if (strFind == "Narrations")
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER.COMP_VOUCHER_NARRATION = '" + strExpression + "'";
                        }
                        else
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER.COMP_VOUCHER_DATE BETWEEN ";
                            strSQL = strSQL + Utility.cvtSQLDateString(mdteVFromDate) + " ";
                            strSQL = strSQL + "AND ";
                            strSQL = strSQL + " " + Utility.cvtSQLDateString(mdteVToDate) + " ";
                        }
                        strSQL = strSQL + "AND ACC_COMPANY_VOUCHER.BRANCH_ID in (select BRANCH_ID from USER_PRIVILEGES_BRANCH where USER_LOGIN_NAME  ='" + strUserName + "')";
                        strSQL = strSQL + " ORDER By ACC_COMPANY_VOUCHER.COMP_REF_NO,ACC_LEDGER.TERITORRY_CODE,ACC_LEDGER.LEDGER_CODE,ACC_LEDGER.LEDGER_NAME  ";

                    }
                    else if (mintVType == 12)
                    {
                        strSQL = "SELECT ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_SERIAL ,ACC_COMPANY_VOUCHER_BRANCH_VIEW.APPS_COMM_CAL,ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_REF_NO,ACC_COMPANY_VOUCHER_BRANCH_VIEW.REF_NO,ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_TYPE,ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_DATE,";
                        strSQL = strSQL + "ACC_COMPANY_VOUCHER_BRANCH_VIEW.LEDGER_NAME , ACC_COMPANY_VOUCHER_BRANCH_VIEW.BRANCH_NAME, ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_AMOUNT, ";
                        strSQL = strSQL + "ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_NET_AMOUNT,ACC_COMPANY_VOUCHER_BRANCH_VIEW.LEDGER_CODE,ACC_COMPANY_VOUCHER_BRANCH_VIEW.LEDGER_NAME_MERZE,'' VOUCHER_REVERSE_LEDGER ";
                        strSQL = strSQL + ",APP_STATUS,APPS_SI_RET,ACC_LEDGER_Z_D_A.DIVISION,ACC_LEDGER_Z_D_A.AREA,ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_STATUS From ACC_COMPANY_VOUCHER_BRANCH_VIEW,ACC_LEDGER_Z_D_A WHERE ACC_LEDGER_Z_D_A.LEDGER_NAME =ACC_COMPANY_VOUCHER_BRANCH_VIEW.LEDGER_NAME  ";
                        strSQL = strSQL + " AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_TYPE = " + mintVType + " ";
                        strSQL = strSQL + " AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.SP_JOURNAL= " + intSPJ + " ";
                        strSQL = strSQL + " AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.SAMPLE_STATUS=0 ";

                        if (mintVType == (int)Utility.VOUCHER_TYPE.vtSALES_ORDER)
                        {
                            if ((intStatusCol != 5) && (intStatusCol != 1))
                            {
                                if (intAreaStaus == 1)
                                {
                                    strSQL = strSQL + " AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.APP_STATUS =0 ";
                                    strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_STATUS=0 ";
                                }

                            }

                        }

                        if (strFind == "Voucher Number")
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_REF_NO like '%" + strExpression + "%'";
                        }
                        else if (strFind == "Voucher Date")
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_DATE BETWEEN ";
                            strSQL = strSQL + Utility.cvtSQLDateString(mdteVFromDate) + " ";
                            strSQL = strSQL + "AND ";
                            strSQL = strSQL + " " + Utility.cvtSQLDateString(mdteVToDate) + " ";
                        }
                        else if (strFind == "Ledger Name")
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.LEDGER_NAME = '" + strExpression + "'";
                        }
                        else if (strFind == "Branch Name")
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.BRANCH_NAME =  '" + Utility.gstrGetBranchName(strDeComID, strExpression) + "'";
                        }
                        else if (strFind == "Amount")
                        {
                            if (strExpression != "")
                            {
                                strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_NET_AMOUNT like '%" + strExpression + "%'";
                            }
                        }
                        else if (strFind == "Narrations")
                        {
                            strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_NARRATION = '" + strExpression + "'";
                        }
                        else
                        {
                            if (mdteVFromDate != "")
                            {
                                strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_DATE BETWEEN ";
                                strSQL = strSQL + Utility.cvtSQLDateString(mdteVFromDate) + " ";
                                strSQL = strSQL + "AND ";
                                strSQL = strSQL + " " + Utility.cvtSQLDateString(mdteVToDate) + " ";
                            }
                        }
                        strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.BRANCH_ID in (select BRANCH_ID from USER_PRIVILEGES_BRANCH where USER_LOGIN_NAME  ='" + strUserName + "')";
                        strSQL = strSQL + " AND ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + strUserName + "')";

                        strSQL = strSQL + " AND ACC_LEDGER_Z_D_A.DIVISION in( select LEDGER_GROUP_NAME from USER_PRIVILEGES_COLOR WHERE USER_LOGIN_NAME ='" + strUserName + "')";

                        if (intStatusCol != 0)
                        {
                            if (intStatusCol == 1)
                            {
                                //bill Done
                                strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.APPS_COMM_CAL= 1  ";
                                strSQL = strSQL + "AND APP_STATUS=4  ";
                                strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_STATUS= 1 ";
                            }
                            if (intStatusCol == 2)
                            {
                                //Order Return
                                strSQL = strSQL + "AND APPS_SI_RET IN(1,2) ";
                            }
                            if (intStatusCol == 3)
                            {
                                //Commi.Cal
                                strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.APPS_COMM_CAL=1 ";
                                strSQL = strSQL + "AND APPS_SI_RET <>1 ";
                                strSQL = strSQL + "AND APP_STATUS IN(1,0)  ";
                            }
                            if (intStatusCol == 4)
                            {
                                //New order
                                strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.APPS_COMM_CAL IN(0)  ";
                                strSQL = strSQL + "AND APP_STATUS=1  ";
                                strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_STATUS= 0  ";
                            }
                            if (intStatusCol == 5)
                            {
                                //ZH Approved
                                strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.APPS_COMM_CAL= 1  ";
                                strSQL = strSQL + "AND APP_STATUS=2  ";
                                strSQL = strSQL + "AND ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_STATUS= 0  ";
                            }
                        }


                        strSQL = strSQL + " ORDER By ACC_COMPANY_VOUCHER_BRANCH_VIEW.COMP_VOUCHER_SERIAL DESC,ACC_COMPANY_VOUCHER_BRANCH_VIEW.REF_NO,ACC_COMPANY_VOUCHER_BRANCH_VIEW.TERITORRY_CODE,ACC_COMPANY_VOUCHER_BRANCH_VIEW.LEDGER_CODE,ACC_COMPANY_VOUCHER_BRANCH_VIEW.LEDGER_NAME  ";

                    }

                    else
                    {
                        strSQL = "SELECT ACC_COMPANY_VOUCHER_BRANCH_VIEW.APPS_COMM_CAL,COMP_REF_NO,REF_NO,COMP_VOUCHER_TYPE,COMP_VOUCHER_DATE,";
                        strSQL = strSQL + "LEDGER_NAME , BRANCH_NAME, COMP_VOUCHER_AMOUNT, COMP_VOUCHER_NET_AMOUNT,LEDGER_CODE,LEDGER_NAME_MERZE,'' VOUCHER_REVERSE_LEDGER ";
                        strSQL = strSQL + ",APP_STATUS,APPS_SI_RET,'' DIVISION,'' AREA,COMP_VOUCHER_STATUS  From ACC_COMPANY_VOUCHER_BRANCH_VIEW ";
                        strSQL = strSQL + "WHERE COMP_VOUCHER_TYPE = " + mintVType + " ";
                        strSQL = strSQL + " AND SP_JOURNAL= " + intSPJ + " ";
                        strSQL = strSQL + " AND SAMPLE_STATUS=0 ";
                        if (mintVType == (int)Utility.VOUCHER_TYPE.vtSALES_ORDER)
                        {
                            if (intAreaStaus == 1)
                            {
                                strSQL = strSQL + " AND APP_STATUS =0 ";
                            }
                            else
                            {
                                strSQL = strSQL + " AND APP_STATUS =1 ";
                            }
                        }
                        if (strFind == "Voucher Number")
                        {
                            strSQL = strSQL + "AND COMP_REF_NO like '%" + strExpression + "%'";
                        }
                        else if (strFind == "Voucher Date")
                        {
                            strSQL = strSQL + "AND COMP_VOUCHER_DATE BETWEEN ";
                            strSQL = strSQL + Utility.cvtSQLDateString(mdteVFromDate) + " ";
                            strSQL = strSQL + "AND ";
                            strSQL = strSQL + " " + Utility.cvtSQLDateString(mdteVToDate) + " ";
                        }
                        else if (strFind == "Ledger Name")
                        {
                            strSQL = strSQL + "AND LEDGER_NAME = '" + strExpression + "'";
                        }
                        else if (strFind == "Branch Name")
                        {
                            strSQL = strSQL + "AND BRANCH_NAME =  '" + Utility.gstrGetBranchName(strDeComID, strExpression) + "'";
                        }
                        else if (strFind == "Amount")
                        {
                            if (strExpression != "")
                            {
                                strSQL = strSQL + "AND COMP_VOUCHER_NET_AMOUNT like '%" + strExpression + "%'";
                            }
                        }
                        else if (strFind == "Narrations")
                        {
                            strSQL = strSQL + "AND COMP_VOUCHER_NARRATION = '" + strExpression + "'";
                        }
                        else
                        {
                            strSQL = strSQL + "AND COMP_VOUCHER_DATE BETWEEN ";
                            strSQL = strSQL + Utility.cvtSQLDateString(mdteVFromDate) + " ";
                            strSQL = strSQL + "AND ";
                            strSQL = strSQL + " " + Utility.cvtSQLDateString(mdteVToDate) + " ";
                        }
                        strSQL = strSQL + "AND BRANCH_ID in (select BRANCH_ID from USER_PRIVILEGES_BRANCH where USER_LOGIN_NAME  ='" + strUserName + "')";
                        strSQL = strSQL + " ORDER By REF_NO,TERITORRY_CODE,LEDGER_CODE,LEDGER_NAME  ";
                    }
                }
                else
                {
                    strSQL = strmySql;
                }

                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    AccountsVoucher oLedg = new AccountsVoucher();
                    oLedg.strVoucherNo = Utility.Mid(dr["COMP_REF_NO"].ToString(), 6, dr["COMP_REF_NO"].ToString().Length - 6);
                    oLedg.strLedgerName = dr["LEDGER_NAME"].ToString();
                    oLedg.strBranchName = dr["BRANCH_NAME"].ToString();
                    oLedg.strTranDate = Convert.ToDateTime(dr["COMP_VOUCHER_DATE"]).ToString("dd/MM/yyyy");
                    oLedg.dblAmount = Convert.ToDouble(dr["COMP_VOUCHER_NET_AMOUNT"].ToString());

                    if (dr["VOUCHER_REVERSE_LEDGER"].ToString() != "")
                    {
                        oLedg.strReverseLegder = dr["VOUCHER_REVERSE_LEDGER"].ToString();
                    }
                    else
                    {
                        oLedg.strReverseLegder = "";
                    }

                    oLedg.strMerzeName = dr["LEDGER_NAME_MERZE"].ToString();
                    oLedg.intAppStatus = Convert.ToInt16(dr["APP_STATUS"].ToString());
                    oLedg.intvoucherPos = Convert.ToInt16(dr["APPS_COMM_CAL"].ToString());
                    oLedg.intAppSIRet = Convert.ToInt16(dr["APPS_SI_RET"].ToString());
                    if (dr["DIVISION"].ToString() != "")
                    {
                        oLedg.strDivisionName = dr["DIVISION"].ToString();
                    }
                    else
                    {
                        oLedg.strDivisionName = "";
                    }
                    if (dr["AREA"].ToString() != "")
                    {
                        oLedg.strArea = dr["AREA"].ToString();
                    }
                    else
                    {
                        oLedg.strArea = "";
                    }
                    oLedg.intStatus = Convert.ToInt16(dr["COMP_VOUCHER_STATUS"].ToString());
                    oLedg.strPreserveSQL = strSQL;
                    ooAccLedger.Add(oLedg);
                }
                //return ooAccLedger;
                dr.Close();
                gcnMain.Close();
                gcnMain.Dispose();

                return Json(ooAccLedger, JsonRequestBehavior.AllowGet);

            }

        }

        List<StockGroup>  StockGrouplsit(string strDeComID)
        {
            string strSQL;
            SqlDataReader drGetGroup;
            List<StockGroup> oogrp = new List<StockGroup>();
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            strSQL = "SELECT GR_NAME_SERIAL, GR_NAME ";
            strSQL = strSQL + "FROM INV_GROUP_MASTER ORDER BY GR_NAME ASC ";
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
                    StockGroup ogrp = new StockGroup();
                    ogrp.lngslNo = Convert.ToInt64(drGetGroup["GR_NAME_SERIAL"].ToString());
                    ogrp.GroupName = drGetGroup["GR_NAME"].ToString();
                    oogrp.Add(ogrp);

                }
                drGetGroup.Close();
                gcnMain.Dispose();
                return oogrp;

            }
        }
        public JsonResult CommossionCalculet(List<StockItem> objStockItem)
        {
            string strSubGroup = "", strItemGroup = "", str2ndGroup = "", strGrid = "", strOrdate = "", strCustomer = "", strOldRefNo="";
            double dblAmount = 0, dblItemAmount=0;
            int m_action = 1;
            string strComID = "0003";
            string strBranchID = "0001";
          List< CommitionGroup > objCommitionGroup = new List<CommitionGroup>();
            List<StockItem> oogrp = new List<StockItem>();
            foreach (var s in objStockItem)
            {


                strOrdate = s.strDate;
                strCustomer = s.strCustomer;
            }
                //List<StockGroup> ooSample = invms.mFillStockGroupconfig(strComID).ToList();
                List<StockGroup> ooSample = StockGrouplsit(strComID);
                foreach (StockGroup oobj in ooSample)
                {
                    strItemGroup = oobj.GroupName;

                    for (int int2nd = 0; int2nd < objStockItem.Count; int2nd++)
                    {
                        if (objStockItem[int2nd].strItemName != null)
                        {
                            str2ndGroup = objStockItem[int2nd].strSubGroup_Name;
                            if (strItemGroup == str2ndGroup)
                            {

                                dblAmount = objStockItem[int2nd].intItemQty * objStockItem[int2nd].dblItemRate;
                                dblItemAmount = dblItemAmount + dblAmount;
                            }
                        }
                    }
                    if (dblItemAmount != 0)
                    {
                        strGrid += strItemGroup + "|" + dblItemAmount + "~";
                    }
                    dblItemAmount = 0;
                }


            //}

            if (strGrid != "")
            {
                double dblPercent = 0, dblFixedPercent = 0;
                string strFDate = "", strTdate = "";
                string[] words = strGrid.Split('~');
                foreach (string ooassets in words)
                {
                    string[] oAssets = ooassets.Split('|');
                    if (oAssets[0] != "")
                    {
                        dblPercent = Utility.mdblGetCommiPercen(strComID, oAssets[0], Utility.Val(oAssets[1]), strBranchID);
                        strFDate = Utility.FirstDayOfMonth(Convert.ToDateTime( strOrdate)).ToString("dd/MM/yyyy");
                        strTdate = Convert.ToDateTime( strOrdate).ToString("dd-MM-yyyy");
                        if (m_action == 1)
                        {
                            dblFixedPercent = Utility.mdblGetMaxCommiPercen(strComID, strCustomer, oAssets[0], strFDate, strTdate, strBranchID, "");
                        }
                        else
                        {
                            dblFixedPercent = Utility.mdblGetMaxCommiPercen(strComID, strCustomer, oAssets[0], strFDate, strTdate, strBranchID, strOldRefNo);
                        }
                        if (dblFixedPercent == 40)
                        {
                            dblPercent = 40;
                        }

                     

                        for (int int2nd = 0; int2nd < objStockItem.Count; int2nd++)
                        {
                            if (objStockItem[int2nd].strSubGroup_Name != null)
                            {
                                str2ndGroup = objStockItem[int2nd].strSubGroup_Name;
                                if (oAssets[0] == str2ndGroup)
                                {

                   
                        

                                    StockItem objg = new StockItem();
                                    double dblCommitionValp = 0;

                                    objg.strItemGroup = objStockItem[int2nd].strItemGroup;
                                    objg.strItemName = objStockItem[int2nd].strItemName;
                                    objg.strPowerClass = objStockItem[int2nd].strPowerClass;
                                    objg.strSubGroup_Name = objStockItem[int2nd].strSubGroup_Name;
                                    objg.strPackSize = objStockItem[int2nd].strPackSize;
                                    objg.intItemQty = objStockItem[int2nd].intItemQty;
                                    objg.dblItemRate = objStockItem[int2nd].dblItemRate;
                                    objg.strUnit = objStockItem[int2nd].strUnit;
                                    objg.intBonusQty = objStockItem[int2nd].intBonusQty;
                                    objg.dblTotalAmount = objStockItem[int2nd].dblTotalAmount;
                                    objg.strCustomer = objStockItem[int2nd].strCustomer;
                                    objg.strDate = objStockItem[int2nd].strDate;
                               

                                    objg.strCommitionGroupItemName = objStockItem[int2nd].strItemName;
                                    objg.dblCommitionVal = (((objStockItem[int2nd].intItemQty) * (objStockItem[int2nd].dblItemRate)) * dblPercent) / 100;
                                    dblCommitionValp = (((objStockItem[int2nd].intItemQty) * (objStockItem[int2nd].dblItemRate)) * dblPercent) / 100;
                                    objg.dblItemNetVal = (((objStockItem[int2nd].dblTotalAmount) - dblCommitionValp));
                                    objg.dblPercent = dblPercent;
                                    objg.strCommitionGroupN = oAssets[0];
                                    oogrp.Add(objg);
           
                                }
                            }
                        }

                        dblItemAmount = 0;
                    }
                }
                //calculateTotal();
            }

       

              return Json(oogrp, JsonRequestBehavior.AllowGet);
        }


        #region "Sales Order"

      
        #endregion

    }


     
    
}