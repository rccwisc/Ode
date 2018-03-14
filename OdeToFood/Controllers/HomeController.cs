using Microsoft.AspNetCore.Mvc;
using OdeToFood.Models;
using OdeToFood.Services;
using OdeToFood.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IResturantData restaurantData,
                              IGreeter greeter)
        {
            _resturantData = restaurantData;
            _greeter = greeter;
        }

        public IActionResult Index()
        {
            var model = new HomeIndexViewModel();
            model.Restaurants = _resturantData.GetAll();
            model.CurrentMessage = _greeter.GetMessageOfTheDay();
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = _resturantData.Get(id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RestaurantEditModel res)
        {
            if (ModelState.IsValid)
            {
                var newRes = new Restaurant();
                newRes.Cuisine = res.Cuisine;
                newRes.Name = res.Name;

                newRes = _resturantData.Add(newRes);

                return RedirectToAction(nameof(Details), new { Id = newRes.Id });
            } 
            else
            {
                return View();
            }

        }


        private IResturantData _resturantData;
        private IGreeter _greeter;
    }
}
