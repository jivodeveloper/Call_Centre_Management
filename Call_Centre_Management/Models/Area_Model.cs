using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Call_Centre_Management.Models
{
    public class Area_Model
    {
    }
    public class Area
    {
        public int id { get; set; }
        public string Area_Name { get; set; }
        public string Zone_id { get; set;} 
        public string State_id { get; set; }
        public int pincode { get; set; }

    }
}