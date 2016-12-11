using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kenrapid.CRM.Web.Domain;
using Kenrapid.CRM.Web.Infrastructure.Mapping;
using Kenrapid.CRM.Web.Models.Category;
using Kenrapid.CRM.Web.Models.Color;
using Kenrapid.CRM.Web.Models.Material;
using Kenrapid.CRM.Web.Models.Vendor;

namespace Kenrapid.CRM.Web.Models.Product
{
    public class AddProductForm : IMapTo<Kenrapid.CRM.Web.Domain.Product>
    {
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required, StringLength(10)]
        public string Code { get; set; }
        public decimal StandardCost { get; set; }
        [DataType("_Price"), DisplayName("Price")]
        public decimal ListPrice { get; set; }
        [DisplayName("Size")]
        public string Size { get; set; }
        public List<ProductSizeViewModel> Sizes { get; set; }
        [DisplayName("Packaging")]
        public string Packaging { get; set; }
        [DisplayName("CM-W"), DataType("_SizeNumber")]
        public decimal CMW { get; set; }
        [DisplayName("CM-D"), DataType("_SizeNumber")]
        public decimal CMD { get; set; }
        [DisplayName("CM-H"), DataType("_SizeNumber")]
        public decimal CMH { get; set; }
        [DisplayName("PCS/SE"), DataType("_SizeNumber")]
        public decimal PCSSE { get; set; }
        [DisplayName("CBM"), DataType("_SizeNumber")]
        public decimal CBM { get; set; }
        [DataType("_ImageFile")]
        public HttpPostedFileBase Picture { get; set; }
        [DataType("_Material"), DisplayName("Material")]
        public int MaterialId { get; set; }
        [DataType("_Color"), DisplayName("Color")]
        public int ColorId { get; set; }
        [DataType("_Category"), DisplayName("Category")]
        public int CategoryId { get; set; }
        [DataType("_Vendor"), DisplayName("Vendor")]
        public int VendorId { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LastPriceDate { get; set; }
    }
}