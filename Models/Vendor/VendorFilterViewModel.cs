using Kenrapid.CRM.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kenrapid.CRM.Web.Models.Vendor
{
    public class VendorFilterViewModel : PagedFilter
    {
        public string Keyword { get; set; }
       
    }
}