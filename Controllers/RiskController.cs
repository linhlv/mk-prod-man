using Kenrapid.CRM.Web.Data;
using Kenrapid.CRM.Web.Models.Risk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Kenrapid.CRM.Web.Infrastructure;
using HeroicCRM.Web.Models;

namespace Kenrapid.CRM.Web.Controllers
{
    public class RiskController : KenrapidControllerBase
    {
        private readonly KenrapidDbContext _context;

        public RiskController(KenrapidDbContext context)
        {
            _context = context;
        }

        public ViewResult Index()
        {
            var models = _context.Risks.Project().To<RiskViewModel>().ToArray();
            return View(models);
        }

        public JsonResult Add(AddRiskForm form)
        {
            var customer = _context.Customers.Include(x => x.Risks).Single(x => x.Id == form.CustomerId);

            var risk = Mapper.Map<Kenrapid.CRM.Web.Domain.Risk>(form);

            customer.Risks.Add(risk);

            _context.SaveChanges();

            var model = Mapper.Map<CustomerRiskViewModel>(risk);

            return JsonSuccess(model);
        }
    }
}