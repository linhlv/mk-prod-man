using AutoMapper;
using Kenrapid.CRM.Web.Identity;
using Kenrapid.CRM.Web.Infrastructure;
using Kenrapid.CRM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Kenrapid.CRM.Web.Controllers
{
    public class ProfileController : KenrapidControllerBase
    {

        private readonly ApplicationUserManager _userManager;

        private readonly ICurrentUser _currentUser;

        public ProfileController(ApplicationUserManager userManager, ICurrentUser currentUser)
        {
            _userManager = userManager;
            _currentUser = currentUser;
        }

        public ActionResult Index()
        {
            var model = Mapper.Map<ProfileForm>(
                _userManager.FindById(_currentUser.User.Id));

            return View(model);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        public JsonResult Update(ProfileForm form)
        {
            var user = _userManager.FindById(_currentUser.User.Id);
            user.Email = form.EmailAddress;
            //user.UserName = form.FullName;
            _userManager.Update(user);
            return JsonSuccess(true);
        }
    }
}