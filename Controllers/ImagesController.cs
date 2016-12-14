using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Simple.ImageResizer.MvcExtensions;

namespace Kenrapid.CRM.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ImagesController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        [OutputCache(VaryByParam = "*", Duration = 60 * 60 * 24 * 365)]
        [AllowAnonymous]
        public ImageResult Index(string filename, int w = 0, int h = 0)
        {
            var filepath = Path.Combine(Server.MapPath("~/Content/data/images/prod"), filename);
            return new ImageResult(filepath, w, h);
        }
    }
}