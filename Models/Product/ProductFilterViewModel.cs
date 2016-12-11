using Kenrapid.CRM.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kenrapid.CRM.Web.Models.Product
{
    public class ProductFilterViewModel : PagedFilter
    {
        public string Keyword { get; set; }
        public int Factory { get; set; }
        public int Material { get; set; }
        public int Category { get; set; }
    }
}