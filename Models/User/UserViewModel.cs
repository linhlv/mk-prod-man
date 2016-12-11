using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kenrapid.CRM.Web.Infrastructure.Mapping;

namespace Kenrapid.CRM.Web.Models.User
{
    public class UserViewModel : IMapFrom<Kenrapid.CRM.Web.Domain.ApplicationUser>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
    }
}