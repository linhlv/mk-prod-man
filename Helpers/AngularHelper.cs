using HtmlTags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Kenrapid.CRM.Web.Helpers.Ng;
using Microsoft.Web.Mvc;

namespace Kenrapid.CRM.Web.Helpers
{
    public static class AngularHelperExtension
    {
        public static AngularHelper<TModel> Angular<TModel>(this HtmlHelper<TModel> helper)
        {
            return new AngularHelper<TModel>(helper);
        }
    }

    public class AngularHelper<TModel>
    {
        private readonly HtmlHelper<TModel> _htmlHelper;

        public AngularHelper(HtmlHelper<TModel> helper)
        {
            _htmlHelper = helper;
        }

        public IHtmlString EditorForModel(string modelPrefix)
        {
            return _htmlHelper.EditorForModel("Angular/Object",
                new
                {
                    Prefix = modelPrefix
                });
        }

        public IHtmlString BindingForModel()
        {
            var prefix = (string)(_htmlHelper.ViewBag.Prefix);
            if (prefix != null)
            {
                prefix = prefix + ".";
            }

            return MvcHtmlString.Create(prefix + _htmlHelper.CamelCaseIdForModel());
        }
      
        public AngularModelHelper<TModel> ModelFor(string expressionPrefix)
        {
            return new AngularModelHelper<TModel>(_htmlHelper, expressionPrefix);
        }

        public HtmlTag FormForModel(string expressionPrefix)
        {
            var modelHelper = ModelFor(expressionPrefix);
            var formGroupForMethodGeneric = typeof(AngularModelHelper<TModel>)
                .GetMethod("FormGroupFor");
            var wrapperTag = new HtmlTag("div").NoTag();

            foreach (var prop in typeof(TModel).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (prop.GetCustomAttributes().OfType<HiddenInputAttribute>().Any()) continue;

                var formGroupForProp = formGroupForMethodGeneric
                                        .MakeGenericMethod(prop.PropertyType);
                var propertyLamda = MakeLamda(prop);
                var formGroupTag = (HtmlTag)formGroupForProp.Invoke(modelHelper, new[] { propertyLamda });
                wrapperTag.Append(formGroupTag);
            }
            return wrapperTag;
        }

        //Construct a lamda of the form x=>x.PropName
        private object MakeLamda(PropertyInfo prop)
        {
            var parameter = Expression.Parameter(typeof(TModel), "x");
            var property = Expression.Property(parameter, prop);
            var funcType = typeof(Func<,>).MakeGenericType(typeof(TModel), prop.PropertyType);

            //x => x.PropName
            return Expression.Lambda(funcType, property, parameter);
        }

        public UIRatingTag UIRating(string model)
        {
            return new UIRatingTag(model);
        }

        public UIPagination UIPagination()
        {
            return new UIPagination();
        }


        public UIDropDownFilter UIDropDownFilter(string model, Dictionary<int, string> keyValues)
        {
            return new UIDropDownFilter(model, keyValues);
        }

        public UIDropDownFilter UIDropDownFilter(string model, string ngOptionsExpression)
        {
            return new UIDropDownFilter(model, ngOptionsExpression);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">Input type: text/email/tel/password.</param>
        /// <returns></returns>
        public NgTextBox NgTextBox(string type)
        {
            return new NgTextBox(type);
        }

        public NgMultilineText NgMultilineText()
        {
            return new NgMultilineText();
        }

        public UIGridTag UIGridTagFor<TController>(Expression<Action<TController>> targetAction, string gridOptions)
           where TController : Controller
        {
            var dataUrl = _htmlHelper.BuildUrlFromExpression(targetAction);
            return new UIGridTag(dataUrl, gridOptions);
        }
    }
}