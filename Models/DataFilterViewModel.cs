using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kenrapid.CRM.Web.Infrastructure;

namespace Kenrapid.CRM.Web.Models
{
    public class DataFilterViewModel : PagedFilter
    {
        public string Keyword { get; set; }
    }
}