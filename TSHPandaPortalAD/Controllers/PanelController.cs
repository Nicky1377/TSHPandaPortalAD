using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSHPandaPortalAD.DBConnect;
using TSHPandaPortalAD.Models;
using TSHPandaPortalAD.ModelView;

namespace TSHPandaPortalAD.Controllers
{
    public class PanelController : Controller
    {
        // GET: Panel
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //public ActionResult SetType(bool HasChang, int Id = 0)
        //{
        //    //
        //    //bool HasChang = false;
        //    //int Id = 1;
        //    PDBC db = new PDBC("PandaMarketCMS", true);
        //    db.Connect();
        //    DataTable dt = db.Select(
        //        "SELECT [tbl_Product_Type].[id_PT],[tbl_Product_Type].[PTname],[tbl_Product_Type].[ISDESABLED],[tbl_Product_Type].[ISDelete],(SELECT COUNT(*)FROM [tbl_Product_MainCategory]WHERE[tbl_Product_Type].[id_PT]=[tbl_Product_MainCategory].id_PT)AS [count] FROM[tbl_Product_Type]");

        //    List<Type_Model> tM=new List<Type_Model>();

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        var model=new Type_Model()
        //        {
        //            num = i+1,
        //            Id =Convert.ToInt32(dt.Rows[i]["id_PT"]),
        //            Name = dt.Rows[i]["PTname"].ToString(),
        //            Disabled = Convert.ToBoolean(dt.Rows[i]["ISDESABLED"]),
        //            Deleted = Convert.ToBoolean(dt.Rows[i]["ISDelete"]),
        //            Count = Convert.ToInt32(dt.Rows[i]["count"])
        //        };
        //        tM.Add(model);
        //    }

        //    var ModelView = new SetTypeModelView()
        //    {
        //        HasChange = HasChang,
        //        TypeModels = tM

        //    };

        //    if (HasChang)
        //    {
        //        DataTable dt1 = db.Select("SELECT[PTname]FROM[tbl_Product_Type]WHERE[id_PT]=" + Id);
        //        if (dt1.Rows.Count!=0)
        //        {
        //            ModelView.Edit_Name =dt1.Rows[0][0].ToString();
        //            ModelView.Edit_Id = Id;
        //        }

        //    }

        //    return View(ModelView);
        //}

        //public ActionResult Enable(int id)
        //{
        //    PDBC db = new PDBC("PandaMarketCMS", true);
        //    db.Connect();

        //    db.Script("UPDATE[tbl_Product_Type] SET[ISDESABLED] = 0  WHERE id_PT=" + id);

        //    return RedirectToAction("SetType", "Panel", new { HasChang = false });
        //}

        //public ActionResult Disable(int id)
        //{
        //    PDBC db = new PDBC("PandaMarketCMS", true);
        //    db.Connect();

        //    db.Script("UPDATE[tbl_Product_Type] SET[ISDESABLED] = 1  WHERE id_PT=" + id);

        //    return RedirectToAction("SetType","Panel", new { HasChang = false });
        //}

        //public ActionResult Delete(int id)
        //{
        //    PDBC db = new PDBC("PandaMarketCMS", true);
        //    db.Connect();

        //    db.Script("UPDATE[tbl_Product_Type] SET [ISDelete] = 1  WHERE id_PT=" + id);

        //    return RedirectToAction("SetType", "Panel", new { HasChang = false });
        //}

        //public ActionResult Add_Update_Type(bool HasChang, string text, int Id = 0)
        //{

        //    PDBC db = new PDBC("PandaMarketCMS", true);
        //    db.Connect();
        //    if (HasChang)
        //    {
        //        db.Script("UPDATE [dbo].[tbl_Product_Type] SET [PTname] = N'" + text + "' WHERE id_PT=" + Id);
        //    }
        //    else
        //    {
        //        db.Script("INSERT INTO [dbo].[tbl_Product_Type] VALUES(N'" + text + "',0,0,null,null)");
        //    }

        //    return RedirectToAction("SetType", "Panel", new { HasChang = false });

        //    //return Content((!HasChang).ToString());
        //}

        //public ActionResult Test()
        //{
        //    return View();
        //}
        public ActionResult Search()
        {
            return View();
        }
      
    }
}