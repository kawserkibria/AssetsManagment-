using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetsManagment.Models
{
    public class BrandResponse
    {


        public void InitialList()
        {
            Brandlist = new List<Brand>();
        }


        public List<Brand> Brandlist { get; set; }


    }



 
    public class Brand
    {

 
          public int intSERIAL { get; set; }
          public string strBRAND_ID { get; set; }
          public string strBRAND_NAME { get; set; }
          public string strUnder { get; set; }
          public string strBRAND_STATUS { get; set; }
          public int intBRAND_STATUS { get; set; } 
          public string strINSERT_BY { get; set; } 
      

    }



}