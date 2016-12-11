using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Kenrapid.CRM.Web.Data;
using Kenrapid.CRM.Web.Infrastructure;
using Kenrapid.CRM.Web.Models.Product;
using Kenrapid.CRM.Web.Models.Quotation;
using Kenrapid.CRM.Web.Models.Vendor;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Json;
using AutoMapper.Internal;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Kenrapid.CRM.Web.Domain;
using Kenrapid.CRM.Web.Models;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using Xdr = DocumentFormat.OpenXml.Drawing.Spreadsheet;
using A = DocumentFormat.OpenXml.Drawing;
using A14 = DocumentFormat.OpenXml.Office2010.Drawing;
using System.Configuration;

namespace Kenrapid.CRM.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class QuotationController : KenrapidControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly KenrapidDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        private readonly ICurrentUser _currentUser;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="currentUser"></param>
        public QuotationController(KenrapidDbContext context,
            ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        private string QuotationFileName
        {
            get
            {
                return Server.MapPath("~/Content/quotations/" + _currentUser.User.Id + ".qtdata");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private string QuotationXlsxFileName
        {
            get
            {
                return Server.MapPath("~/Content/quotations/" + _currentUser.User.Id + ".xlsx");
            }
        }
        
        /// <summary>
        /// Writes the given object instance to a binary file.
        /// <para>Object type (and all child types) must be decorated with the [Serializable] attribute.</para>
        /// <para>To prevent a variable from being serialized, decorate it with the [NonSerialized] attribute; cannot be applied to properties.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the XML file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the XML file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = System.IO.File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        /// <summary>
        /// Reads an object instance from a binary file.
        /// </summary>
        /// <typeparam name="T">The type of object to read from the XML.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the binary file.</returns>
        public static T ReadFromBinaryFile<T>(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                using (Stream stream = System.IO.File.Open(filePath, FileMode.Open))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    return (T)binaryFormatter.Deserialize(stream);
                }
            }

            return default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public QuotationModel EnsureModel()
        {
            var q = ReadFromBinaryFile<QuotationModel>(QuotationFileName) ?? new QuotationModel {
                CompanyName = string.Empty,
                Attn = string.Empty,
                QuotationItems = new List<QuotationItem>(),
                QuotationDate = DateTime.Now,
                DefaultRate = Convert.ToDecimal(ConfigurationManager.AppSettings["crm:DefaultRate"]),
                RateOfExchange = Convert.ToDecimal(ConfigurationManager.AppSettings["crm:RateOfExchange"])
            };            

            return q;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public FileResult Export()
        {
            var data = EnsureModel();
            if (data.HasQuantity)
            {
                new GeneratedClass2(data, Server.MapPath("~/Content/data/images/prod/")).CreatePackage(QuotationXlsxFileName);
            }
            else
            {
                new GeneratedClass(data, Server.MapPath("~/Content/data/images/prod/")).CreatePackage(QuotationXlsxFileName);
            }
            
            return File(QuotationXlsxFileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Quotation.xlsx");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        private string CopyFile(string source, string dest)
        {
            string result = "Copied file";
            try
            {
                // Overwrites existing files
                System.IO.File.Copy(source, dest, true);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        
        #region Data Operations
        /// <summary>
        /// 
        /// </summary>
        /// <param name="quotationModel"></param>
        /// <returns></returns>
        public JsonResult Save(QuotationModel quotationModel)
        {
            WriteToBinaryFile(QuotationFileName, quotationModel);

            return JsonSuccess(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult All()
        {
            var q = EnsureModel();

            return JsonSuccess(q);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public JsonResult Add(ProductViewModel form)
        {
            var q = EnsureModel();

            if (q.QuotationItems.Any(qi => qi.ProductId == form.Id))
            {

            }
            else
            {
                q.QuotationItems.Add(
                    new QuotationItem
                    {
                        ItemId = Guid.NewGuid(),
                        ProductId = form.Id,
                        CodeNo = form.Code,
                        Name = form.Code,
                        Size = form.Size,
                        Category = form.Category.Name,
                        Material = form.Material.Name,
                        Description = form.Description,
                        PackingPCSSE = form.PCSSE,
                        PackingCBM = form.CBM,
                        CartonMeasurementD = form.CMD,
                        CartonMeasurementH = form.CMH,
                        CartonMeasurementW = form.CMW,
                        Picture = form.ProductImages[0].ImageFileUrl,
                        Price = form.ListPrice,
                        PriceFOB = form.ListPrice,
                        Quantity = 1,
                        Rate = q.DefaultRate
                    }
                );

                WriteToBinaryFile(QuotationFileName, q);
            }

            return JsonSuccess(form);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public JsonResult Remove(ProductViewModel form)
        {
            var q = EnsureModel();

            if (q.QuotationItems.Any(qi => qi.ProductId == form.Id))
            {
                q.QuotationItems.RemoveAll(qi => qi.ProductId == form.Id);

                WriteToBinaryFile(QuotationFileName, q);
            }

            return JsonSuccess(form);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quotationItem"></param>
        /// <returns></returns>
        public JsonResult RemoveByProductId(QuotationItem quotationItem)
        {
            var q = EnsureModel();

            var model = q.QuotationItems.FirstOrDefault(x => x.ProductId == quotationItem.ProductId);
            if (q.QuotationItems.Any(qi => qi.ProductId == quotationItem.ProductId))
            {
                q.QuotationItems.RemoveAll(qi => qi.ProductId == quotationItem.ProductId);

                WriteToBinaryFile(QuotationFileName, q);
            }

            return JsonSuccess(model);
        }
        #endregion

    }
}