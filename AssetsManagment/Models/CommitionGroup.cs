using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetsManagment.Models
{
    public class CommitionGroup
    {
       
        public string strCommitionGroupN { get; set; }
        public string strCommitionGroupItemName { get; set; }
        public double dblPercent { get; set; }
        public double dblCommitionVal { get; set; }
        public double dblItemNetVal { get; set; }
    }
}