using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Call_Centre_Management.Models
{
    public class Employees_Modal
    {
        public int id { get; set; }

        [Required]
        [Display(Name = "Enter Full Name")]
        public string fullName { get; set; }

        [Required]
        [Display(Name = "Enter Gender")]
        public string gender { get; set; }

        [Required]
        [Display(Name = "Enter DOB")]
        public string dob { get; set; }

        [Required]
        [Display(Name = "Enter Mobile")]
        public string mobile { get; set; }
        public string email { get; set; }

        [Required]
        [Display(Name = "Enter Full Address")]
        public string address { get; set; }

        [Required]
        [Display(Name = "Enter Area")]
        public string area { get; set; }

        [Required]
        [Display(Name = "Enter State")]
        public string state { get; set; }
        public string Security { get; set; }
        public string pincode { get; set; }

        [Required]
        [Display(Name = "Enter Username")]
        public string userName { get; set; }

        [Required]
        [Display(Name = "Enter Password")]
        public string password { get; set; }
        public string role { get; set; }
        

    }
    public class MenuMaster
    {
        public int NodeID { get; set; }
        public string NodeName { get; set; }
        // public string Description { get; set; }
        public Nullable<int> ParentID { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public bool IsChecked { get; set; }
        public string destination { get; set; }
        public List<MenuMaster> menus { get; set; }
        //   public IEnumerable<SelectListItem> users { get; set; }
    }
    public class Employees
    {
        public string userName { get; set; }
        public string Password { get; set; }
    }
    public class state
    {
        public int Id { get; set; }
        public string State { get; set; }

    }
    public class Role
    {
        public int Id { get; set; }
        public string role { get; set; }

    }
    // Lucky ka kaam
    public class Item_Model
    {
        public int id { get; set; }
        public string Item_id { get; set; }
        public string item_name { get; set; }
        public string category { get; set; }
        public string sub_category { get; set; }
        public string UOM { get; set; }
        public int UOM_value { get; set; }
        public double rate { get; set; }
        public double Scheme_values { get; set; }
        public int scheme_qty { get; set; }
        public int gst { set; get; }

    }
    public class MasterRole
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string active { get; set; }
        public string InsertedDate { get; set; }
    }
    public class StateMasterModal
    {
        public int Id { get; set; }
        public string StateName { get; set; }
        public string Active { get; set; }
    }
    public class ZoneMastermodal
    {
        public int Id { get; set; }
        public string ZoneName { get; set; }
        public int? StateId { get; set; }
        public string StateName { get; set; }
        public string Active { get; set; }
    }

    public class DropdownLit
    {
        public int Id { get; set; }
        public string DropName { get; set; }
    }

}

