﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSHPandaPortalAD.Models
{
    public class SubCategory
    {
        public string id_SC { get; set; }
        public string id_MC { get; set; }
        public string SCName { get; set; }
        public string ISDESABLED { get; set; }
        public string ISDelete { get; set; }
        public string DateDesabled { get; set; }
        public string DateDeleted { get; set; }
        public string count { get; set; }
    }
}