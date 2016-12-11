using Kenrapid.CRM.Web.Data;
using Kenrapid.CRM.Web.Models.Vendor;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Kenrapid.CRM.Web.Infrastructure;
using Kenrapid.CRM.Web.Models.Product;

namespace Kenrapid.CRM.Web.Controllers
{
    public class VendorController : KenrapidControllerBase
    {
        private readonly KenrapidDbContext _context;

        public VendorController(KenrapidDbContext context)
        {
            _context = context;
        }


        // GET: Vendor
        public ActionResult Index()
        {
            var models = _context.Vendors.Project().To<VendorViewModel>().ToArray();
            return View(models);
        }

        public JsonResult All()
        {
            var queryData = _context.Vendors.AsQueryable();
            var vendors = queryData.Project().To<VendorViewModel>().ToList();
            return JsonSuccess(vendors);
        }

        public JsonResult Search(VendorFilterViewModel vendorFilterViewModel)
        {
            var queryData = _context.Vendors.AsQueryable();

            if (!string.IsNullOrWhiteSpace(vendorFilterViewModel.Keyword))
            {
                var keyword = vendorFilterViewModel.Keyword.ToLower();
                queryData = queryData.Where(
                    x => x.Name.ToLower().Contains(keyword)
                        || x.Description.ToLower().Contains(keyword)
                        || x.HomeEmail.ToLower().Contains(keyword)
                        || x.WorkEmail.ToLower().Contains(keyword)
                        || x.HomePhone.ToLower().Contains(keyword)
                        || x.WorkPhone.ToLower().Contains(keyword)
                        || x.HomeAddress.ToLower().Contains(keyword)
                        || x.WorkAddress.ToLower().Contains(keyword)
                );
            }

            var count = queryData.Count();

            var models = queryData
                .OrderByDescending(o => o.LastModifiedDate)
                .Skip((vendorFilterViewModel.Page - 1) * vendorFilterViewModel.ItemsPerPage)
                .Take(vendorFilterViewModel.ItemsPerPage)
                .Project().To<VendorViewModel>();

            return JsonSuccess<PagedViewModel<VendorViewModel>>(new PagedViewModel<VendorViewModel>(models.ToList(), count));
        }

        [HttpPost]
        public JsonResult Add(AddVendorForm form)
        {
            var vendor = Mapper.Map<Kenrapid.CRM.Web.Domain.Vendor>(form);
            vendor.LastModifiedDate = DateTime.Now;
            _context.Vendors.Add(vendor);
            _context.SaveChanges();

            var model = Mapper.Map<VendorViewModel>(vendor);

            return JsonSuccess(model);
        }

        [HttpPost]
        public JsonResult Update(EditVendorForm form)
        {
            var target = _context.Vendors.Find(form.Id);

            Mapper.Map(form, target);

            target.LastModifiedDate = DateTime.Now;

            _context.SaveChanges();

            var result = _context.Vendors.Project().To<VendorViewModel>().Single(x => x.Id == form.Id);

            return JsonSuccess(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(VendorViewModel data)
        {
            var removing = _context.Vendors.FirstOrDefault(x => x.Id == data.Id);
            if (removing != null)
            {
                _context.Vendors.Remove(removing);
                _context.SaveChanges();
                var model = Mapper.Map<VendorViewModel>(removing);
                return JsonSuccess(model);
            }
            return JsonError("Vendor not found!");
        }
    }
}