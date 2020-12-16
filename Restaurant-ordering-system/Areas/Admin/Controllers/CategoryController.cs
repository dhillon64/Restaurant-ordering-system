using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurant_ordering_system.Contracts;
using Restaurant_ordering_system.Data;
using Restaurant_ordering_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_ordering_system.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET-Categories
        public async Task<IActionResult> Index()
        {
            var obj =await _repo.FindAll();
            var objVm = _mapper.Map<List<CategoryVM>>(obj);
            return View(objVm);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(CategoryVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var exists = await _repo.Exists(model.Name);
                if (exists)
                {
                    ModelState.AddModelError("", "Category already exists");
                    return View(model);
                }

                var obj = _mapper.Map<Category>(model);
                var success = await _repo.Create(obj);
                if (!success)
                {
                    ModelState.AddModelError("", "Something went wrong with request");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong with request");
                return View(model);
            }

        }
        //GET- Edit
        public async Task<IActionResult> Edit(int id)
        {
            var exists = await _repo.Exists(id);
            if (!exists)
            {
                return NotFound();
            }

            var obj = await _repo.FindById(id);
            var objVm = _mapper.Map<CategoryVM>(obj);

            return View(objVm);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(CategoryVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var obj = _mapper.Map<Category>(model);
                var success = await _repo.Update(obj);
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
                return View();
            }
        }

        //GET- Delete
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _repo.Exists(id);
            if (!exists)
            {
                return NotFound();
            }

            var obj = await _repo.FindById(id);
            var objVm = _mapper.Map<CategoryVM>(obj);

            return View(objVm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(int Id, CategoryVM model)
        {
            try
            {
                var obj = await _repo.FindById(Id);
                if (obj == null)
                {
                    return NotFound();
                }

                var deleted = await _repo.Delete(obj);
                if (!deleted)
                {
                    ModelState.AddModelError("", "Something went wrong when delteting item");
                    return View(model);

                }
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong");
                return View(model);

            }
        }





    }
}
