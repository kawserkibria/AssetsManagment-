using AssetsManagment.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AssetsManagment.Repository
{
    public class RepositoryAsst
    {

        public List<Brand> mFillBrandList(string strDeComID)
        {
            string strSQL;
            SqlDataReader drGetGroup;
            List<Brand> oogrp = new List<Brand>();

            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            strSQL = "SELECT SERIAL, BRAND_ID, BRAND_NAME, BRAND_STATUS, STOCKGROUP_NAME,INSERT_BY, INSERT_DATE, UPDATE_DATE FROM  INV_BRAND ";
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
                    ogrp.intSERIAL = Convert.ToInt32(drGetGroup["SERIAL"].ToString());
                    ogrp.strBRAND_ID = drGetGroup["BRAND_ID"].ToString();
                    ogrp.strBRAND_NAME = drGetGroup["BRAND_NAME"].ToString();
                    ogrp.strUnder = drGetGroup["STOCKGROUP_NAME"].ToString();
                    ogrp.intBRAND_STATUS = Convert.ToInt32(drGetGroup["BRAND_STATUS"].ToString());
                    if (Convert.ToInt32(drGetGroup["BRAND_STATUS"].ToString()) == 1)
                    {
                        ogrp.strBRAND_STATUS = "No";
                    }
                    else
                    {
                        ogrp.strBRAND_STATUS = "Yes";
                    }
                    oogrp.Add(ogrp);

                }
                drGetGroup.Close();
                gcnMain.Dispose();
                return oogrp;

            }
        }
        public string InsertBrand(Brand regObject)
        {
            int intBRANDID = 0;
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



                    strSQL = "SELECT (case when  MAX(BRAND_ID) is null then 0 else MAX(BRAND_ID) end) +1 as BRAND_ID FROM INV_BRAND ";
                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();
                    dr = cmdInsert.ExecuteReader();
                    if (dr.Read())
                    {
                        intBRANDID = Convert.ToInt32(dr["BRAND_ID"].ToString());
                    }
                    dr.Close();


                    strSQL = "INSERT INTO INV_BRAND(BRAND_NAME,BRAND_ID,STOCKGROUP_NAME";
                    if ((regObject.strINSERT_BY == null) && (regObject.strINSERT_BY == ""))
                    {
                        strSQL = strSQL + ",INSERT_BY ";
                    }
                    strSQL = strSQL + ") ";
                    strSQL = strSQL + "VALUES('" + regObject.strBRAND_NAME.Replace("'", "''") + "' ";
                    strSQL = strSQL + ",'" + intBRANDID + "' ";
                    strSQL = strSQL + ",'" + regObject.strUnder + "' ";
                    if ((regObject.strINSERT_BY == null) && (regObject.strINSERT_BY == "") && (regObject.strINSERT_BY == ""))
                    {
                        strSQL = strSQL + "'" + regObject.strINSERT_BY + "' ";
                    }
                  
                    strSQL = strSQL + ")";
                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();
                    cmdInsert.Transaction.Commit();
                    gcnMain.Close();

                    return ("Ok");
                }
                catch (Exception ex)
                {
                    return ("Save fail !!");
                }
                finally
                {
                    gcnMain.Close();
                }
            }
        }
        public string UpdateData(Brand regObject)
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
                    SqlCommand cmdInsert = new SqlCommand();
                    SqlTransaction myTrans;

                    myTrans = gcnMain.BeginTransaction();
                    cmdInsert.Connection = gcnMain;
                    cmdInsert.Transaction = myTrans;

                    strSQL = "UPDATE INV_BRAND SET BRAND_NAME = '" + regObject.strBRAND_NAME + "',";
                    strSQL = strSQL + "STOCKGROUP_NAME = '" + regObject.strUnder + "' ";
                    //strSQL = strSQL + "STATUS = " + regObject.intBRAND_STATUS + " ";
                    strSQL = strSQL + "WHERE BRAND_ID = " + regObject.strBRAND_ID + " ";
                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();
                    cmdInsert.Transaction.Commit();
                    gcnMain.Close();
                    return ("Ok");
                }
                catch (Exception ex)
                {
                    return ("Update fail !!");
                }
                finally
                {
                    gcnMain.Close();
                }
            }


        }
        public string DeleteBrand(Brand obj)
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

                    SqlCommand cmdDelete = new SqlCommand();
                    SqlTransaction myTrans;
                    myTrans = gcnMain.BeginTransaction();
                    cmdDelete.Connection = gcnMain;
                    cmdDelete.Transaction = myTrans;

                    strSQL = "DELETE FROM INV_BRAND ";
                    strSQL = strSQL + "WHERE BRAND_ID = '" + obj.strBRAND_ID + "'";
                    cmdDelete.CommandText = strSQL;
                    cmdDelete.ExecuteNonQuery();
                    cmdDelete.Transaction.Commit();
                    gcnMain.Close();

                    return ("OK");
                }

                catch (Exception ex)
                {
                   
                    return ("Error");
                }
                finally
                {
                    gcnMain.Close();
                }

            }




        }

        public List<Group> groupList(Branch obj)
        {

            SqlDataReader drGetGroup;
            List<Group> oogrp = new List<Group>();
            StockGroup sp = new StockGroup();
            Response response = new Response();
            string strSQL;


            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            strSQL = "SELECT STOCKGROUP_SERIAL, STOCKGROUP_NAME, STOCKGROUP_PARENT,STOCKGROUP_ONE_DOWN, STOCKGROUP_PRIMARY,STOCKGROUP_NAME_DEFAULT ,G_STATUS ";
            strSQL = strSQL + "FROM INV_STOCKGROUP ORDER BY STOCKGROUP_NAME ASC ";
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
                    Group ogrp = new Group();
                    ogrp.SERIAL = Convert.ToInt64(drGetGroup["STOCKGROUP_SERIAL"].ToString());
                    ogrp.NAME = drGetGroup["STOCKGROUP_NAME"].ToString();
                    ogrp.PARENT = drGetGroup["STOCKGROUP_PARENT"].ToString();
                    ogrp.PRIMARY = drGetGroup["STOCKGROUP_PRIMARY"].ToString();
                    ogrp.G_STATUS = Convert.ToInt16(drGetGroup["G_STATUS"].ToString());
                    if (drGetGroup["STOCKGROUP_ONE_DOWN"].ToString() != "")
                    {
                        ogrp.ONE_DOWN = drGetGroup["STOCKGROUP_ONE_DOWN"].ToString();
                    }
                    else
                    {
                        ogrp.ONE_DOWN = "";
                    }

                    oogrp.Add(ogrp);

                }
                drGetGroup.Close();
                gcnMain.Dispose();
                sp.data = oogrp;



                return oogrp;

            }
        }
    }
}