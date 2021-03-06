﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kenrapid.CRM.Web.Appiume.Extensions;
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
        /// <param name="nobg"></param>
        /// <returns></returns>
        [OutputCache(VaryByParam = "*", Duration = 60 * 60 * 24 * 365)]
        [AllowAnonymous]
        public ImagePresentingResult Index(string filename, int w = 0, int h = 0, bool nobg = false)
        {
            var filepath = Path.Combine(Server.MapPath("~/Content/data/images/prod"), filename);
            return new ImagePresentingResult(filepath, w, h, nobg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        [OutputCache(VaryByParam = "*", Duration = 60 * 60 * 24 * 365)]
        [AllowAnonymous]
        public ImagePresentingFixedFrameResult FixedFrame(string filename, int w = 0, int h = 0)
        {
            var filepath = Path.Combine(Server.MapPath("~/Content/data/images/prod"), filename);
            return new ImagePresentingFixedFrameResult(filepath, w, h);
        }
    }
}