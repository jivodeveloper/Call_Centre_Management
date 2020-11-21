using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Call_Centre_Management.Models;
using Call_Centre_Management.Classes;

namespace Call_Centre_Management.Controllers
{
    public class EmployeeController : Controller
    {
        Common_Class commonClass = new Common_Class();
        Dictionary<string, object> dict = new Dictionary<string, object>();
        DataTable global_dt = new DataTable();
        // GET: Employee
        public ActionResult Index()
        {
            List<Employees_Modal> employee = new List<Employees_Modal>();

            dict.Clear();
            dict.Add("@mode", "Get_all_Emp");
            global_dt = commonClass.return_datatable(dict, "proc_employee");
            int j = global_dt.Rows.Count;
            if (j > 0)
            {
                for (int i = 0; i < j; i++)
                {
                    Employees_Modal emp = new Employees_Modal();
                    emp.id = Convert.ToInt32(global_dt.Rows[i]["id"].ToString());
                    emp.fullName = global_dt.Rows[i]["fullName"].ToString();
                    emp.gender = global_dt.Rows[i]["gender"].ToString();
                    emp.dob = global_dt.Rows[i]["dob"].ToString();
                    emp.mobile = global_dt.Rows[i]["mobile"].ToString();
                    emp.email = global_dt.Rows[i]["email"].ToString();
                    emp.address = global_dt.Rows[i]["address"].ToString();
                    emp.area = global_dt.Rows[i]["area"].ToString();
                    emp.state = global_dt.Rows[i]["state"].ToString();
                    emp.Security = global_dt.Rows[i]["Security"].ToString();
                    emp.pincode = global_dt.Rows[i]["pincode"].ToString();
                    emp.userName = global_dt.Rows[i]["userName"].ToString();
                    emp.password = global_dt.Rows[i]["password"].ToString();
                    emp.role = global_dt.Rows[i]["role"].ToString();
                    employee.Add(emp);
                }
                ViewBag.count = employee.Count;
                ViewBag.get_employee = employee;
            }
            return View(employee);
        }

        [HttpGet]
        public ActionResult EmployeeRegistration()
        {
            dict.Clear();
            dict.Add("@mode", "get_all_states/Role");
            DataSet ds = commonClass.return_dataset(dict, "proc_employee");
            List<state> list_state = new List<state>();
            List<Role> list_role = new List<Role>();
            int count_state = ds.Tables[0].Rows.Count;
            int count_Role = ds.Tables[1].Rows.Count;


            if (count_state > 0)
            {
                for (int i = 0; i < count_state; i++)
                {
                    state s = new state();
                    s.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
                    s.State = ds.Tables[0].Rows[i]["state"].ToString();
                    list_state.Add(s);
                }
                if (count_Role > 0)
                {
                    for (int i = 0; i < count_Role; i++)
                    {
                        Role r = new Role();
                        r.Id = Convert.ToInt32(ds.Tables[1].Rows[i]["id"].ToString());
                        r.role = ds.Tables[1].Rows[i]["Role"].ToString();
                        list_role.Add(r);
                    }

                }
                ViewBag.getrole = list_role;
                ViewBag.getstate = list_state;
            }
            return View();
        }

        [HttpPost]
        public ActionResult EmployeeRegistrations(Employees_Modal emp)
        {
            //Employees_Modal emp = new Employees_Modal();
            dict.Clear();
            dict.Add("@FullName", emp.fullName);
            dict.Add("@Gender", emp.gender);
            dict.Add("@DOB", emp.dob);
            dict.Add("@Mobile", emp.mobile);
            dict.Add("@Email", emp.email);
            dict.Add("@Address", emp.address);
            dict.Add("@Area", emp.area);
            dict.Add("@State", emp.state);
            dict.Add("@UserName", emp.userName);
            dict.Add("@Password", emp.password);
            dict.Add("@Role", emp.role);
            dict.Add("@Security", emp.Security);
            dict.Add("@mode", "insert");
            var Result = commonClass.return_nonquery(dict, "proc_employee");
            //= i > 0 ? true : false;

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult checkuserName(string username)

        {
            dict.Clear();
            dict.Add("@UserName", username);
            dict.Add("@mode", "check_user_name");
            DataTable dt = commonClass.return_datatable(dict, "proc_employee");
            var Result = dt.Rows.Count > 0 ? true : false;
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit_Employee(int id)
        {
            //id = 3; // remove it after use
            #region return State and Role

            dict.Clear();
            dict.Add("@mode", "get_all_states/Role");
            DataSet ds = commonClass.return_dataset(dict, "proc_employee");
            List<state> list_state = new List<state>();
            List<Role> list_role = new List<Role>();
            int count_state = ds.Tables[0].Rows.Count;
            int count_Role = ds.Tables[1].Rows.Count;


            if (count_state > 0)
            {
                for (int i = 0; i < count_state; i++)
                {
                    state s = new state();
                    s.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
                    s.State = ds.Tables[0].Rows[i]["state"].ToString();
                    list_state.Add(s);
                }
                if (count_Role > 0)
                {
                    for (int i = 0; i < count_Role; i++)
                    {
                        Role r = new Role();
                        r.Id = Convert.ToInt32(ds.Tables[1].Rows[i]["id"].ToString());
                        r.role = ds.Tables[1].Rows[i]["Role"].ToString();
                        list_role.Add(r);
                    }

                }
                ViewBag.getrole = list_role;
                ViewBag.getstate = list_state;
            }
            #endregion

            Employees_Modal emp = new Employees_Modal();
            dict.Clear();
            dict.Add("@id", id);
            dict.Add("@mode", "search_Emp_byID");
            DataTable dt = commonClass.return_datatable(dict, "proc_employee");

            if (dt.Rows.Count > 0)
            {
                emp.id = Convert.ToInt32(dt.Rows[0]["id"].ToString());
                emp.fullName = dt.Rows[0]["fullName"].ToString();
                emp.gender = dt.Rows[0]["gender"].ToString();
                emp.dob = dt.Rows[0]["dob"].ToString();
                emp.mobile = dt.Rows[0]["mobile"].ToString();
                emp.email = dt.Rows[0]["email"].ToString();
                emp.Security = dt.Rows[0]["Security"].ToString();
                emp.area = dt.Rows[0]["area"].ToString();
                emp.state = dt.Rows[0]["state"].ToString();
                emp.address = dt.Rows[0]["address"].ToString();
                emp.role = dt.Rows[0]["role"].ToString();
                emp.userName = dt.Rows[0]["userName"].ToString();
                emp.password = dt.Rows[0]["password"].ToString();

            }
            ViewBag.Employee = emp;
            return View(emp);
        }

        [HttpGet]

        public ActionResult Employee_Delete(int id)
        {
            dict.Clear();
            dict.Add("@id", id);
            dict.Add("@mode", "Active/Deactive_Employee");
            var result = commonClass.return_nonquery(dict, "proc_employee");
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}