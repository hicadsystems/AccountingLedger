
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NavyAccountCore.Core.Entities;
using NavyAccountWeb.IServices;
using NavyAccountWeb.ViewModels.Users;
using System;
using NavyAccountCore.Core.Extention;
using System.Linq;
using System.Threading.Tasks;
using NavyAccountCore.Core.AuditService;

namespace NavyAccountWeb.Controllers
{

    public class UserController : BaseController
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        public UserController(IUserService userService, IMapper mapper,
            IRoleService roleService):base(userService)
        {
            this.roleService = roleService;
            this.userService = userService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(new UsersViewModel
            {
                Users = userService.GetUsers().ToList(),
                Roles = roleService.GetActiveRoles()
            });
        }

        public async Task<IActionResult> Update(UserViewModel user)
        {
            if (user.Id > 0)
            {
                var updatedUser = await userService.GetUserById(user.Id);
                updatedUser.FirstName = user.FirstName;
                updatedUser.LastName = user.Lastname;
                updatedUser.UpdatedOn = DateTime.Now;
                updatedUser.Email = user.Email;
                updatedUser.UserName = user.UserName;

                var response = await userService.UpdateUserInfo(updatedUser, user.Roles, user.Device);
                if (response.Succeeded)
                {
                    TempData["SuccessMessage"] = "Successfully Updated User";
                    return RedirectToAction(nameof(Index));
                }
                TempData["ErrorMessage"] = response.Errors.First().Description;
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Cannot update new user";
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Create(UserViewModel user)
        {
            var newUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.Lastname,                
                UpdatedOn = DateTime.Now,
                Email = user.Email,
                CreatedOn = DateTime.Now,
                UserName = user.UserName,
                IsActive = true,
            };

            var createdUser = await userService.CreateUser(newUser, user.Roles, user.Password, user.Device);
            if (createdUser.Success)
            {
                TempData["SuccessMessage"] = "Successfully created User";
                return RedirectToAction(nameof(Index));
            }
                
            TempData["ErrorMessage"] = createdUser.Message;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("/user/{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await userService.GetUserRolesDevices(id);

            if (user == null)
            {
                TempData["ErrorMessage"] = "Period does not exist";
                return RedirectToAction(nameof(Index));
            }
            var Roles = user.UserRoles?.Select(x => x.RoleId).ToArray();
            
            return Json(new
            {
                User = user,
                Roles = user.UserRoles?.Select(x => x.RoleId).ToArray()
            }.ToResponse(null, true));
        }

        [Route("/user/togglestatus/{id}")]
        [HttpGet]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var user = await userService.GetUserById(id);

            user.IsActive = !user.IsActive;

            var toggled = await userService.UpdateUser(user);

            if (!toggled.Succeeded)
            {
                TempData["ErrorMessage"] = toggled.Errors.ToList().First().Description;
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = $"Status Change Successful";
            return RedirectToAction(nameof(Index));
        }
    }
}