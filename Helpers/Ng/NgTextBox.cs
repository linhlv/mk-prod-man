using HtmlTags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kenrapid.CRM.Web.Helpers.Ng
{
    public class NgTextBox : HtmlTag
    {
        private readonly string[] _types = new string[] { "text", "email", "tel", "number", "password", "date" };

        public NgTextBox(string type)
            : base("input")
        {
            if (!string.IsNullOrWhiteSpace(type) && !_types.Contains(type.ToLower()))
            {
                throw new Exception("NgTextBox only support text, email, tel, number and password.");
            }
            else
            {
                Attr("type", type);
            }
        }

        public new NgTextBox Value(object value)
        {
            Attr("value", value);
            return this;
        }

        public NgTextBox Classes(string classes)
        {
            Attr("class", classes);
            return this;
        }

        public new NgTextBox Name(string name)
        {
            Attr("name", name);
            return this;
        }

        public NgTextBox NgModel(string model)
        {
            Attr("ng-model", model);
            return this;
        }

        public NgTextBox Watermark(string watermarkText)
        {
            Attr("placeholder", watermarkText);
            return this;
        }

        public NgTextBox IsRequired(bool required)
        {
            if (required)
                Attr("required", "required");
            return this;
        }

        public NgTextBox Pattern(string pattern)
        {
            if (!String.IsNullOrWhiteSpace(pattern))
                Attr("pattern", pattern);
            return this;
        }

    }
}