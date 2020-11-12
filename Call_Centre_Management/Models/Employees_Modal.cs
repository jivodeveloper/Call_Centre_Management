using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Call_Centre_Management.Models
{
    public class Employees_Modal
    {
    }

    public class MenuMaster
    {
        public int NodeID { get; set; }
        public string NodeName { get; set; }
       // public string Description { get; set; }
        public Nullable<int> ParentID { get; set; }
        public string Controller { get; set; }
        public string Action{ get; set; }
        public bool IsChecked { get; set; }
        public string destination { get;set; }
        public List<MenuMaster> menus { get; set; }
     //   public IEnumerable<SelectListItem> users { get; set; }
    }

    public class Employees
    {
        public string userName { get; set; }
        public string Password { get; set; }
    }


}