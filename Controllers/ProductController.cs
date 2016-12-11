using Kenrapid.CRM.Web.Data;
using Kenrapid.CRM.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Kenrapid.CRM.Web.Domain;
using Kenrapid.CRM.Web.Models;
using Kenrapid.CRM.Web.Models.Product;
using System.IO;
using Kenrapid.CRM.Web.Filters;
using Kenrapid.CRM.Web.Models.Quotation;

namespace Kenrapid.CRM.Web.Controllers
{
    public class ProductController : KenrapidControllerBase
    {
        private readonly KenrapidDbContext _context;

        private readonly ICurrentUser _currentUser;

        public List<QuotationItem> QuotationItems
        {
            get
            {
                return EnsureModel().QuotationItems;
            }
        }


        public ProductController(KenrapidDbContext context,
            ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }


        private string QuotationFileName
        {
            get
            {
                return Server.MapPath("~/Content/quotations/" + _currentUser.User.Id + ".qtdata");
            }
        }

        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public QuotationModel EnsureModel()
        {
            var q = QuotationController.ReadFromBinaryFile<QuotationModel>(QuotationFileName) ?? new QuotationModel { QuotationItems = new List<QuotationItem>() };

            if (q.QuotationItems == null)
            {
                q.QuotationItems = new List<QuotationItem>();
            }

            return q;
        }

        public JsonResult Search(ProductFilterViewModel productFilterViewModel)
        {
            var queryData = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(productFilterViewModel.Keyword))
            {
                var keyword = productFilterViewModel.Keyword.ToLower();
                queryData = queryData.Where(
                    x => x.Code.ToLower().Contains(keyword)
                   // || x.Name.ToLower().Contains(keyword)
                    || x.Size.ToLower().Contains(keyword)
                    || x.Packaging.ToLower().Contains(keyword)
                    || x.Description.ToLower().Contains(keyword)
                );
            }

            if (productFilterViewModel.Category > 0)
            {
                queryData = queryData.Where(x => x.CategoryId == productFilterViewModel.Category);
            }

            if (productFilterViewModel.Material > 0)
            {
                queryData = queryData.Where(x => x.MaterialId == productFilterViewModel.Material);
            }

            if (productFilterViewModel.Factory > 0)
            {
                queryData = queryData.Where(x => x.VendorId == productFilterViewModel.Factory);
            }


            var count = queryData.Count();

            var models = queryData
                .Include(o => o.ProductImages)
                .Include(o => o.ProductSizes)
                .OrderByDescending(o => o.LastModifiedDate)
                .Skip((productFilterViewModel.Page - 1) * productFilterViewModel.ItemsPerPage)
                .Take(productFilterViewModel.ItemsPerPage)
                .Project().To<ProductViewModel>();

            var list = models.ToList();

            foreach (ProductViewModel t in list)
            {
                var qItem = QuotationItems.FirstOrDefault(qi => qi.ProductId == t.Id);
                if (qItem != null)
                {
                    t.QuotationSelected = true;
                }
            }


        //    list[0].LastPriceDate = DateTime.Now;


            return JsonSuccess<PagedViewModel<ProductViewModel>>(new PagedViewModel<ProductViewModel>(list, count));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public JsonResult Delete(ProductViewModel data)
        {
            var product = _context.Products.Include(x=>x.ProductImages).FirstOrDefault(x => x.Id == data.Id);
            if (product != null)
            {
                if (product.ProductImages != null && product.ProductImages.Count > 0)
                {
                    foreach (var productImage in product.ProductImages.ToList())
                    {
                        var path = Server.MapPath("~/Content/data/images/prod/" + productImage.ImageFileUrl);
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                    }

                    _context.ProductImages.RemoveRange(product.ProductImages);
                }

                if (product.ProductSizes != null && product.ProductSizes.Count > 0)
                {
                    _context.ProductImages.RemoveRange(product.ProductImages);
                }

                _context.Products.Remove(product);
                _context.SaveChanges();
                var model = Mapper.Map<ProductViewModel>(product);
                return JsonSuccess(model);
            }
            return JsonError("Product not found!");
        }

        [HttpPost]
        public JsonResult AddProductImage(AddProductImage form)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == form.ProductId);
            if (product != null)
            {
                if (Request.Files.Count > 0)
                {
                    var fileGuid = Guid.Empty;
                    var fileName = string.Empty;

                    var fileContent = Request.Files[0];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        fileGuid = Guid.NewGuid();
                        var inputStream = fileContent.InputStream;
                        fileName = Path.GetFileName(fileContent.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/data/images/prod/"),
                            fileGuid.ToString() + "-" + fileName);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            inputStream.CopyTo(fileStream);
                        }

                        var result = _context.ProductImages.Add(new ProductImage
                        {
                            Guid = fileGuid,
                            ProductId = product.Id,
                            ImageFileUrl = fileGuid.ToString() + "-" + fileName
                        });

                        _context.SaveChanges();

                        var model = Mapper.Map<ProductImageViewModel>(result);

                        return JsonSuccess(model);
                    }

