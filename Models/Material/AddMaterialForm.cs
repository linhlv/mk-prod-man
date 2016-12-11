using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Kenrapid.CRM.Web.Domain;
using Kenrapid.CRM.Web.Infrastructure.Mapping;

namespace Kenrapid.CRM.Web.Models.Material
{
    public class AddMaterialForm : IMapTo<Kenrapid.CRM.Web.Domain.Material>
    {
        [Required, StringLength(200)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}