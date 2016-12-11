using Kenrapid.CRM.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kenrapid.CRM.Web.Controllers
{
    public class TemplateController : KenrapidControllerBase
    {
        public PartialViewResult Render(string feature, string name)
        {
            return PartialView(string.Format("~/app/{0}/templates/{1}", feature, name));
        }
    }
}