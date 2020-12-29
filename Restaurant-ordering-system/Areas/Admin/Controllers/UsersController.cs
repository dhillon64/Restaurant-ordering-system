using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant_ordering_system.Data;
using Restaurant_ordering_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_ordering_system.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="Manager")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        // GET: UsersController
        public async Task<IActionResult> Index()
        {
           var managers= await _userManager.GetUsersInRoleAsync("Manager");
            var objVmManagers = _mapper.Map<List<UsersVM>>(managers);
            var customers = await _userManager.GetUsersInRoleAsync("Customer");
            var objVmCustomers = _mapper.Map<List<UsersVM>>(customers);
            var model = new UsersListVM
            {
                Managers = objVmManagers,
                Customers=objVmCustomers

            };

            return View(model);
        }

        // GET: UsersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsersController/RegisterEmployee
        public ActionResult CreateUser()
        {
            var roles = _roleManager.Roles;
            var roleTypesItems = roles.Select(q => new SelectListItem
            {
                Text=q.Name,
                Value=q.Name
            });

            var model = new RegistrationVM
            {
                RoleTypes = roleTypesItems
            };


            return View(model);
        }

        // POST: UsersController/RegisterEmployee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(RegistrationVM model)
        {
            try
            {
                var roles = _roleManager.Roles;
                var roleTypesItems = roles.Select(q => new SelectListItem
                {
                    Text = q.Name,
                    Value = q.Name
                });

                model.RoleTypes = roleTypesItems;

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var user = new ApplicationUser
                {
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    StreetAddress = model.StreetAddress,
                    City = model.City,
                    County = model.County,
                    PostCode = model.PostCode
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.Role);
                }
                else
                {
                    ModelState.AddModelError("", "Something went wrong with request");
                    return View(model);
                }



                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong when creating user");
                return View(model);
            }
        }

        // GET: UsersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
