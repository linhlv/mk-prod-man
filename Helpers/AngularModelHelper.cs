using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Kenrapid.CRM.Web.Utilities;
using HtmlTags;

namespace Kenrapid.CRM.Web.Helpers
{
    public class AngularModelHelper<TModel>
    {
        protected readonly HtmlHelper Helper;
        private readonly string _expressionPrefix;

        public AngularModelHelper(HtmlHelper helper, string expressionPrefix)
        {
            Helper = helper;
            _expressionPrefix = expressionPrefix;
        }

        /// <summary>
        /// Converts an lambda expression into a camel-cased string, prefixed
        /// with the helper's configured prefix expression, ie:
        /// vm.model.parentProperty.childProperty
        /// </summary>
        public IHtmlString ExpressionFor<TProp>(Expression<Func<TModel, TProp>> property)
        {
            var expressionText = ExpressionForInternal(property);
            return new MvcHtmlString(expressionText);
        }

        /// <summary>
        /// Converts a lambda expression into a camel-cased AngularJS binding expression, ie:
        /// {{vm.model.parentProperty.childProperty}} 
        /// </summary>
        public IHtmlString BindingFor<TProp>(Expression<Func<TModel, TProp>> property)
        {
            return MvcHtmlString.Create("{{" + ExpressionForInternal(property) + "}}");
        }

        //<summary>
        //Creates a div with an ng-repeat directive to enumerate the specified property,
        //and returns a new helper you can use for strongly-typed bindings on the items
        //in the enumerable property.
        //</summary>
        public AngularNgRepeatHelper<TSubModel> Repeat<TSubModel>(
            Expression<Func<TModel, IEnumerable<TSubModel>>> property, string variableName)
        {
            var propertyExpression = ExpressionForInternal(property);
            return new AngularNgRepeatHelper<TSubModel>(
                Helper, variableName, propertyExpression);
        }

        private string ExpressionForInternal<TProp>(Expression<Func<TModel, TProp>> property)
        {
            var camelCaseName = property.ToCamelCaseName();

            var expression = !string.IsNullOrEmpty(_expressionPrefix)
                ? _expressionPrefix + "." + camelCaseName
                : camelCaseName;

            return expression;
        }

        public HtmlTag FormGroupPasswordTextBoxFor<TProp>(Expression<Func<TModel, TProp>> property, string compareTo)
        {
            var metadata = ModelMetadata.FromLambdaExpression(property,
                new ViewDataDictionary<TModel>());

            //Turns x => x.SomeName into "SomeName"
            var name = ExpressionHelper.GetExpressionText(property);
            var labelText = metadata.DisplayName ?? name;

            //Turns x => x.SomeName into vm.model.someName
            var expression = ExpressionForInternal(property);

            //Create <div class="form-group">
            var formGroup = new HtmlTag("div")
                    .AddClasses("form-group", "has-feedback");

            formGroup.Attr("form-group-validation", name);

            //Creates <label class="control-label" for="Name">Name</label>
            var label = new HtmlTag("label")
                    .AddClasses("control-label")
                    .Attr("for", name)
                    .Text(labelText);

            var placeHolder = metadata.Watermark ?? (labelText + "...");

            var textBox = new HtmlTag("input")
                 .AddClasses("form-control")
                 .Attr("type", "password")
                 .Attr("name", name)
                 .Attr("ng-model", expression)
                 .Attr("match", compareTo)
                 .Attr("placeholder", placeHolder);


            return formGroup
                .Append(label)
                .AppendHtml(textBox.ToHtmlString());
        }

