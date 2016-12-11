using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kenrapid.CRM.Web.Domain;
using Kenrapid.CRM.Web.Infrastructure.Mapping;

namespace Kenrapid.CRM.Web.Models.Category
{
    public class EditCategoryForm : IMapTo<Kenrapid.CRM.Web.Domain.Category>
    {
        [HiddenInput]
        [DataType("HiddenField")]
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}