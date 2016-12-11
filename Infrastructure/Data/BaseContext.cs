using Kenrapid.CRM.Web.Domain;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Kenrapid.CRM.Web.Infrastructure.Data
{
    public class BaseContext<TContext>
                    : IdentityDbContext<ApplicationUser> where TContext : DbContext
    {
        static BaseContext()
        {
            Database.SetInitializer<TContext>(null);
        }

        protected BaseContext()
            : base("name=DefaultConnection")
        {
            this.Database.Connection.Open();
        }
    }
}