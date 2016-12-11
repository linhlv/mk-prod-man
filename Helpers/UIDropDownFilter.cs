using HtmlTags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kenrapid.CRM.Web.Helpers
{
    public class UIDropDownFilter : HtmlTag
    {
        public UIDropDownFilter(string model, Dictionary<int, string> keyValues)
            : base("select")
        {
           
            foreach(var keyValue in keyValues)
            {
                var htmlTag = new HtmlTag("option", this);
                htmlTag.Attr("value", keyValue.Key);
                htmlTag.Text(keyValue.Value);
            }
        }

        public UIDropDownFilter(string model, string ngOptionsExpression)
            : base("select")
        {
            Attr("class", "form-control");
            Attr("ng-model", model);
            Attr("ng-options", ngOptionsExpression);
        }

        public UIDropDownFilter NgChange(string expression)
        {
            Attr("ng-change", expression);
            return this;
        }
    }
}