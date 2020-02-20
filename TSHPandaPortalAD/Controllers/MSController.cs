using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSHPandaPortalAD.DBConnect;
using TSHPandaPortalAD.Models;

namespace TSHPandaPortalAD.Controllers
{
    public class MSController : Controller
    {
        /////////////////////////////{   START Index   }//////////////////////////////
        public TypeASPX model;
        public type data_type;
        List<type> list_dat = new List<type>();
        // GET: MS
        /////////////////////////////////////////////////////////// Index : get
        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }
        /////////////////////////////////////////////////////////// New_type : get
        [HttpGet]
        public ActionResult New_type()
        {
            model = new TypeASPX();
            if (Session["edit"] != null)
            {
                model = (TypeASPX)Session["edit"];
                ViewBag.model = model;
            }
            else
            {
                ViewBag.model = model;
            }
            return View();
        }
        /////////////////////////////////////////////////////////// TypePage : post
        [HttpPost]
        public ActionResult TypePage(string action, string value, string id)
        {
            string query_new, query_edit;
            string res = " ";


            PDBC db = new PDBC("PandaMarketCMS", true);
            db.Connect();

            List<ExcParameters> paramss = new List<ExcParameters>();
            ExcParameters parameters = new ExcParameters();

            if (action == "new")
            {

                query_new = "INSERT INTO [dbo].[tbl_Product_Type]([PTname],[ISDESABLED],[ISDelete])VALUES(@PTname,0,0)";

                parameters = new ExcParameters()
                {
                    _KEY = "@PTname",
                    _VALUE = value
                };
                paramss.Add(parameters);

                res = db.Script(query_new, paramss);

            }

            else if (action == "edit")
            {

                query_edit = "UPDATE [dbo].[tbl_Product_Type] SET [PTname] = @PTname WHERE id_PT =@id_PT";

                parameters = new ExcParameters()
                {
                    _KEY = "@id_PT",
                    _VALUE = id
                };

                paramss.Add(parameters);

                parameters = new ExcParameters()
                {
                    _KEY = "@PTname",
                    _VALUE = value
                };

                paramss.Add(parameters);

                res = db.Script(query_edit, paramss);

            }

            return Content(res);
        }
        //////////////////////////////////////////////////// list type : get
        public ActionResult table_Type()
        {
            string query_type;

            PDBC db = new PDBC("PandaMarketCMS", true);
            db.Connect();
            // query_type = "SELECT [id_PT],[PTname],[ISDESABLED],[ISDelete] FROM [dbo].[tbl_Product_Type]";
            query_type = "select [id_PT] ,[PTname],[ISDESABLED],[ISDelete],( select count (id_SC) from [dbo].[tbl_Product_SubCategoryOptionKey] where id_SC in ( select id_SC from [dbo].[tbl_Product_SubCategory] where id_MC in ( select id_MC from [dbo].[tbl_Product_MainCategory] where [id_PT]=[dbo].[tbl_Product_Type].[id_PT] )))  as 'count' from [dbo].[tbl_Product_Type]";
            using (DataTable dt = db.Select(query_type))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    data_type = new type();

                    data_type.id_PT = dt.Rows[i]["id_PT"].ToString();
                    data_type.PTname = dt.Rows[i]["PTname"].ToString();
                    data_type.ISDelete = dt.Rows[i]["ISDelete"].ToString();
                    data_type.ISDESABLED = dt.Rows[i]["ISDESABLED"].ToString();
                    data_type.count = dt.Rows[i]["count"].ToString();
                    list_dat.Add(data_type);
                }
                ViewBag.type = list_dat;
            }

            return View();

        }
        /////////////////////////////////////////////////////////// list type : post
        [HttpPost]
        public ActionResult Type_Switch(string action, string id)
        {

            string str = " ", query;

            PDBC db = new PDBC("PandaMarketCMS", true);
            db.Connect();

            List<ExcParameters> paramss = new List<ExcParameters>();
            ExcParameters parameters = new ExcParameters();

            parameters = new ExcParameters()
            {
                _KEY = "@id_PT",
                _VALUE = id
            };

            paramss.Add(parameters);

            if (action == "edit")
            {
                using (DataTable dt = db.Select($"SELECT [id_PT],[PTname] FROM [dbo].[tbl_Product_Type] where id_PT= '{id}'"))
                {
                    model = new TypeASPX()
                    {
                        ChangeID = dt.Rows[0]["id_PT"].ToString(),
                        HasChange = true,
                        ChangeValue = dt.Rows[0]["PTname"].ToString()
                    };

                    Session["edit"] = model;
                    return RedirectToAction("New_type");

                };
            }
            else if (action == "delete")
            {
                parameters = new ExcParameters()
                {
                    _KEY = "@value",
                    _VALUE = "1"
                };
                paramss.Add(parameters);

                query = "UPDATE [dbo].[tbl_Product_Type] SET [ISDelete] = @value ,[DateDeleted] = GETDATE() WHERE id_PT = @id_PT";

                str = db.Script(query, paramss);

            }
            else if (action == "off")
            {
                parameters = new ExcParameters()
                {
                    _KEY = "@value",
                    _VALUE = "1"
                };
                paramss.Add(parameters);

                query = "UPDATE [dbo].[tbl_Product_Type] SET [ISDESABLED] = @value ,[DateDesabled] = GETDATE()  WHERE id_PT = @id_PT";

                str = db.Script(query, paramss);
            }
            else if (action == "on")
            {
                parameters = new ExcParameters()
                {
                    _KEY = "@value",
                    _VALUE = "0"
                };
                paramss.Add(parameters);

                query = "UPDATE [dbo].[tbl_Product_Type] SET [ISDESABLED] = @value ,[DateDesabled] = GETDATE() WHERE id_PT = @id_PT";

                str = db.Script(query, paramss);
            }
            return RedirectToAction("table_Type");
        }

        ///------///////////////////////{   End Index   }//////////////////////////////





        //////////////////////////{   START maincat   }//////////////////////////////
        public MainCategory data_cat;
        List<MainCategory> list_cat = new List<MainCategory>();
        /////////////////////////////////////////////////////////// maincat : get
        [HttpGet]
        public ActionResult maincat()
        {
            return View();
        }
        /////////////////////////////////////////////////////////// New_Cat : get
        [HttpGet]
        public ActionResult New_Cat()
        {
            model = new TypeASPX();
            PDBC db = new PDBC("PandaMarketCMS", true);
            db.Connect();

            using (DataTable dt = db.Select("SELECT [id_PT],[PTname],[ISDESABLED],[ISDelete] FROM [dbo].[tbl_Product_Type]"))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    data_type = new type();


                    if (dt.Rows[i]["ISDelete"].ToString() == "1")
                    {
                        data_type.id_PT = dt.Rows[i]["id_PT"].ToString();
                        data_type.PTname = dt.Rows[i]["PTname"].ToString() + "(حذف شده)";
                        list_dat.Add(data_type);

                    }
                    else if (dt.Rows[i]["ISDESABLED"].ToString() == "1")
                    {

                        data_type.id_PT = dt.Rows[i]["id_PT"].ToString();
                        data_type.PTname = dt.Rows[i]["PTname"].ToString() + "(غیر فعال)";
                        list_dat.Add(data_type);
                    }
                    else
                    {
                        data_type.id_PT = dt.Rows[i]["id_PT"].ToString();
                        data_type.PTname = dt.Rows[i]["PTname"].ToString();
                        list_dat.Add(data_type);
                    }


                }
                ViewBag.Cat = list_dat;
            };


            if (Session["edit_cat"] != null)
            {
                model = (TypeASPX)Session["edit_cat"];
                ViewBag.model = model;
            }
            else
            {
                ViewBag.model = model;
            }

            return View();
        }
        /////////////////////////////////////////////////////////// CatPage : post
        [HttpPost]
        public ActionResult CatPage(string action, string value, string id, string data_typa)
        {
            string query_new, query_edit;
            string res = " ";


            PDBC db = new PDBC("PandaMarketCMS", true);
            db.Connect();

            List<ExcParameters> paramss = new List<ExcParameters>();
            ExcParameters parameters = new ExcParameters();

            if (action == "new")
            {

                query_new = "INSERT INTO [dbo].[tbl_Product_MainCategory]([id_PT],[MCName],[ISDESABLED],[ISDelete])VALUES (@data_typa, @value,0,0)";

                parameters = new ExcParameters()
                {
                    _KEY = "@value",
                    _VALUE = value
                };
                paramss.Add(parameters);

                parameters = new ExcParameters()
                {
                    _KEY = "@data_typa",
                    _VALUE = data_typa
                };
                paramss.Add(parameters);

                res = db.Script(query_new, paramss);

            }

            else if (action == "edit")
            {

                query_edit = "UPDATE [dbo].[tbl_Product_MainCategory]SET [MCName] = @value WHERE id_MC = @id ";

                parameters = new ExcParameters()
                {
                    _KEY = "@id",
                    _VALUE = id
                };

                paramss.Add(parameters);

                parameters = new ExcParameters()
                {
                    _KEY = "@value",
                    _VALUE = value
                };

                paramss.Add(parameters);


                res = db.Script(query_edit, paramss);

            }

            return Content(res);
        }
        //////////////////////////////////////////////////// list cat : get
        public ActionResult table_Cat()
        {
            string query_type;

            PDBC db = new PDBC("PandaMarketCMS", true);
            db.Connect();
            query_type = "SELECT [id_MC],[id_PT],[MCName],[ISDESABLED],[ISDelete],(select PTname from [dbo].[tbl_Product_Type]where [id_PT]=[tbl_Product_MainCategory].[id_PT] ) as 'name_PT',( select count (id_SC) from [dbo].[tbl_Product_SubCategoryOptionKey] where id_SC in( select id_SC from [dbo].[tbl_Product_SubCategory] where id_MC =[dbo].[tbl_Product_MainCategory].[id_MC] ))  as 'count' FROM [dbo].[tbl_Product_MainCategory]";
            // query_type = "select [id_PT] ,[PTname],[ISDESABLED],[ISDelete],( select count (id_SC) from [dbo].[tbl_Product_SubCategoryOptionKey] where id_SC in ( select id_SC from [dbo].[tbl_Product_SubCategory] where id_MC in ( select id_MC from [dbo].[tbl_Product_MainCategory] where [id_PT]=[dbo].[tbl_Product_Type].[id_PT] )))  as 'count' from [dbo].[tbl_Product_Type]";
            using (DataTable dt = db.Select(query_type))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    data_cat = new MainCategory();

                    data_cat.id_MC = dt.Rows[i]["id_MC"].ToString();
                    data_cat.id_PT = dt.Rows[i]["name_PT"].ToString();
                    data_cat.MCName = dt.Rows[i]["MCName"].ToString();
                    data_cat.ISDelete = dt.Rows[i]["ISDelete"].ToString();
                    data_cat.ISDESABLED = dt.Rows[i]["ISDESABLED"].ToString();
                    data_cat.count = dt.Rows[i]["count"].ToString();
                    list_cat.Add(data_cat);
                }
                ViewBag.Cats = list_cat;
            }

            return View();

        }
        /////////////////////////////////////////////////////////// list cat : post
        [HttpPost]
        public ActionResult Cat_Switch(string action, string id)
        {

            string str = " ", query;

            PDBC db = new PDBC("PandaMarketCMS", true);
            db.Connect();

            List<ExcParameters> paramss = new List<ExcParameters>();
            ExcParameters parameters = new ExcParameters();

            parameters = new ExcParameters()
            {
                _KEY = "@id_MC",
                _VALUE = id
            };

            paramss.Add(parameters);

            if (action == "edit")
            {
                using (DataTable dt = db.Select($"SELECT [id_MC],[MCName]FROM [dbo].[tbl_Product_MainCategory] where id_MC= '{id}'"))
                {
                    model = new TypeASPX()
                    {
                        ChangeID = dt.Rows[0]["id_MC"].ToString(),
                        HasChange = true,
                        ChangeValue = dt.Rows[0]["MCName"].ToString()
                    };

                    Session["edit_cat"] = model;
                    return RedirectToAction("New_Cat");

                };
            }
            else if (action == "delete")
            {
                parameters = new ExcParameters()
                {
                    _KEY = "@value",
                    _VALUE = "1"
                };
                paramss.Add(parameters);

                query = "UPDATE [dbo].[tbl_Product_MainCategory] SET [ISDelete] = @value , [DateDeleted] = GETDATE()  WHERE id_MC = @id_MC";

                str = db.Script(query, paramss);

            }
            else if (action == "off")
            {
                parameters = new ExcParameters()
                {
                    _KEY = "@value",
                    _VALUE = "1"
                };
                paramss.Add(parameters);

                query = "UPDATE [dbo].[tbl_Product_MainCategory] SET [ISDESABLED] = @value , [DateDesabled] = GETDATE() WHERE id_MC= @id_MC";

                str = db.Script(query, paramss);
            }
            else if (action == "on")
            {
                parameters = new ExcParameters()
                {
                    _KEY = "@value",
                    _VALUE = "0"
                };
                paramss.Add(parameters);

                query = "UPDATE [dbo].[tbl_Product_MainCategory] SET [ISDESABLED] = @value , [DateDesabled] = GETDATE() WHERE id_MC= @id_MC";

                str = db.Script(query, paramss);
            }
            return RedirectToAction("table_Cat");
        }

        ///------///////////////////////{   End maincat   }//////////////////////////////




        //////////////////////////{   START SubCat    }//////////////////////////////
        public SubCategory data_Sub;
        List<SubCategory> list_Sub = new List<SubCategory>();
        /////////////////////////////////////////////////////////// SubCat : get
        [HttpGet]
        public ActionResult SubCat()
        {
            return View();
        }
        /////////////////////////////////////////////////////////// New_Sub : get
        [HttpGet]
        public ActionResult New_Sub()
        {
            model = new TypeASPX();
            PDBC db = new PDBC("PandaMarketCMS", true);
            db.Connect();

            using (DataTable dt = db.Select("SELECT [id_MC],[id_PT],[MCName],[ISDESABLED],[ISDelete]FROM [dbo].[tbl_Product_MainCategory]"))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    data_cat = new MainCategory();


                    if (dt.Rows[i]["ISDelete"].ToString() == "1")
                    {
                        data_cat.id_MC = dt.Rows[i]["id_MC"].ToString();
                        data_cat.MCName = dt.Rows[i]["MCName"].ToString() + " (حذف شده) ";
                        list_cat.Add(data_cat);

                    }
                    else if (dt.Rows[i]["ISDESABLED"].ToString() == "1")
                    {

                        data_cat.id_MC = dt.Rows[i]["id_MC"].ToString();
                        data_cat.MCName = dt.Rows[i]["MCName"].ToString() + " (غیر فعال) ";
                        list_cat.Add(data_cat);
                    }
                    else
                    {
                        data_cat.id_MC = dt.Rows[i]["id_MC"].ToString();
                        data_cat.MCName = dt.Rows[i]["MCName"].ToString();
                        list_cat.Add(data_cat);
                    }


                }
                ViewBag.Sub = list_cat;
            };


            if (Session["edit_Sub"] != null)
            {
                model = (TypeASPX)Session["edit_Sub"];
                ViewBag.model = model;
            }
            else
            {
                ViewBag.model = model;
            }

            return View();
        }
        /////////////////////////////////////////////////////////// SubPage : post
        [HttpPost]
        public ActionResult SubPage(string action, string value, string id, string data_Sub)
        {
            string query_new, query_edit;
            string res = " ";


            PDBC db = new PDBC("PandaMarketCMS", true);
            db.Connect();

            List<ExcParameters> paramss = new List<ExcParameters>();
            ExcParameters parameters = new ExcParameters();

            if (action == "new")
            {

                query_new = "INSERT INTO [dbo].[tbl_Product_SubCategory]([id_MC],[SCName],[ISDESABLED],[ISDelete])VALUES (@data_Sub,@value,0,0)";

                parameters = new ExcParameters()
                {
                    _KEY = "@value",
                    _VALUE = value
                };
                paramss.Add(parameters);

                parameters = new ExcParameters()
                {
                    _KEY = "@data_Sub",
                    _VALUE = data_Sub
                };
                paramss.Add(parameters);

                res = db.Script(query_new, paramss);

            }

            else if (action == "edit")
            {

                query_edit = "UPDATE [dbo].[tbl_Product_SubCategory]SET [SCName] = @value WHERE id_SC = @id ";

                parameters = new ExcParameters()
                {
                    _KEY = "@id",
                    _VALUE = id
                };

                paramss.Add(parameters);

                parameters = new ExcParameters()
                {
                    _KEY = "@value",
                    _VALUE = value
                };

                paramss.Add(parameters);


                res = db.Script(query_edit, paramss);

            }

            return Content(res);
        }
        //////////////////////////////////////////////////// list sub : get
        public ActionResult table_Sub()
        {
            string query_type;

            PDBC db = new PDBC("PandaMarketCMS", true);
            db.Connect();
            //query_type = "SELECT [id_MC],[id_PT],[MCName],[ISDESABLED],[ISDelete],(select PTname from [dbo].[tbl_Product_Type]where [id_PT]=[tbl_Product_MainCategory].[id_PT] ) as 'name_PT',( select count (id_SC) from [dbo].[tbl_Product_SubCategoryOptionKey] where id_SC in( select id_SC from [dbo].[tbl_Product_SubCategory] where id_MC =[dbo].[tbl_Product_MainCategory].[id_MC] ))  as 'count' FROM [dbo].[tbl_Product_MainCategory]";
            query_type = "SELECT [id_SC],[id_MC],[SCName],[ISDESABLED],[ISDelete],(select MCName from [dbo].[tbl_Product_MainCategory] where [id_MC]=[tbl_Product_SubCategory].[id_MC] ) as 'nameMC',( select count (id_SC) from [dbo].[tbl_Product_SubCategoryOptionKey] where id_SC =[dbo].[tbl_Product_SubCategory].[id_SC]) as 'count' FROM [dbo].[tbl_Product_SubCategory]";
            using (DataTable dt = db.Select(query_type))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    data_Sub = new SubCategory();

                    data_Sub.id_SC = dt.Rows[i]["id_SC"].ToString();
                    data_Sub.id_MC = dt.Rows[i]["nameMC"].ToString();
                    data_Sub.SCName = dt.Rows[i]["SCName"].ToString();
                    data_Sub.ISDelete = dt.Rows[i]["ISDelete"].ToString();
                    data_Sub.ISDESABLED = dt.Rows[i]["ISDESABLED"].ToString();
                    data_Sub.count = dt.Rows[i]["count"].ToString();
                    list_Sub.Add(data_Sub);
                }
                ViewBag.Subs = list_Sub;
            }

            return View();

        }
        /////////////////////////////////////////////////////////// list sub : post
        [HttpPost]
        public ActionResult Sub_Switch(string action, string id)
        {

            string str = " ", query;

            PDBC db = new PDBC("PandaMarketCMS", true);
            db.Connect();

            List<ExcParameters> paramss = new List<ExcParameters>();
            ExcParameters parameters = new ExcParameters();

            parameters = new ExcParameters()
            {
                _KEY = "@id_SC",
                _VALUE = id
            };

            paramss.Add(parameters);

            if (action == "edit")
            {
                using (DataTable dt = db.Select($"SELECT [id_SC],[SCName] FROM [dbo].[tbl_Product_SubCategory] where [id_SC]= '{id}'"))
                {
                    model = new TypeASPX()
                    {
                        ChangeID = dt.Rows[0]["id_SC"].ToString(),
                        HasChange = true,
                        ChangeValue = dt.Rows[0]["SCName"].ToString()
                    };

                    Session["edit_Sub"] = model;
                    return RedirectToAction("New_Sub");

                };
            }
            else if (action == "delete")
            {
                parameters = new ExcParameters()
                {
                    _KEY = "@value",
                    _VALUE = "1"
                };
                paramss.Add(parameters);

                query = "UPDATE [dbo].[tbl_Product_SubCategory] SET [ISDelete] = @value , [DateDeleted] = GETDATE()  WHERE id_SC= @id_SC";

                str = db.Script(query, paramss);

            }
            else if (action == "off")
            {
                parameters = new ExcParameters()
                {
                    _KEY = "@value",
                    _VALUE = "1"
                };
                paramss.Add(parameters);

                query = "UPDATE [dbo].[tbl_Product_SubCategory] SET [ISDESABLED] = @value , [DateDesabled] = GETDATE() WHERE id_SC= @id_SC";

                str = db.Script(query, paramss);
            }
            else if (action == "on")
            {
                parameters = new ExcParameters()
                {
                    _KEY = "@value",
                    _VALUE = "0"
                };
                paramss.Add(parameters);

                query = "UPDATE [dbo].[tbl_Product_SubCategory] SET [ISDESABLED] = @value , [DateDesabled] = GETDATE() WHERE id_SC= @id_SC";

                str = db.Script(query, paramss);
            }
            return RedirectToAction("table_Sub");
        }
        ///------///////////////////////{   End Subcat   }//////////////////////////////



        //////////////////////////{   START SubCatKey    }//////////////////////////////
        public SubCategoryOptionKey data_SCK;
        List<SubCategoryOptionKey> list_SCK = new List<SubCategoryOptionKey>();
        /////////////////////////////////////////////////////////// SubCatKey : get
        [HttpGet]
        public ActionResult SubCatKey()
        {
            return View();
        }
        /////////////////////////////////////////////////////////// New_SCK : get
        [HttpGet]
        public ActionResult New_SCK()
        {
            model = new TypeASPX();
            PDBC db = new PDBC("PandaMarketCMS", true);
            db.Connect();

            using (DataTable dt = db.Select("SELECT [id_SC],[id_MC],[SCName],[ISDESABLED],[ISDelete],[DateDesabled],[DateDeleted]FROM [dbo].[tbl_Product_SubCategory]"))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    data_Sub = new SubCategory();


                    if (dt.Rows[i]["ISDelete"].ToString() == "1")
                    {
                        data_Sub.id_SC = dt.Rows[i]["id_SC"].ToString();
                        data_Sub.SCName = dt.Rows[i]["SCName"].ToString() + " (حذف شده) ";
                        list_Sub.Add(data_Sub);

                    }
                    else if (dt.Rows[i]["ISDESABLED"].ToString() == "1")
                    {

                        data_Sub.id_SC = dt.Rows[i]["id_SC"].ToString();
                        data_Sub.SCName = dt.Rows[i]["SCName"].ToString() + " (غیر فعال) ";
                        list_Sub.Add(data_Sub);
                    }
                    else
                    {
                        data_Sub.id_SC = dt.Rows[i]["id_SC"].ToString();
                        data_Sub.SCName = dt.Rows[i]["SCName"].ToString();
                        list_Sub.Add(data_Sub);
                    }


                }
                ViewBag.SCK = list_Sub;
            };


            if (Session["edit_SCK"] != null)
            {
                model = (TypeASPX)Session["edit_SCK"];
                ViewBag.model = model;
            }
            else
            {
                ViewBag.model = model;
            }

            return View();
        }
        /////////////////////////////////////////////////////////// SCKPage : post
        [HttpPost]
        public ActionResult SCKPage(string action, string value, string id, string data_SCK)
        {
            string query_new, query_edit;
            string res = " ";


            PDBC db = new PDBC("PandaMarketCMS", true);
            db.Connect();

            List<ExcParameters> paramss = new List<ExcParameters>();
            ExcParameters parameters = new ExcParameters();

            if (action == "new")
            {

                query_new = "INSERT INTO [dbo].[tbl_Product_SubCategoryOptionKey]([id_SC],[SCOKName],[ISDESABLED],[ISDelete])VALUES(@data_SCK,@value,0,0)";

                parameters = new ExcParameters()
                {
                    _KEY = "@value",
                    _VALUE = value
                };
                paramss.Add(parameters);

                parameters = new ExcParameters()
                {
                    _KEY = "@data_SCK",
                    _VALUE = data_SCK
                };
                paramss.Add(parameters);

                res = db.Script(query_new, paramss);

            }

            else if (action == "edit")
            {

                query_edit = "UPDATE [dbo].[tbl_Product_SubCategoryOptionKey] SET [SCOKName] = @value WHERE id_SCOK =@id";

                parameters = new ExcParameters()
                {
                    _KEY = "@id",
                    _VALUE = id
                };

                paramss.Add(parameters);

                parameters = new ExcParameters()
                {
                    _KEY = "@value",
                    _VALUE = value
                };

                paramss.Add(parameters);


                res = db.Script(query_edit, paramss);

            }

            return Content(res);
        }
        //////////////////////////////////////////////////// list SCK : get
        public ActionResult table_SCK()
        {
            string query_type;

            PDBC db = new PDBC("PandaMarketCMS", true);
            db.Connect();
            query_type = "SELECT [id_SCOK],[id_SC],[SCOKName],[ISDESABLED],[ISDelete],(SELECT [SCName]FROM [dbo].[tbl_Product_SubCategory]where [id_SC]=[tbl_Product_SubCategoryOptionKey].[id_SC])as 'name_SC' FROM [dbo].[tbl_Product_SubCategoryOptionKey]";
            // query_type = "SELECT [id_SC],[id_MC],[SCName],[ISDESABLED],[ISDelete],(select MCName from [dbo].[tbl_Product_MainCategory] where [id_MC]=[tbl_Product_SubCategory].[id_MC] ) as 'nameMC',( select count (id_SC) from [dbo].[tbl_Product_SubCategoryOptionKey] where id_SC =[dbo].[tbl_Product_SubCategory].[id_SC]) as 'count' FROM [dbo].[tbl_Product_SubCategory]";
            using (DataTable dt = db.Select(query_type))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    data_SCK = new SubCategoryOptionKey();

                    data_SCK.id_SCOK = dt.Rows[i]["id_SCOK"].ToString();
                    data_SCK.id_SC = dt.Rows[i]["name_SC"].ToString();
                    data_SCK.SCOKName = dt.Rows[i]["SCOKName"].ToString();
                    data_SCK.ISDelete = dt.Rows[i]["ISDelete"].ToString();
                    data_SCK.ISDESABLED = dt.Rows[i]["ISDESABLED"].ToString();
                    //data_SCK.count = dt.Rows[i]["count"].ToString();
                    list_SCK.Add(data_SCK);
                }
                ViewBag.SCKs = list_SCK;
            }

            return View();

        }
        /////////////////////////////////////////////////////////// list SCK : post
        [HttpPost]
        public ActionResult SCK_Switch(string action, string id)
        {

            string str = " ", query;

            PDBC db = new PDBC("PandaMarketCMS", true);
            db.Connect();

            List<ExcParameters> paramss = new List<ExcParameters>();
            ExcParameters parameters = new ExcParameters();

            parameters = new ExcParameters()
            {
                _KEY = "@id_SCOK",
                _VALUE = id
            };

            paramss.Add(parameters);

            if (action == "edit")
            {
                using (DataTable dt = db.Select($"SELECT [id_SCOK],[SCOKName] FROM [dbo].[tbl_Product_SubCategoryOptionKey] where [id_SCOK]= '{id}'"))
                {
                    model = new TypeASPX()
                    {
                        ChangeID = dt.Rows[0]["id_SCOK"].ToString(),
                        HasChange = true,
                        ChangeValue = dt.Rows[0]["SCOKName"].ToString()
                    };

                    Session["edit_SCK"] = model;
                    return RedirectToAction("New_SCK");

                };
            }
            else if (action == "delete")
            {
                parameters = new ExcParameters()
                {
                    _KEY = "@value",
                    _VALUE = "1"
                };
                paramss.Add(parameters);

                query = "UPDATE [dbo].[tbl_Product_SubCategoryOptionKey] SET [ISDelete] = @value , [DateDeleted] = GETDATE()  WHERE id_SCOK= @id_SCOK";

                str = db.Script(query, paramss);

            }
            else if (action == "off")
            {
                parameters = new ExcParameters()
                {
                    _KEY = "@value",
                    _VALUE = "1"
                };
                paramss.Add(parameters);

                query = "UPDATE [dbo].[tbl_Product_SubCategoryOptionKey] SET [ISDESABLED] = @value , [DateDesabled] = GETDATE() WHERE id_SCOK= @id_SCOK";

                str = db.Script(query, paramss);
            }
            else if (action == "on")
            {
                parameters = new ExcParameters()
                {
                    _KEY = "@value",
                    _VALUE = "0"
                };
                paramss.Add(parameters);

                query = "UPDATE [dbo].[tbl_Product_SubCategoryOptionKey] SET [ISDESABLED] = @value , [DateDesabled] = GETDATE() WHERE id_SCOK= @id_SCOK";

                str = db.Script(query, paramss);
            }
            return RedirectToAction("table_SCK");
        }
        ///------///////////////////////{   End SubCatKey   }//////////////////////////////
        ///
    }
}