using HtmlTags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Kenrapid.CRM.Web.Utilities;

namespace Kenrapid.CRM.Web.Helpers
{
    public class UIGridTag : HtmlTag
    {
        public class ColumnBuilder<T>
        {
            private readonly UIGridTag _tag;
            public ColumnBuilder(UIGridTag tag)
            {
                _tag = tag;
            }

            public void Add<TProp>(Expression<Func<T, TProp>> property,
                string columnHeader = null,
                string cellFitler = null)
            {
                _tag._columns.Add(new ColumnDefinition
                {
                    Field = property.ToCamelCaseName(),
                    Name = columnHeader,
                    CellFilter = cellFitler
                });
            }
        }

        private class ColumnDefinition
        {
            public string Field { get; set; }
            public string Name { get; set; }
            public string CellFilter { get; set; }
        }

        private readonly List<ColumnDefinition> _columns = new List<ColumnDefinition>();

        public UIGridTag(string dataUrl, string gridOptions)
            : base("mvc-grid")
        {
            Attr("grid-data-url", dataUrl);
            Attr("grid-options", gridOptions);
        }

        public new UIGridTag Title(string title)
        {
            Attr("title", title);
            return this;
        }

        public UIGridTag Columns<T>(Action<ColumnBuilder<T>> configAction)
        {
            var builder = new ColumnBuilder<T>(this);
            configAction(builder);
            return this;
        }

        protected override void writeHtml(System.Web.UI.HtmlTextWriter html)
        {
            if (_columns.Any())
            {
                this.Attr("columns", _columns.ToArray().ToJson(includeNull: false));
            }

            base.writeHtml(html);
        }
    }
}