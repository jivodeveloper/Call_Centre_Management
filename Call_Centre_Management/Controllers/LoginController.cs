using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Call_Centre_Management.Classes;
using Call_Centre_Management.Models;

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

        public ActionResult Emp_Login(string Username, string Password)
        {
            if (Username != null && Password != null)
            {
                Session["user_name"] = Username.ToString();
                Session["Passsword"] = Password.ToString();
                dict.Clear();
                dict.Add("@UserName", Username);
                dict.Add("@Password", Password);

            }
            else
            {
                dict.Clear();
                dict.Add("@UserName", Session["user_name"].ToString());
                dict.Add("@Password", Session["Passsword"].ToString());
            }

            dict.Add("@mode", "Emp_login");
            DataTable dt = common_Class.return_datatable(dict, "proc_employee");
            int j = Convert.ToInt32(dt.Rows.Count);
            List<MenuMaster> menu_list = new List<MenuMaster>();
            menu_list.Clear();
            if (j > 0)
            {
                for (int i = 0; i <= j - 1; i++)
                {
                    MenuMaster menu = new MenuMaster();
                    menu.NodeID = Convert.ToInt32(dt.Rows[i]["nodeId"].ToString());
                    menu.ParentID = Convert.ToInt32(dt.Rows[i]["parentId"].ToString());
                    menu.NodeName = dt.Rows[i]["nodeName"].ToString();
                    menu.destination = dt.Rows[i]["destination"].ToString();
                    menu_list.Add(menu);
                }

                ViewBag.MenuList = menu_list;
                Session["Permission"] = menu_list;
                return View(menu_list);
            }
            else
            {
                return JavaScript("\"Check User Name And Password\"");
            }

        }

        public ActionResult Employee_Login()
        {

            return View();
        }


        [HttpGet]
        public ActionResult Login()
        {
            Employees emp = new Employees();
            if (emp.userName != null && emp.Password != null)
            {
                string i, j;
                i = emp.userName;
                j = emp.Password;
            }

            return View();
        }
        
  
        public ActionResult logout()
        {
            if (Session["user_name"] != null && Session["Passsword"] != null)
            {
                Session["user_name"] = null;
                Session["Passsword"] = null;
                Session.Contents.RemoveAll();
                Session.Abandon();
                Response.Cookies.Clear();
                Session.Clear();
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMilliseconds(-1); 
            }
            return RedirectToAction("Employee_Login", "Login");
        }

    }

}