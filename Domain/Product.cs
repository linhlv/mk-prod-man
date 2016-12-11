using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Kenrapid.CRM.Web.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class Product : DomainObjectBase
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //[Required, StringLength(100)]
        //public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required, StringLength(30)]
        public string Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal StandardCost { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal ListPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Packaging { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal CMW { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal CMD { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal CMH { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal PCSSE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal CBM { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ColorId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int MaterialId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Material Material { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int VendorId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Vendor Vendor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<ProductImage> ProductImages { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public virtual List<ProductSize> ProductSizes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? SellStartDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? ShippingWeight { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? SellEndDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? DiscontinuedDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime LastPriceDate { get; set; }
    }
}
