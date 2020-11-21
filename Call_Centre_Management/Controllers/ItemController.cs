using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using Call_Centre_Management.Classes;
using Call_Centre_Management.Models;

namespace Call_Centre_Management.Controllers
{
    public class ItemController : Controller
    {
        Dictionary<string, object> dict = new Dictionary<string, object>();
        Common_Class common_class = new Common_Class();
        public ActionResult Index()
        {
            List<Item_Model> item_list = new List<Item_Model>();
            dict.Clear();
            dict.Add("@mode", "Getall_Items");
            DataTable dt = common_class.return_datatable(dict, "proc_item");
            int j = dt.Rows.Count;
            if (j > 0)
            {
                for (int i = 0; i < j; i++)
                {
                    Item_Model item = new Item_Model();
                    item.id = Convert.ToInt32(dt.Rows[i]["id"].ToString());
                    item.Item_id = dt.Rows[i]["item_id"].ToString();
                    item.item_name = dt.Rows[i]["Item_Name"].ToString();
                    item.category = dt.Rows[i]["category"].ToString();
                    item.sub_category = dt.Rows[i]["Sub_category"].ToString();
                    item.UOM = dt.Rows[i]["Uom"].ToString();
                    item.UOM_value = Convert.ToInt32(dt.Rows[i]["Uom_value"].ToString());
                    item.rate = Convert.ToDouble(dt.Rows[i]["rate"].ToString());
                    item.Scheme_values = Convert.ToInt32(dt.Rows[i]["Scheme_value"].ToString());
                    item.scheme_qty = Convert.ToInt32(dt.Rows[i]["Scheme_Quantity"].ToString());
                    item.gst = Convert.ToInt32(dt.Rows[i]["Gst"].ToString());
                    item_list.Add(item);
                }
            }
            ViewBag.get_item = item_list;
            return View();
        }

        [HttpGet]
        public ActionResult Insert_Item()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert_Item(Item_Model item)
        {
            dict.Clear();
         //   dict.Add("@id",item.id);
            dict.Add("@Item_Name", item.item_name);
            dict.Add("@category", item.category);
            dict.Add("@Sub_category", item.sub_category);
            dict.Add("@Uom", item.UOM);
            dict.Add("@Uom_value", item.UOM_value);
            dict.Add("@rate", item.rate);
            dict.Add("@Scheme_value", item.Scheme_values);
            dict.Add("@Scheme_Quantity", item.scheme_qty);
            dict.Add("@Gst", item.gst);
            dict.Add("@mode", "insert");
            var Result = common_class.return_nonquery(dict, "proc_item");
            return Json(Result,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete_Item(int id)
        {
            dict.Clear();
            dict.Add("@id",id);
            dict.Add("@mode", "Deactive");
            var Result = common_class.return_nonquery(dict, "proc_item");
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit_Item(int id )
        {
            List<Item_Model> item_list = new List<Item_Model>();
            Item_Model item = new Item_Model();
            dict.Clear();
            dict.Add("@id", id);
            dict.Add("@mode", "Getall_Items_ById");
            DataTable dt = common_class.return_datatable(dict, "proc_item");
            int j = dt.Rows.Count;
            if (j > 0)
            {
                item.id = Convert.ToInt32(dt.Rows[0]["id"].ToString());
                item.Item_id = dt.Rows[0]["item_id"].ToString();
                item.item_name = dt.Rows[0]["Item_Name"].ToString();
                item.category = dt.Rows[0]["category"].ToString();
                item.sub_category = dt.Rows[0]["Sub_category"].ToString();
                item.UOM = dt.Rows[0]["Uom"].ToString();
                item.UOM_value = Convert.ToInt32(dt.Rows[0]["Uom_value"].ToString());
                item.rate = Convert.ToDouble(dt.Rows[0]["rate"].ToString());
                item.Scheme_values = Convert.ToInt32(dt.Rows[0]["Scheme_value"].ToString());
                item.scheme_qty = Convert.ToInt32(dt.Rows[0]["Scheme_Quantity"].ToString());
                item.gst = Convert.ToInt32(dt.Rows[0]["Gst"].ToString());
            }
            ViewBag.get_Edit_item = item;
            return View(item);

        }
        [HttpPost]
        public ActionResult Edit_Item(Item_Model item)
        {
            dict.Clear();
            dict.Add("@id", item.id);
            dict.Add("@Item_Name", item.item_name);
            dict.Add("@category", item.category);
            dict.Add("@Sub_category", item.sub_category);
            dict.Add("@Uom", item.UOM);
            dict.Add("@Uom_value", item.UOM_value);
            dict.Add("@rate", item.rate);
            dict.Add("@Scheme_value", item.Scheme_values);
            dict.Add("@Scheme_Quantity", item.scheme_qty);
            dict.Add("@Gst", item.gst);
            dict.Add("@mode", "update");
            var Result = common_class.return_nonquery(dict, "proc_item");
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
    }
}