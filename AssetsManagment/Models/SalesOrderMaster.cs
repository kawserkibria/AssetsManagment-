using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetsManagment.Models
{
    public class SalesOrderMaster
    {

        public void InitialList()
        {
            data = new List<OrderDetails>();
        }

        public List<OrderDetails> data { get; set; }


    }
 


    public class OrderMaster
    {
        public string strBranchId { get; set; }
        public string strBranchName { get; set; }
        public string strLedgerName { get; set; }
        public string strCustomer { get; set; }
        public string strOrderNo { get; set; }
        public string strOrderDate { get; set; }
        public string strLocation { get; set; }

        public string issue_date { get; set; }
        public string created_by { get; set; }
        public DateTime create_date { get; set; }
        public string updated_by { get; set; }
        public DateTime update_date { get; set; }
        public List<StockItem> detailsList { get; set; }

  
    }
    public class OrderDetails
    {
        public List<OrderMaster> gLocationList { get; set; }
        //public File imgfi { get; set; }
        public double dblbonusQty { get; set; }

      

        public string strCommitionGroupN { get; set; }
        public string strItemName { get; set; }
        public string strItemGroup { get; set; }
        public string strSubGroup_Name { get; set; } 
        public double dblPercent { get; set; }
        public double dblCommitionVal { get; set; }
        public int intItemQty { get; set; }
        public int dblItemRate { get; set; }
        public double dblTotalAmount { get; set; }
        public int intBonusQty { get; set; }
        public string strPackSize { get; set; }

        public string strPowerClass { get; set; }
        public string strUnit { get; set; }
        public double dblItemNetVal { get; set; }
        public DateTime create_date { get; set; }
        

    }
}




