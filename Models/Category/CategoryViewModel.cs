using Kenrapid.CRM.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kenrapid.CRM.Web.Models.Category
{
    public class CategoryViewModel : IMapFrom<Kenrapid.CRM.Web.Domain.Category>
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}