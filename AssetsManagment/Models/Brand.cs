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
            data = new List<Brand>();
        }


        public List<Brand> data { get; set; }


    }



 
    public class Brand
    {
        public long intBrandID { get; set; }
        public int B_STATUS { get; set; }
        public string BRAND_NAME { get; set; }

    }



}