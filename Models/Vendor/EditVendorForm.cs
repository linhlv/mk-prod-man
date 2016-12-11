using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kenrapid.CRM.Web.Domain;
using Kenrapid.CRM.Web.Infrastructure.Mapping;

namespace Kenrapid.CRM.Web.Models.Vendor
{
    public class EditVendorForm : IMapTo<Kenrapid.CRM.Web.Domain.Vendor>
    {
        [HiddenInput]
        [DataType("HiddenField")]
        public int Id { get; set; }
        [Required, Display(Name = "Vendor Name",
            Prompt = "Vendor Name (ex: Artex Inc)")]
        public string Name { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string WorkEmail { get; set; }
        [DataType(DataType.EmailAddress)]
        public string HomeEmail { get; set; }
        [Required, DataType(DataType.PhoneNumber)]
        public string WorkPhone { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string HomePhone { get; set; }
        [DataType(DataType.MultilineText)]
        public string HomeAddress { get; set; }
        [Required, DataType(DataType.MultilineText)]
        public string WorkAddress { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}