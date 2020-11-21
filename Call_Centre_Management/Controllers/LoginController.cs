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

        // lucky ka kaam
        [HttpGet]
        public ActionResult AddRole()
        {
            List<MasterRole> RolesList = new List<MasterRole>();
            List<StateMasterModal> StateList = new List<StateMasterModal>();
            List<ZoneMastermodal> ZoneList = new List<ZoneMastermodal>();
            List<DropdownLit> DropList = new List<DropdownLit>();
            dict.Clear();
            dict.Add("@mode", "GetRoleRecord");
            DataSet dt = common_Class.return_dataset(dict, "Proc_role");
            int r = Convert.ToInt32(dt.Tables.Count);
            if (r > 0)
            {
                for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                {
                    MasterRole master = new MasterRole();
                    master.Id = Convert.ToInt32(dt.Tables[0].Rows[i]["id"]);
                    master.RoleName = dt.Tables[0].Rows[i]["Role"].ToString();
                    master.active = Convert.ToInt32(dt.Tables[0].Rows[i]["Active"]).ToString() == "1" ? "Active" : "Deactive";
                    master.InsertedDate = dt.Tables[0].Rows[i]["inserted_on"].ToString();
                    RolesList.Add(master);
                }
                for (int j = 0; j < dt.Tables[1].Rows.Count; j++)
                {
                    StateMasterModal state = new StateMasterModal();
                    state.Id = Convert.ToInt32(dt.Tables[1].Rows[j]["id"]);
                    state.StateName = dt.Tables[1].Rows[j]["state"].ToString();
                    state.Active = Convert.ToInt32(dt.Tables[1].Rows[j]["Active"]).ToString() == "1" ? "Active" : "Deactive";
                    StateList.Add(state);
                }
                for (int k = 0; k < dt.Tables[2].Rows.Count; k++)
                {
                    ZoneMastermodal zone = new ZoneMastermodal();
                    zone.Id = Convert.ToInt32(dt.Tables[2].Rows[k]["id"]);
                    zone.StateId = Convert.ToInt32(dt.Tables[2].Rows[k]["State_id"]);
                    zone.ZoneName = dt.Tables[2].Rows[k]["Zone"].ToString();
                    zone.StateName = dt.Tables[2].Rows[k]["state"].ToString();
                    zone.Active = Convert.ToInt32(dt.Tables[2].Rows[k]["active"]).ToString() == "1" ? "Active" : "Deactive";
                    ZoneList.Add(zone);
                }
                for (int l = 0; l < dt.Tables[3].Rows.Count; l++)
                {
                    DropdownLit DDLState = new DropdownLit();
                    DDLState.Id = Convert.ToInt32(dt.Tables[3].Rows[l]["id"]);
                    DDLState.DropName = dt.Tables[3].Rows[l]["state"].ToString();
                    DropList.Add(DDLState);
                }
                ViewBag.DropListState = DropList;
                ViewBag.GetZoneList = ZoneList;
                ViewBag.GetRoleList = RolesList;
                ViewBag.GetStateList = StateList;
            }
            return View();
        }
        [HttpPost]
        public ActionResult InsertRole(MasterRole model)
        {
            dict.Clear();
            dict.Add("@Role", model.RoleName);
            dict.Add("@mode", "InsertRole");
            int i = common_Class.return_nonquery(dict, "Proc_role");
            var Result = i > 0 ? Convert.ToBoolean(true).ToString() : Convert.ToBoolean(false).ToString();
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EditRole(MasterRole model)
        {
            dict.Clear();
            dict.Add("@id", model.Id);
            if (model.Id > 0 && model.RoleName != null)
            {
                dict.Add("@Role", model.RoleName);
                dict.Add("@mode", "UpdateRole");
                int i = common_Class.return_nonquery(dict, "Proc_role");
                var Result = i > 0 ? Convert.ToBoolean(true).ToString() : Convert.ToBoolean(false).ToString();
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            else if (model.Id > 0 && model.RoleName == null)
            {
                dict.Add("@mode", "RoleActive/Diactivate");
                DataTable dt = common_Class.return_datatable(dict, "Proc_role");
                int Result2 = Convert.ToInt32(dt.Rows[0][0].ToString());
                return Json(Result2, JsonRequestBehavior.AllowGet);
            }
            return Json(3, JsonRequestBehavior.AllowGet);
        }

    }

}