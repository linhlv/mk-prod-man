using Kenrapid.CRM.Web.Data;
using Kenrapid.CRM.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Kenrapid.CRM.Web.Models;
using Kenrapid.CRM.Web.Models.Category;
using Kenrapid.CRM.Web.Models.Material;
using Kenrapid.CRM.Web.Models.Vendor;

namespace Kenrapid.CRM.Web.Controllers
{
    public class MaterialController : KenrapidControllerBase
    {
        private readonly KenrapidDbContext _context;

        public MaterialController(KenrapidDbContext context)
        {
            _context = context;
        }


        // GET: Category
        public ActionResult Index()
        {
            var models = _context.Materials.Project().To<MaterialViewModel>().ToArray();
            return View(models);
        }

        public JsonResult Search(DataFilterViewModel dataFilterViewModel)
        {
            var queryData = _context.Materials.AsQueryable();

            if (!string.IsNullOrWhiteSpace(dataFilterViewModel.Keyword))
            {
                var keyword = dataFilterViewModel.Keyword.ToLower();
                queryData = queryData.Where(
                    x => x.Name.ToLower().Contains(keyword)
                        || x.Description.ToLower().Contains(keyword)
                );
            }

            var count = queryData.Count();

            var models = queryData
                .OrderByDescending(o => o.LastModifiedDate)
                .Skip((dataFilterViewModel.Page - 1) * dataFilterViewModel.ItemsPerPage)
                .Take(dataFilterViewModel.ItemsPerPage)
                .Project().To<MaterialViewModel>();

            return JsonSuccess<PagedViewModel<MaterialViewModel>>(new PagedViewModel<MaterialViewModel>(models.ToList(), count));
        }

        public JsonResult All()
        {
            var data = _context.Materials
                .Project().To<MaterialViewModel>();
            return JsonSuccess(data);
        }

        [HttpPost]
        public JsonResult Add(AddMaterialForm form)
        {
            var data = Mapper.Map<Kenrapid.CRM.Web.Domain.Material>(form);
            data.LastModifiedDate = DateTime.Now;
            _context.Materials.Add(data);
            _context.SaveChanges();

            var model = Mapper.Map<MaterialViewModel>(data);

            return JsonSuccess(model);
        }

        [HttpPost]
        public JsonResult Update(EditMaterialForm form)
        {
            var target = _context.Materials.Find(form.Id);

            Mapper.Map(form, target);

            target.LastModifiedDate = DateTime.Now;

            _context.SaveChanges();

            var result = _context.Materials.Project().To<MaterialViewModel>().Single(x => x.Id == form.Id);

            return JsonSuccess(result);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(MaterialViewModel data)
        {
            var removing = _context.Materials.FirstOrDefault(x => x.Id == data.Id);
            if (removing != null)
            {
                _context.Materials.Remove(removing);
                _context.SaveChanges();
                var model = Mapper.Map<MaterialViewModel>(removing);
                return JsonSuccess(model);
            }
            return JsonError("Material not found!");
        }
    }
}