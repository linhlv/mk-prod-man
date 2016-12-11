using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Kenrapid.CRM.Web.Data;
using Kenrapid.CRM.Web.Infrastructure;
using Kenrapid.CRM.Web.Models.Opportunity;
using Kenrapid.CRM.Web.Models;
using Kenrapid.CRM.Web.Domain;

namespace Kenrapid.CRM.Web.Controllers
{
    public class OpportunityController : KenrapidControllerBase
	{
        private readonly KenrapidDbContext _context;

        public OpportunityController(KenrapidDbContext context)
		{
			_context = context;
		}

		public ViewResult Index()
		{
			var models = _context.Opportunities.Project().To<OpportunityViewModel>().ToArray();
			return View(models);
		}

		public JsonResult Add(AddOpportunityForm form)
		{
			var customer = _context.Customers.Include(x => x.Opportunities).Single(x => x.Id == form.CustomerId);

			var opportunity = Mapper.Map<Opportunity>(form);

            opportunity.LastModifiedDate = DateTime.Now;

			customer.Opportunities.Add(opportunity);

			_context.SaveChanges();

			var model = Mapper.Map<CustomerOpportunityViewModel>(opportunity);

			return JsonSuccess(model);
		}
	}
}