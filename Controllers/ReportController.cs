using Kenrapid.CRM.Web.Data;
using Kenrapid.CRM.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kenrapid.CRM.Web.Utilities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Kenrapid.CRM.Web.Models.Report;

namespace Kenrapid.CRM.Web.Controllers
{
    public class ReportController : KenrapidControllerBase
    {
        private readonly KenrapidDbContext _context;

        public ReportController(KenrapidDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult NewCustomers()
        {
            var startOfMonth = DateTime.Today.ToStartOfMonth();
            var endOfMonth = DateTime.Today.ToEndOfMonth();

            var customers = _context.Customers.Where(x => x.CreatedDate >= startOfMonth && x.CreatedDate <= endOfMonth)
                .Project().To<NewCustomerReportViewModel>().ToArray();

            return JsonSuccess(customers);
        }

        public JsonResult LostCustomers()
        {
            var startOfMonth = DateTime.Today.ToStartOfMonth();
            var endOfMonth = DateTime.Today.ToEndOfMonth();

            var customers = _context.Customers.Where(x => x.TerminationDate >= startOfMonth && x.TerminationDate <= endOfMonth)
                .Project().To<LostCustomerReportViewModel>().ToArray();

            return Json(customers);
        }
    }
}