                    return JsonError("No file uploaded!");
                }
            }

            return JsonError("Product not found!");
        }

        [HttpPost]
        public JsonResult DeleteProductImage(int id)
        {
            var result = _context.ProductImages.FirstOrDefault(x => x.Id == id);
            if (result != null)
            {
                if (System.IO.File.Exists(Server.MapPath("~/Content/data/images/prod/" + result.ImageFileUrl)))
                {
                    var model = Mapper.Map<ProductImageViewModel>(result);
                    System.IO.File.Delete(Server.MapPath("~/Content/data/images/prod/" + result.ImageFileUrl));
                    _context.Entry(result).State = EntityState.Deleted;
                    _context.SaveChanges();
                    return JsonSuccess(model);
                }
            }

            return JsonError("Picture not found!");
        }

        [HttpPost, Log("Update price {form}")]

        public JsonResult UpdatePrice(ProductUpdatePriceViewModel form)
        {
            var data = _context.Products.FirstOrDefault(x => x.Id == form.Id);
            if (data != null)
            {
                data.ListPrice = form.ListPrice;
                data.LastModifiedDate = DateTime.UtcNow;
                data.LastPriceDate = form.LastPriceDate;
                _context.Entry(data).State = EntityState.Modified;
                _context.SaveChanges();
                return JsonSuccess(form);
            }
            return JsonError("Product not found!");
        }

        [HttpPost]
        public JsonResult Add(AddProductForm form)
        {
            var fileGuid = Guid.Empty;
            var fileName = string.Empty;
            if (Request.Files.Count > 0)
            {
                var fileContent = Request.Files[0];
                if (fileContent != null && fileContent.ContentLength > 0)
                {
                    fileGuid = Guid.NewGuid();
                    var inputStream = fileContent.InputStream;
                    fileName = Path.GetFileName(fileContent.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/data/images/prod/"), fileGuid.ToString() + "-" + fileName);
                    using (var fileStream = System.IO.File.Create(path))
                    {
                        inputStream.CopyTo(fileStream);
                    }
                }
            }

            var data = Mapper.Map<Product>(form);
            var dt = DateTime.Now;
            data.LastModifiedDate = dt;
            //data.LastPriceDate = dt;

            if (!String.IsNullOrEmpty(Request.Form["lastPriceDate"]))
            {
                data.LastPriceDate = DateTime.Parse(Request.Form["lastPriceDate"]);
            }

            var product = _context.Products.Add(data);

            if (!string.IsNullOrWhiteSpace(fileName))
            {
                _context.ProductImages.Add(new ProductImage
                {
                    Guid = Guid.NewGuid(),
                    ProductId = product.Id,
                    ImageFileUrl = fileGuid.ToString() + "-" + fileName
                });
            }


            var idx = 0;

            while (Request.Form["size_" + idx] != null)
            {
                var size = Convert.ToString(Request.Form["size_" + idx]);
                _context.ProductSizes.Add(new ProductSize()
                {
                    Value = size,
                    Product = product
                });
                idx++;
            }

            _context.SaveChanges();

            product = _context.Products
                    .Include(x => x.Color)
                    .Include(x => x.Material)
                    .Include(x => x.Vendor)
                    .Include(x => x.Category)
                .FirstOrDefault(x => x.Id == product.Id);

            var model = Mapper.Map<ProductViewModel>(product);
            return JsonSuccess(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetProductSizes(int productId)
        {
            var productSizes = _context.ProductSizes.Where(x => x.Product.Id == productId).Select(x => new ProductSizeViewModel
            {
                Id = x.Id,
                Value = x.Value
            }).ToList();


            return JsonSuccess(productSizes);
        }

        public JsonResult Update(EditProductForm form)
        {
            var target = _context.Products.Find(form.Id);

            var productSizes = target.ProductSizes.ToList();

            _context.ProductSizes.RemoveRange(productSizes);

            if (form.Sizes != null && form.Size.Any())
            {
                foreach (var size in form.Sizes)
                {
                    _context.ProductSizes.Add(new ProductSize()
                    {
                        Value = size.Value,
                        Product = target
                    });
                }
            }

            Mapper.Map(form, target);

            target.LastModifiedDate = DateTime.Now;

            _context.SaveChanges();

            var updatedData = _context.Products.Project().To<ProductViewModel>().Single(x => x.Id == form.Id);


            productSizes = target.ProductSizes.ToList();

            if (productSizes.Any())
            {
                updatedData.ProductSizes = new List<ProductSizeViewModel>();
                foreach (var size in productSizes)
                {
                    updatedData.ProductSizes.Add(new ProductSizeViewModel
                    {
                        Id= size.Id,
                        Value = size.Value
                    });
                }
            }

            return JsonSuccess(updatedData);
        }

    }
}