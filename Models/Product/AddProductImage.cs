using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kenrapid.CRM.Web.Infrastructure.Mapping;

namespace Kenrapid.CRM.Web.Models.Product
{
    public class AddProductImage : IMapTo<Kenrapid.CRM.Web.Domain.ProductImage>
    {
        public int ProductId { get; set; }
    }
}