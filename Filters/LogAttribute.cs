﻿using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Kenrapid.CRM.Web.Domain;
using Kenrapid.CRM.Web.Infrastructure;

namespace Kenrapid.CRM.Web.Filters
{
	public class LogAttribute : ActionFilterAttribute
	{
		private IDictionary<string, object> _parameters;
        public Kenrapid.CRM.Web.Data.KenrapidDbContext Context { get; set; }
		public ICurrentUser CurrentUser { get; set; }
         
		public string Description { get; set; }

		public LogAttribute(string description)
		{
			Description = description;
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			_parameters = filterContext.ActionParameters;
			base.OnActionExecuting(filterContext);
		}
		
		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			var description = Description;

			foreach (var kvp in _parameters)
			{
				description = description.Replace("{" + kvp.Key + "}", kvp.Value.ToString());
			}

			Context.Logs.Add(new LogAction(CurrentUser.User, filterContext.ActionDescriptor.ActionName,
				filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, description));

			Context.SaveChanges();
		}
	}
}