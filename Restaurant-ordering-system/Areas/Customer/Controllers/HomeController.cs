using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restaurant_ordering_system.Contracts;
using Restaurant_ordering_system.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_ordering_system.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMenuItemRepository _repo;
        private readonly ICategoryRepository _catRepo;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger,IMenuItemRepository repo, ICategoryRepository catRepo, IMapper mapper)
        {
            _logger = logger;
            _repo = repo;
            _catRepo = catRepo;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // GET: HomeController/GetMenu
        public async Task<IActionResult> MenuItems()
        {
            var categoriesObj = await _catRepo.FindAll();
            var categoriesVm = _mapper.Map<List<CategoryVM>>(categoriesObj);

            var starterCategory = await _catRepo.FindByName("Starter");
            var MenuItemStarters = await _repo.GetMenuItemsByCategory(starterCategory.Id);
            var MenuItemStartersVm = _mapper.Map<List<MenuItemVM>>(MenuItemStarters);



            var mainCourseCategory = await _catRepo.FindByName("Main Course");
            var menuItemMainCourse = await _repo.GetMenuItemsByCategory(mainCourseCategory.Id);
            var MenuItemMainCourseVm = _mapper.Map<List<MenuItemVM>>(menuItemMainCourse);

            var dessertCategory = await _catRepo.FindByName("Dessert");
            var menuItemDesserts = await _repo.GetMenuItemsByCategory(dessertCategory.Id);
            var MenuItemDessertsVm = _mapper.Map<List<MenuItemVM>>(menuItemDesserts);

            var model = new CustomerMenuVM
            {
                Categories = categoriesVm,
                Starters = MenuItemStartersVm,
                MainCourse = MenuItemMainCourseVm,
                Desserts = MenuItemDessertsVm
            };

            return View(model);


        } 

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
