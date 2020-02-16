using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSHPandaPortalAD.Models
{
    public class Type_Model
    {
        public int  num { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Disabled { get; set; }
        public bool Deleted { get; set; }
        public int Count { get; set; }
    }
}