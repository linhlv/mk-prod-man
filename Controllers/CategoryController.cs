﻿using Kenrapid.CRM.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Kenrapid.CRM.Web.Data;
using Kenrapid.CRM.Web.Models;
using Kenrapid.CRM.Web.Models.Category;
using Kenrapid.CRM.Web.Models.Material;
using Kenrapid.CRM.Web.Models.Vendor;

namespace Kenrapid.CRM.Web.Controllers
{
    public class CategoryController : KenrapidControllerBase
    {
        private readonly KenrapidDbContext _context;

        public CategoryController(KenrapidDbContext context)
		{
			_context = context;
		}


        // GET: Category
        public ActionResult Index()
        {
            var models = _context.Categories.Project().To<CategoryViewModel>().ToArray();
            return View(models);            
        }

        public JsonResult All()
        {
            var categories = _context.Categories
                .Project().To<CategoryViewModel>();
            return JsonSuccess(categories);
        }

        public JsonResult Search(DataFilterViewModel dataFilterViewModel)
        {
            var queryData = _context.Categories.AsQueryable();

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
                .Project().To<CategoryViewModel>();

            return JsonSuccess<PagedViewModel<CategoryViewModel>>(new PagedViewModel<CategoryViewModel>(models.ToList(), count));
        }

        public JsonResult Add(AddCategoryForm form)
        {
            var data = Mapper.Map<Kenrapid.CRM.Web.Domain.Category>(form);
            data.LastModifiedDate = DateTime.Now;
            _context.Categories.Add(data);
            _context.SaveChanges();

            var model = Mapper.Map<CategoryViewModel>(data);

            return JsonSuccess(model);
        }

        [HttpPost]
        public JsonResult Update(EditCategoryForm form)
        {
            var target = _context.Categories.Find(form.Id);

            Mapper.Map(form, target);

            target.LastModifiedDate = DateTime.Now;

            _context.SaveChanges();

            var result = _context.Categories.Project().To<CategoryViewModel>().Single(x => x.Id == form.Id);

            return JsonSuccess(result);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(CategoryViewModel data)
        {
            var removing = _context.Categories.FirstOrDefault(x => x.Id == data.Id);
            if (removing != null)
            {
                _context.Categories.Remove(removing);
                _context.SaveChanges();
                var model = Mapper.Map<CategoryViewModel>(removing);
                return JsonSuccess(model);
            }
            return JsonError("Category not found!");
        }
    }
}