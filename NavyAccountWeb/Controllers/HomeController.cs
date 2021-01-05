using System;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NavyAccountCore.Core.AuditService;
using NavyAccountWeb.IServices;
using NavyAccountWeb.Models;

namespace NavyAccountWeb.Controllers
{
    
    public class HomeController : BaseController
    {

        private readonly IUserService userService;
        private readonly IConfiguration config;
        public HomeController(IUserService userService, IConfiguration config) : base(userService)
        {
            this.userService = userService;
            this.config = config;
        }


        public async Task<IActionResult> Index()
        {
            int rd;
            var currentUser = await GetCurrentUser();
            
           
            if (currentUser == null)
            {

                if (HttpContext.Session.GetString("Message") != null)
                {
                    ViewBag.message = HttpContext.Session.GetString("Message");
                }
                
                return View("Login");
            }
            var role = currentUser.UserRoles;
            foreach (var r in role)
            {
                rd = r.RoleId;
                HttpContext.Session.SetInt32("roleid", rd);
            }
            //Get session FundType
            ViewBag.Category = HttpContext.Session.GetString("fundtypedescription");
          

            return View();
            //return Redirect("User/index");

        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View("ForgotPassword");
        }

        [HttpPost]
        public async Task<ActionResult> ForgotPassword(string EmailID)
        {
            //Verify Email ID
            //Generate Reset password link 
            //Send Email 


            string message = "";


            var account = await userService.GetUserByEmail(EmailID);
            if (account != null)
            {
                //Send email for reset password
                string resetCode = Guid.NewGuid().ToString();
                SendVerificationLinkEmail(account.Email, resetCode, "ResetPassword");
                account.ResetPasswordCode = resetCode;
                //This line I have added here to avoid confirm password not match issue , as we had added a confirm password property 
                //in our model class in part 1
                await userService.UpdateUser(account);
                message = "Reset password link has been sent to your email id.";

            }
            else
            {
                message = "Account not found";
            }

            ViewBag.Message = message;
            return View();
        }

        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode, string emailFor = "ResetPassword")
        {
            var CentralServerAddress = config.GetValue<string>("ServerSettings:CentralServerAddress");
            var verifyUrl = "/Home/ResetPassword?id=" + activationCode;
            var link = $"{CentralServerAddress}{verifyUrl}";

           

            string subject = "";
            string body = "";
           
            
                subject = "Reset Password";
                body = "Hi,<br/>br/>We got request for reset your account password. Please click on the below link to reset your password" +
                    "<br/><br/><a href=" + link + ">Reset Password link</a>";
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("afolesmith@gmail.com", "Naval NPF");
            mail.To.Add(emailID);
            mail.Subject = subject;
            mail.Body = body;

            

            var host = config.GetValue<string>("mailConfig:host");
            var port = config.GetValue<int>("mailConfig:port");
            var username = config.GetValue<string>("mailConfig:username");
            var password = config.GetValue<string>("mailConfig:password");
            var from = config.GetValue<string>("mailConfig:from");
            var enableSSL = config.GetValue<bool>("mailConfig:enableSSL");
            var smtp = new SmtpClient
            {
                Host = host,
                Port = port,
                EnableSsl = enableSSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(username, password)
            };
            SmtpServer.Port = port; // Also Add the port number to send it, its default for Gmail
            SmtpServer.Credentials = new System.Net.NetworkCredential(username, password);
            SmtpServer.EnableSsl = enableSSL;
            SmtpServer.Timeout = 20000; // Add Timeout property
            SmtpServer.Send(mail);

            //using (var message = new MailMessage(fromEmail, toEmail)
            //{
            //    Subject = subject,
            //    Body = body,
            //    IsBodyHtml = true
            //})
            //    smtp.Send(message);
        }

        public ActionResult ResetPassword(string id)
        {
            //Verify the reset password link
            //Find account associated with this link
            //redirect to reset password page
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            
                var user = userService.GetUserByResetCode(id);
                if (user != null)
                {
                    ResetPasswordModel model = new ResetPasswordModel();
                    model.ResetCode = id;
                    return View(model);
                }
                else
                {
                    return NotFound();
                }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                
                    var user = await userService.GetUserByResetCode(model.ResetCode);
                    if (user != null)
                    {

                    var uuser = await userService.GetUserByUserName(user.UserName);
                    uuser.PasswordHash = HashPassword(model.NewPassword);
                    uuser.ResetPasswordCode = "";
                        await userService.UpdateUser(uuser);
                        message = "New password updated successfully";


                    HttpContext.Session.SetString("Message", message);
                    return RedirectToAction("Index", "Home");
                }
             }
            
            else
            {
                message = "Something invalid";
            }
            ViewBag.Message = message;
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        public IActionResult AccessControl()
        {
            return View();
        }
    }
}
