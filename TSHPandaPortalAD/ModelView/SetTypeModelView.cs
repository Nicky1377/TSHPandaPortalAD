using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSHPandaPortalAD.Models;

namespace TSHPandaPortalAD.ModelView
{
    public class SetTypeModelView
    {
        public bool HasChange { get; set; }
        public List<Type_Model> TypeModels { get; set; }
        public string Edit_Name { get; set; }
        public int Edit_Id { get; set; }

    }
}