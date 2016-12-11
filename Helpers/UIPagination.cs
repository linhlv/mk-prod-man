using HtmlTags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kenrapid.CRM.Web.Helpers
{
    public class UIPagination : HtmlTag
    {
        public UIPagination()
            : base("uib-pagination")
        {
        }

        public UIPagination Model(string page)
        {
            Attr("ng-model", page);
            return this;
        }

        public UIPagination TotalItems(string totalItems)
        {
            Attr("total-items", totalItems);
            return this;
        }
        public UIPagination ItemsPerPage(string itemsPerPage)
        {
            Attr("items-per-page", itemsPerPage);
            return this;
        }

        public UIPagination PreviousText(string previousText)
        {
            Attr("previous-text", previousText);
            return this;
        }

        public UIPagination NextText(string nextText)
        {
            Attr("next-text", nextText);
            return this;
        }

        public UIPagination FirstText(string firstText)
        {
            Attr("first-text", firstText);
            return this;
        }

        public UIPagination LastText(string lastText)
        {
            Attr("last-text", lastText);
            return this;
        }

        public UIPagination MaxSize(int maxSize)
        {
            Attr("max-size", maxSize);
            return this;
        }

        public UIPagination Rotate(bool rotate)
        {
            Attr("rotate", rotate.ToString().ToLower());
            return this;
        }

        public UIPagination BoundaryLinks(bool boundaryLinks)
        {
            Attr("boundary-links", boundaryLinks.ToString().ToLower());
            return this;
        }

        public UIPagination DirectionLinks(bool directionLinks)
        {
            Attr("direction-links", directionLinks.ToString().ToLower());
            return this;
        }

        public UIPagination OnPageChanged(string onPageChanged)
        {
            Attr("ng-change", onPageChanged);
            return this;
        }

        
    }
}