        public HtmlTag FormGroupNewFor<TProp>(Expression<Func<TModel, TProp>> property)
        {
            var metadata = ModelMetadata.FromLambdaExpression(property,
                new ViewDataDictionary<TModel>());

            //Turns x => x.SomeName into "SomeName"
            var name = ExpressionHelper.GetExpressionText(property);
            var labelText = metadata.DisplayName ?? name;

            //Turns x => x.SomeName into vm.model.someName
            var expression = ExpressionForInternal(property);

            //Create <div class="form-group">
            var formGroup = new HtmlTag("div")
                    .AddClasses("form-group", "has-feedback");

            //if (!string.IsNullOrWhiteSpace(metadata.DataTypeName) && metadata.DataTypeName.Contains("ImageFile"))
            //{
            //    //skip file field
            //}
            //else
            //{
            //    formGroup.Attr("form-group-validation", name);
            //}

            formGroup.Attr("form-group-validation", name);

            //Creates <label class="control-label" for="Name">Name</label>
            var label = new HtmlTag("label")
                    .AddClasses("control-label")
                    .Attr("for", name)
                    .Text(labelText);

            var placeHolder = metadata.Watermark ?? (labelText + "...");

            var editor = Helper.Editor(metadata.PropertyName, AngularTemplateHelper.GetTemplateForProperty(metadata),
                new
                {
                    Prefix = _expressionPrefix,
                    name = name,
                    ng_model = expression,
                    placeholder = placeHolder
                });

            return formGroup
                .Append(label)
                .AppendHtml(editor.ToHtmlString());
        }


        public HtmlTag FormGroupNewFor<TProp>(Expression<Func<TModel, TProp>> property, string onChange)
        {
            var metadata = ModelMetadata.FromLambdaExpression(property,
                new ViewDataDictionary<TModel>());

            //Turns x => x.SomeName into "SomeName"
            var name = ExpressionHelper.GetExpressionText(property);
            var labelText = metadata.DisplayName ?? name;

            //Turns x => x.SomeName into vm.model.someName
            var expression = ExpressionForInternal(property);

            //Create <div class="form-group">
            var formGroup = new HtmlTag("div")
                    .AddClasses("form-group", "has-feedback");

            //if (!string.IsNullOrWhiteSpace(metadata.DataTypeName) && metadata.DataTypeName.Contains("ImageFile"))
            //{
            //    //skip file field
            //}
            //else
            //{
            //    formGroup.Attr("form-group-validation", name);
            //}

            formGroup.Attr("form-group-validation", name);

            //Creates <label class="control-label" for="Name">Name</label>
            var label = new HtmlTag("label")
                    .AddClasses("control-label")
                    .Attr("for", name)
                    .Text(labelText);

            var placeHolder = metadata.Watermark ?? (labelText + "...");

            var editor = Helper.Editor(metadata.PropertyName, AngularTemplateHelper.GetTemplateForProperty(metadata),
                new
                {
                    Prefix = _expressionPrefix,
                    name = name,
                    ng_model = expression,
                    ng_change = onChange,
                    placeholder = placeHolder
                });

            return formGroup
                .Append(label)
                .AppendHtml(editor.ToHtmlString());
        }


        public HtmlTag FormGroupFor<TProp>(Expression<Func<TModel, TProp>> property)
        {
            var metadata = ModelMetadata.FromLambdaExpression(property,
                new ViewDataDictionary<TModel>());

            //Turns x => x.SomeName into "SomeName"
            var name = ExpressionHelper.GetExpressionText(property);
            var labelText = metadata.DisplayName ?? name;

            //Turns x => x.SomeName into vm.model.someName
            var expression = ExpressionForInternal(property);
            //Create <div class="form-group">
            var formGroup = new HtmlTag("div")
                    .AddClasses("form-group", "has-feedback")
                    .Attr("form-group-validation", name);

            //Creates <label class="control-label" for="Name">Name</label>
            var label = new HtmlTag("label")
                    .AddClasses("control-label")
                    .Attr("for", name)
                    .Text(labelText);

            var tagName = metadata.DataTypeName == "MultilineText"
                ? "textarea"
                : "input";

            var placeHolder = metadata.Watermark ?? (labelText + "...");

            //Creates <input ng-model="expression"
            //          class="form-control" name="Name" type="text>
            var input = new HtmlTag(tagName)
                    .AddClasses("form-control")
                    .Attr("ng-model", expression)
                    .Attr("name", name)
                    .Attr("placeholder", placeHolder)
                    .Attr("type", "text");

            ApplyValidationToInput(input, metadata);

            return formGroup
                .Append(label)
                .Append(input);
        }

        private void ApplyValidationToInput(HtmlTag input, ModelMetadata metadata)
        {
            if (metadata.IsRequired)
                input.Attr("required", "");

            if (metadata.DataTypeName == "EmailAddress")
                input.Attr("type", "email");

            if (metadata.DataTypeName == "PhoneNumber")
            {
                input.Attr("pattern", @"[\0-9()-]+");
            }
        }
    }
}