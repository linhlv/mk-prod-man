using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Kenrapid.CRM.Web.Helpers
{
    public static class BootstrapHelpers
    {
        public static IHtmlString BootstrapLabelFor<TModel, TProp>(
                this HtmlHelper<TModel> helper,
                Expression<Func<TModel, TProp>> property
        )
        {
            var name = ExpressionHelper.GetExpressionText(property);
            return helper.LabelFor(property, new
            {
                @class = "control-label",
                @for = name
            });
        }

        public static IHtmlString BootstrapLabel(this HtmlHelper helper, string propertyName)
        {
            return helper.Label(propertyName, new
            {
                @class = "control-label",
                @for=propertyName
            });
        }

        public static string IsBrowserClass(this HtmlHelper helper)
        {
            return helper.ViewContext.RequestContext.HttpContext.Request.Browser.Id.Contains("safari") ? "safari-st" : "" ;
        }
    }
}