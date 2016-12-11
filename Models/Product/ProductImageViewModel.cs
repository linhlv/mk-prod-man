using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kenrapid.CRM.Web.Infrastructure.Mapping;

namespace Kenrapid.CRM.Web.Models.Product
{
    public class ProductImageViewModel : IMapFrom<Kenrapid.CRM.Web.Domain.ProductImage>
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public int ProductId { get; set; }
        public string ImageFileUrl { get; set; }
    }
}