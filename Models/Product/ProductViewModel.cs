using Kenrapid.CRM.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Kenrapid.CRM.Web.Models.Category;
using Kenrapid.CRM.Web.Models.Color;
using Kenrapid.CRM.Web.Models.Material;
using Kenrapid.CRM.Web.Models.Vendor;

namespace Kenrapid.CRM.Web.Models.Product
{
    public class ProductViewModel : IMapFrom<Kenrapid.CRM.Web.Domain.Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal StandardCost { get; set; }
        [DataType("_Price"), DisplayName("Price")]
        public decimal ListPrice { get; set; }
        public int MaterialId { get; set; }
        public MaterialViewModel Material { get; set; }
        public int ColorId { get; set; }
        public ColorViewModel Color { get; set; }
        public int CategoryId { get; set; }
        public CategoryViewModel Category { get; set; }
        public int VendorId { get; set; }
        public VendorViewModel Vendor { get; set; }
        public List<ProductImageViewModel> ProductImages { get; set; }
        public List<ProductSizeViewModel> ProductSizes { get; set; }
        [Required, DisplayName("Size")]
        public string Size { get; set; }
        [Required, DisplayName("Packaging")]
        public string Packaging { get; set; }
        [Required, DisplayName("CM-W")]
        public decimal CMW { get; set; }
        [Required, DisplayName("CM-D")]
        public decimal CMD { get; set; }
        [Required, DisplayName("CM-H")]
        public decimal CMH { get; set; }
        [Required, DisplayName("PCS/SE")]
        public decimal PCSSE { get; set; }
        [Required, DisplayName("CBM")]
        public decimal CBM { get; set; }
        public string Description { get; set; }
        public bool QuotationSelected { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LastPriceDate { get; set; }
    }
}