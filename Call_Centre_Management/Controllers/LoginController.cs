using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Call_Centre_Management.Classes;
using Call_Centre_Management.Models;
using System.Linq;

namespace Call_Centre_Management.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        Common_Class common_Class = new Common_Class();
        Dictionary<string, object> dict = new Dictionary<string, object>();
        //MenuMaster menu = new MenuMaster();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Emp_Login()
        
        {
            List<MenuMaster> menu_list = new List<MenuMaster>();
            dict.Clear();
            dict.Add("@mode", "Get_All_Menu");
            DataTable dt = common_Class.return_datatable(dict, "proc_Menu");
            int j = Convert.ToInt32(dt.Rows.Count);
            if (j > 0)
            {
                for (int i = 0; i <= j-1; i++)
                {
                    MenuMaster menu = new MenuMaster();
                    menu.NodeID = Convert.ToInt32(dt.Rows[i]["nodeId"].ToString());
                    menu.ParentID=Convert.ToInt32(dt.Rows[i]["parentId"].ToString());
                    menu.NodeName = dt.Rows[i]["nodeName"].ToString();
                    menu.destination = dt.Rows[i]["destination"].ToString();
                    //menu.Controller = dt.Rows[i]["controller"].ToString();
                    //menu.Action = dt.Rows[i]["Action"].ToString();
                    menu_list.Add(menu);
                }
            }
            ViewBag.MenuList = menu_list;
            return View(menu_list);
        }

    }
}