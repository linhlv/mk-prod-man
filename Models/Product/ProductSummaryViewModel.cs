using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kenrapid.CRM.Web.Infrastructure.Mapping;

namespace Kenrapid.CRM.Web.Models.Product
{
    public class ProductSummaryViewModel : IMapFrom<Kenrapid.CRM.Web.Domain.Product>
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