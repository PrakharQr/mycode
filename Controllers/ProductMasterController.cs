using QuarTest.Models;
using QuarTest.Models.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuarTest.Controllers
{
    public class ProductMasterController : Controller
    {
        // GET: ProductMaster
        DbExecution dl = new DbExecution();
        DataSet ds;
        public ActionResult AddProductMaster()
        {
            return View();
        }
        #region BulkEntry Products
        [HttpPost]
        public ActionResult AddProductMaster(List<MasterProduct_info> Prdtabledata)
        {
            MasterProduct_info obj = new MasterProduct_info();
            DataTable dt = Add_cols();
            DataRow dr = null;
            DataTable dtt = dt as DataTable;
            foreach (MasterProduct_info lst in Prdtabledata)
            {
                dr = dtt.NewRow();
                dr["Brandid"] = Convert.ToInt32(lst.Brandid);
                dr["PrdName"] = lst.PrdName;
                dr["Prdcolor"] = lst.Prdcolor;
                dtt.Rows.Add(dr);
            }
            if (dtt.Rows.Count > 0)
            {
                obj.Prddt = dtt as DataTable;
                obj.Flag = "InrtBulk";
                ds = dl.GetExecute(obj);
            }
            return Json("");
        }
        #endregion
        #region AddColsToTable
        private DataTable Add_cols()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("Brandid", typeof(int)));
            dt.Columns.Add(new DataColumn("PrdName", typeof(string)));
            dt.Columns.Add(new DataColumn("Prdcolor", typeof(string)));
            dr = dt.NewRow();
            return dt;
        }
        #endregion
        #region Bindddl
        public JsonResult GetBrand(string StateId)
        {
            List<SelectListItem> customers = new List<SelectListItem>();
            MasterProduct_info obj = new MasterProduct_info();
            obj.Flag = "GetBrands";
            DataSet ds = dl.GetExecute(obj);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        customers.Add(new SelectListItem
                        {
                            Value = Convert.ToString(ds.Tables[0].Rows[i]["ID"]),
                            Text = Convert.ToString(ds.Tables[0].Rows[i]["Name"]),

                        });
                    }
                }
            }
            return Json(customers);
        }
        #endregion
        #region TableBind
        public JsonResult GetDataBind()
        {
            List<MasterProduct_info> load = new List<MasterProduct_info>();
            MasterProduct_info obj = new MasterProduct_info();
            obj.Flag = "GetLoadData";
            obj.Id = "0";
            DataSet ds = dl.GetExecute(obj);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        load.Add(new MasterProduct_info
                        {
                            PrdName = Convert.ToString(ds.Tables[0].Rows[i]["PrdName"]),
                            Prdcolor = Convert.ToString(ds.Tables[0].Rows[i]["Prdcolor"]),
                            BrandName = Convert.ToString(ds.Tables[0].Rows[i]["brandname"]),
                            CreatedDate = Convert.ToString(ds.Tables[0].Rows[i]["CreatedDate"]),
                            Id= Convert.ToString(ds.Tables[0].Rows[i]["id"]),
                        });
                    }
                }
            }
            //var data = dl.GetExecute(obj);
            //return Json(data, JsonRequestBehavior.AllowGet);
            return Json(load);
        }

        #endregion
        #region Delete Data
        public ActionResult DeleteProduct(string Id)
        {
            MasterProduct_info obj = new MasterProduct_info();
            obj.Flag = "Deldata";
            obj.Id = Id;
            var data = dl.GetExecute(obj);
            return Json("");
        }
        #endregion
    }
}