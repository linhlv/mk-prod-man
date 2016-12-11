using Kenrapid.CRM.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kenrapid.CRM.Web.Models.Vendor
{
    public class VendorViewModel : IMapFrom<Kenrapid.CRM.Web.Domain.Vendor>
    {
        public int Id { get; set; }        
        public string Name { get; set; }
        public string WorkEmail { get; set; }
        public string HomeEmail { get; set; }
        public string WorkPhone { get; set; }
        public string HomePhone { get; set; }
        public string HomeAddress { get; set; }
        public string WorkAddress { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
    }
}