using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Kenrapid.CRM.Web.Data;
using Kenrapid.CRM.Web.Infrastructure;
using Kenrapid.CRM.Web.Models;
using Kenrapid.CRM.Web.Models.Color;
using Kenrapid.CRM.Web.Models.Material;

namespace Kenrapid.CRM.Web.Controllers
{
    public class ColorController : KenrapidControllerBase
    {
        private readonly KenrapidDbContext _context;

        public ColorController(KenrapidDbContext context)
        {
            _context = context;
        }


        // GET: Category
        public ActionResult Index()
        {
            var models = _context.Colors.Project().To<ColorViewModel>().ToArray();
            return View(models);
        }

        public JsonResult Search(DataFilterViewModel dataFilterViewModel)
        {
            var queryData = _context.Colors.AsQueryable();

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
                .Project().To<ColorViewModel>();

            return JsonSuccess<PagedViewModel<ColorViewModel>>(new PagedViewModel<ColorViewModel>(models.ToList(), count));
        }

        public JsonResult All()
        {
            var data = _context.Colors
                .Project().To<ColorViewModel>();
            return JsonSuccess(data);
        }

        [HttpPost]
        public JsonResult Add(AddColorForm form)
        {
            var data = Mapper.Map<Kenrapid.CRM.Web.Domain.Color>(form);
            data.LastModifiedDate = DateTime.Now;
            _context.Colors.Add(data);
            _context.SaveChanges();

            var model = Mapper.Map<ColorViewModel>(data);

            return JsonSuccess(model);
        }

        [HttpPost]
        public JsonResult Update(EditColorForm form)
        {
            var target = _context.Colors.Find(form.Id);

            Mapper.Map(form, target);

            target.LastModifiedDate = DateTime.Now;

            _context.SaveChanges();

            var result = _context.Colors.Project().To<ColorViewModel>().Single(x => x.Id == form.Id);

            return JsonSuccess(result);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(ColorViewModel data)
        {
            var removing = _context.Colors.FirstOrDefault(x => x.Id == data.Id);
            if (removing != null)
            {
                _context.Colors.Remove(removing);
                _context.SaveChanges();
                var model = Mapper.Map<ColorViewModel>(removing);
                return JsonSuccess(model);
            }
            return JsonError("Color not found!");
        }
    }
}