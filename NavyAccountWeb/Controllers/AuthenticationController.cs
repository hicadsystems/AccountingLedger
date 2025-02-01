using NavyAccountCore.Core.Extention;
using NavyAccountCore.Core.Models;
using NavyAccountWeb.IServices;
using NavyAccountWeb.ViewModels;
using NavyAccountWeb.Models.Api;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NavyAccountCore.Core.Data;
using Microsoft.AspNetCore.Authorization;
using NavyAccountCore.Core.Entities;
using System;
//using System.Collections.Generic;


namespace NavyAccountWeb.Controllers
{
    public class AuthenticationController : BaseController
    {
        private readonly INavyAccountDbContext context;
        private readonly IAuthenticationService authenticationService;
        private readonly IUserService userService;
        
        private readonly IFundTypeCodeService fundtypecodeService;
        private readonly IUnitOfWork unitOfWork;

        public AuthenticationController(INavyAccountDbContext context,IUnitOfWork unitOfWork ,IFundTypeCodeService fundtypecodeService,IAuthenticationService authenticationService, IUserService userService) :base(userService)
        {
            this.userService = userService;
            this.authenticationService = authenticationService;
            
            this.fundtypecodeService = fundtypecodeService;
            this.unitOfWork = unitOfWork;
            this.context = context;
        }

        // GET: Authentication
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            try
            {
            var auth = await authenticationService.SignInUserAsync(login.UserName, login.Password, "false");
            
            if (!auth.Success)
            {
                TempData["ErrorMessage"] = auth.Message;
                return new RedirectResult(RefererUrl());
            }
            //set session for fundtype
            //var roleo = context.UserRoles.Find(auth.Data.UserName).RoleId;

            var getfundtypecode = unitOfWork.fundTypeCode.Getfundtypebycode(x => x.Code == ""+login.fundtype);
            int fundid = getfundtypecode.Id;
            string fundcode = getfundtypecode.Code;
            string discription = getfundtypecode.Description;



            //var getfundtype= fundtypeService.GetFundTypeById(login.fundtype).Result;
            HttpContext.Session.SetString("fundtypedescription", discription);
            HttpContext.Session.SetString("fundtypecode", fundcode);
            HttpContext.Session.SetInt32("fundtypeid", fundid);

            HttpContext.Session.SetInt32("processingYear", getfundtypecode.processingYear);
            HttpContext.Session.SetInt32("processingMonth", getfundtypecode.processingMonth);


            return RedirectToAction("Index", "Home");

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //public async Task<IActionResult> ClientLogin(LoginViewModel login)
        //{
        //    var auth = await authenticationService.SignInUserAsync(login.EmailAddress, login.Password, "true");

        //    if (auth.Success)
        //    {
        //        var user = await userService.GetUserRolesDevices(auth.Data.Id);              

        //        var response = new ClientResponse
        //        {
        //            User = user
        //        };
        //        return Ok(response.ToResponse());
        //    }

        //    return NotFound(auth);
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await authenticationService.(model.Email);
        //        if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
        //        {
        //            // Don't reveal that the user does not exist or is not confirmed
        //            return View("ForgotPasswordConfirmation");
        //        }

        //        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
        //        // Send an email with this link
        //        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        //        var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
        //        await _emailSender.SendEmailAsync(model.Email, "Reset Password",
        //           $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
        //        return View("ForgotPasswordConfirmation");
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}
        

        public async Task<IActionResult> Logout()
        {
            await authenticationService.SignOutUserAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}