using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mimi.DBpedia.Access;
using MimiWeb.Models;

namespace MimiWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var cityResourceRepository = new CityResourceRepository();

            var cities = cityResourceRepository.GetCities();

            return View(cities);
        }

        public IActionResult Details(string cityResource)
        {
            var cityResourceRepository = new CityResourceRepository();

            var cities = cityResourceRepository.GetCityInfo(cityResource);

            return View(cities);
        }

        public IActionResult Help()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
