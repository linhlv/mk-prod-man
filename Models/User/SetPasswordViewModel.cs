using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kenrapid.CRM.Web.Models.User
{
    public class SetPasswordViewModel
    {
        [HiddenInput]
        public string Id { get; set; }
        [Required]
        public string Password { get; set; }
    }
}