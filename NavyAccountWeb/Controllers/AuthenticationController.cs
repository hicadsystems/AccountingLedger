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
using NavyAccountWeb.Models;
using NavyAccountWeb.Services;
using Microsoft.AspNetCore.Identity;
using System.Net;


namespace NavyAccountWeb.Controllers
{
    public class AuthenticationController : BaseController
    {
        private readonly INavyAccountDbContext context;
        private readonly IAuthenticationService authenticationService;
        private readonly IUserService userService;
        private readonly IMailService mailService;
        private readonly UserManager<User> userManager;
        private readonly IFundTypeCodeService fundtypecodeService;
        private readonly IUnitOfWork unitOfWork;

        public AuthenticationController(INavyAccountDbContext context, IUnitOfWork unitOfWork
            , IFundTypeCodeService fundtypecodeService, IAuthenticationService authenticationService,
            IUserService userService, IMailService mailService, UserManager<User> userManager) : base(userService)
        {
            this.userService = userService;
            this.mailService = mailService;
            this.userManager = userManager;
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

                var getfundtypecode = unitOfWork.fundTypeCode.Getfundtypebycode(x => x.Code == "" + login.fundtype);
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


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(string email)
        {

           

            try
            {
                var user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return View("ForgotPasswordConfirmation");
                }
                var code = await userManager.GeneratePasswordResetTokenAsync(user);
                var encodedCode = WebUtility.UrlEncode(code); // Ensure safe URL encoding
                var callbackUrl = Url.Action("ResetPassword", "Authentication", new { userId = user.Id, code = encodedCode }, protocol: HttpContext.Request.Scheme);

                var mail = new MailClass
                {
                    bodyText = $@"
                        Please reset your password by clicking the button below:  
                        {callbackUrl}",
                fromName = "NN-DNPF",
                    to = user.Email,
                    subject = "Password Reset Verification",
                    code = code
                };

                mailService.SendEmailTermii(mail);
            }
            catch (Exception ex)
            {
                // Log error (consider using ILogger)
                ModelState.AddModelError("", "There was an error sending the password reset email. Please try again later.");
                return View();
            }

            return View("ForgotPasswordConfirmation");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string userId, string code)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
            {
                return BadRequest("Invalid password reset request.");
            }

            var model = new ResetPasswordDataModel { UserId = userId, Code = code };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordDataModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return View("ResetPasswordConfirmation");
            }
            var decodedToken = WebUtility.UrlDecode(model.Code);
            var result = await userManager.ResetPasswordAsync(user, decodedToken, model.NewPassword);
            if (result.Succeeded)
            {
                return RedirectToAction("Index","Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await authenticationService.SignOutUserAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}