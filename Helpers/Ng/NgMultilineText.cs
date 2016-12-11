using HtmlTags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kenrapid.CRM.Web.Helpers.Ng
{
    public class NgMultilineText : HtmlTag
    {
        public NgMultilineText()
            : base("textarea")
        {
            
        }

        public NgMultilineText Classes(string classes)
        {
            Attr("class", classes);
            return this;
        }

        public new NgMultilineText Name(string name)
        {
            Attr("name", name);
            return this;
        }

        public NgMultilineText NgModel(string model)
        {
            Attr("ng-model", model);
            return this;
        }

        public NgMultilineText Watermark(string watermarkText)
        {
            Attr("placeholder", watermarkText);
            return this;
        }

        public NgMultilineText IsRequired(bool required)
        {
            if (required)
                Attr("required", "required");
            return this;
        }

        public NgMultilineText Pattern(string pattern)
        {
            if (!String.IsNullOrWhiteSpace(pattern))
                Attr("pattern", pattern);
            return this;
        }
    }
}