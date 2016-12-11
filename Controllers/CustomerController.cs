using AutoMapper;
using Kenrapid.CRM.Web.Data;
using Kenrapid.CRM.Web.Domain;
using Kenrapid.CRM.Web.Models;
using Kenrapid.CRM.Web.ActionResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Kenrapid.CRM.Web.Infrastructure;

namespace Kenrapid.CRM.Web.Controllers
{
    public class CustomerController : KenrapidControllerBase
    {
        private readonly KenrapidDbContext _context;

        public CustomerController(KenrapidDbContext context)
        {
            _context = context;
        }


        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult All()
        {
            var customerModels = _context.Customers
                .OrderByDescending(o => o.LastModifiedDate)
                .Project().To<CustomerViewModel>();

            return JsonSuccess(customerModels.ToArray());
        }

        public JsonResult Add(AddCustomerForm form)
        {
            var customer = Mapper.Map<Customer>(form);
            customer.LastModifiedDate = DateTime.Now;
            _context.Customers.Add(customer);
            _context.SaveChanges();

            var model = Mapper.Map<CustomerViewModel>(customer);
            return JsonSuccess(model);
        }

        public JsonResult Update(EditCustomerForm form)
        {
            var target = _context.Customers.Find(form.Id);

            Mapper.Map(form, target);

            _context.SaveChanges();

            var updatedCustomer = _context.Customers.Project().To<CustomerViewModel>().Single(x => x.Id == form.Id);

            return JsonSuccess(updatedCustomer);
        }

    }
}