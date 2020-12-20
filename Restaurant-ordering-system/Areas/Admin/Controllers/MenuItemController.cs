using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant_ordering_system.Contracts;
using Restaurant_ordering_system.Data;
using Restaurant_ordering_system.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_ordering_system.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuItemController : Controller
    {
        private readonly IMenuItemRepository _repo;
        private readonly ICategoryRepository _catRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;

        public MenuItemController(IMenuItemRepository repo,ICategoryRepository catRepo, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _repo = repo;
            _catRepo = catRepo;
            _webHostEnvironment = webHostEnvironment;
            _mapper= mapper;
        }
        

        // GET: MenuItemController
        public async Task<IActionResult> Index()
        {
            var obj = await _repo.FindAll();
            var objVm = _mapper.Map<List<AdminViewVM>>(obj);

            return View(objVm);
        }

        // GET: MenuItemController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var exists =await _repo.Exists(id);
            if (!exists)
            {
                return NotFound();
            }

            var obj =await _repo.FindById(id);
            
            var objVm =_mapper.Map<MenuItemVM>(obj);

            return View(objVm);
        }

        // GET: MenuItemController/Create
        public async Task<IActionResult> Create()
        {
            var categories = await _catRepo.FindAll();
            var categoryItems = categories.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });
            var model = new CreateMenuItemVM
            {
                Category = categoryItems
            };
            return View(model);
        }

        // POST: MenuItemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMenuItemVM model)
        {
            try
            {
                var categories = await _catRepo.FindAll();
                var categoryItems = categories.Select(q => new SelectListItem
                {
                    Text = q.Name,
                    Value = q.Id.ToString()
                });

                model.Category = categoryItems;
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                string uniqueFileName = null;

                if (model.Image != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                    string filepath = Path.Combine(uploadsFolder, uniqueFileName);
                    using(var fileStream=new FileStream(filepath, FileMode.Create))
                    {
                        model.Image.CopyTo(fileStream);
                    }
                }


                /*var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using(var fs1 = files[0].OpenReadStream())
                    {
                        using(var ms1=new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }
                    model.Picture = p1;
                }*/
                var newMenuItem = new MenuItemVM
                {
                    Name = model.Name,
                    Description = model.Description,
                    Spicyness = model.Spicyness,
                    Image=uniqueFileName,
                    CategoryId = model.CategoryId,
                    Price = model.Price
                };
                var obj = _mapper.Map<MenuItem>(newMenuItem);
                var success = await _repo.Create(obj);
                if (!success)
                {
                    ModelState.AddModelError("", "Something went wrong with request");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong with request");
                return View(model);
            }
        }

        // GET: MenuItemController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var obj = await _repo.FindById(id);
            if (obj == null)
            {
                return NotFound();
            }
            
            var categories = await _catRepo.FindAll();
            var categoryItems = categories.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });
            var model = new EditMenuItemVM
            {
                Name=obj.Name,
                Description=obj.Description,
                Spicyness=obj.Spicyness,
                Category = categoryItems,
                CategoryId=obj.CategoryId,
                Price=obj.Price,
                CurrentPicture=obj.Image

            };
            
            return View(model);
        }

        // POST: MenuItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EditMenuItemVM model)
        {
            try
            {
                var categories = await _catRepo.FindAll();
                var categoryItems = categories.Select(q => new SelectListItem
                {
                    Text = q.Name,
                    Value = q.Id.ToString()
                });

                model.Category = categoryItems;
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                string uniqueFileName = model.CurrentPicture;

                if (model.Image != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;

                    string filepath = Path.Combine(uploadsFolder, uniqueFileName);

                    //Delete old Image
                    var imagePath = Path.Combine(uploadsFolder, model.CurrentPicture);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }


                    using (var fileStream = new FileStream(filepath, FileMode.Create))
                    {
                        model.Image.CopyTo(fileStream);
                    }
                }

                var updateMenuItem = new MenuItemVM
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    Spicyness = model.Spicyness,
                    Image = uniqueFileName,
                    CategoryId = model.CategoryId,
                    Price = model.Price

                };

                var obj = _mapper.Map<MenuItem>(updateMenuItem);
                var success =await _repo.Update(obj);

                if (!success)
                {
                    ModelState.AddModelError("", "Something went wrong when updating");
                    return View(model);
                }
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong with request");
                return View(model);
            }
        }

        // GET: MenuItemController/Delete/5
        public async Task<IActionResult> Delete(int id, MenuItemVM model)
        {
                var obj = await _repo.FindById(id);
                if (obj == null)
                {
                    return NotFound();
                }

            if (obj.Image != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                var imagePath = Path.Combine(uploadsFolder, obj.Image);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
                
                var success = await _repo.Delete(obj);
                if (!success)
                {
                    return BadRequest();
                }
                

                return RedirectToAction(nameof(Index));

            
        }

        // POST: MenuItemController/Delete/5
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
