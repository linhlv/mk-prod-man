using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Kenrapid.CRM.Web.Data;
using Kenrapid.CRM.Web.Domain;
using Kenrapid.CRM.Web.Infrastructure;
using Kenrapid.CRM.Web.Models;
using Kenrapid.CRM.Web.Models.Material;
using Kenrapid.CRM.Web.Models.User;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Kenrapid.CRM.Web.Controllers
{
    public class UserManagementController : KenrapidControllerBase
    {
        private readonly KenrapidDbContext _context;
        public UserManager<ApplicationUser> UserManager { get; private set; }
        public UserStore<ApplicationUser> UserStore { get; private set; }

        private readonly ICurrentUser _currentUser;

        public UserManagementController(KenrapidDbContext context, UserManager<ApplicationUser> userManager, UserStore<ApplicationUser> userStore, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
            UserManager = userManager;
            UserStore = userStore;
        }

        // GET: UserManagement
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Create(AddUserViewModel addUserViewModel)
        {
            var user = new ApplicationUser() { UserName = addUserViewModel.UserName };
            var result = UserManager.Create(user, addUserViewModel.Password);

            if (result.Succeeded)
            {
                return JsonSuccess(new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName
                });
            }
            else
            {
                return JsonError(result.Errors.FirstOrDefault());
            }
        }

        public JsonResult Delete(UserViewModel viewModel)
        {
            var user = UserManager.FindById(viewModel.Id);
            if (user != null)
            {
                UserManager.Delete(user);
                return JsonSuccess(viewModel);
            }

            return JsonSuccess(false);
        }

        public JsonResult ChangePassword(ChangePasswordForm form)
        {
            var result = UserManager.ChangePassword(User.Identity.GetUserId(), form.CurrentPassword, form.ConfirmedPassword);

            //var result = UserManager.AddPassword(_currentUser.User.Id, form.ConfirmedPassword);

            if (result.Succeeded)
            {
                return JsonSuccess(true);
            }

            return JsonError(result.Errors.FirstOrDefault());
        }
        public JsonResult SetPassword(SetPasswordViewModel setPasswordViewModel)
        {
            ApplicationUser cUser = UserManager.FindById(setPasswordViewModel.Id);
            cUser.SecurityStamp = Guid.NewGuid().ToString();
            var hashedNewPassword = UserManager.PasswordHasher.HashPassword(setPasswordViewModel.Password);
            var result = UserStore.SetPasswordHashAsync(cUser, hashedNewPassword);
            var updateResult = UserStore.UpdateAsync(cUser);

            return JsonSuccess(true);
        }

        public JsonResult Search(DataFilterViewModel dataFilterViewModel)
        {
            var queryData = _context.Users.AsQueryable();

            queryData = queryData.Where(x => (x.UserName != "admin"));

            if (!string.IsNullOrWhiteSpace(dataFilterViewModel.Keyword))
            {
                var keyword = dataFilterViewModel.Keyword.ToLower();
                queryData = queryData.Where(
                    x => x.UserName.ToLower().Contains(keyword)
                        || x.Email.ToLower().Contains(keyword)
                );
            }

            var count = queryData.Count();

            var models = queryData
                .OrderBy(o => o.Id)
                .Skip((dataFilterViewModel.Page - 1) * dataFilterViewModel.ItemsPerPage)
                .Take(dataFilterViewModel.ItemsPerPage)
                .Project().To<UserViewModel>();

            return JsonSuccess<PagedViewModel<UserViewModel>>(new PagedViewModel<UserViewModel>(models.ToList(), count));
        }
    }
}