using Microsoft.AspNetCore.Mvc;

using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using TestProject02.Models;
using System.Collections.Generic;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Data;
using AnimalShelter.Interfaces;
using AnimalShelter.Services;

namespace TestProject02.Controllers
{
    public class AdminController : Controller
    {
        private const string Folder = "Uploads";
        private readonly IAnimalService _animalService;
        private readonly IAdminService _accountService;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<AccountController> _logger;

        public AdminController(ILogger<AccountController> logger, IWebHostEnvironment _environment, IAnimalService animalService, IAdminService accountService)
        {
            this._environment = _environment;
            _animalService = animalService;
            _accountService = accountService;
            _logger = logger;
        }
        public ActionResult<List<Animal>> Admin()//index
        {
            ViewBag.animals = _animalService.GetAllAnimals(null);
            return View();
            //api endpoint
            //return animal;
        }
        public IActionResult AddAnimal()
        {
            ViewBag.ActualDate = DateOnly.FromDateTime(DateTime.Now).ToString("yyyy'-'MM'-'dd");
            return View();
        }
        [HttpPost]
        public IActionResult AddAnimal(Models.Animal animal, IFormFile image, IFormFile bgImage, string inclusion)
        {
            string path = Path.Combine(_environment.WebRootPath, Folder);
            animal.Inclusion = inclusion;
            string message = _animalService.AddAnimal(animal, image, bgImage, path, Folder);
            ViewBag.message = message;
            ViewBag.animals = _animalService.GetAllAnimals(null);
            
            _logger.LogWarning("{0} id: {1} Admin: {2}", message, animal.Id, HttpContext.Session.GetString("Name"));
            return View("~/Views/Admin/Admin.cshtml");
        }
        [Route("Admin/Delete/{id:int}")]
        public IActionResult DeleteAnimal(int id)
        {
            string message = _animalService.DeleteAnimal(id, _environment.WebRootPath);
            ViewBag.message = message;
            ViewBag.animals = _animalService.GetAllAnimals(null);
            _logger.LogWarning("{0} id: {1} Admin: {2}", message, id, HttpContext.Session.GetString("Name"));
            return View("~/Views/Admin/Admin.cshtml");
        }

        [Route("Admin/Edit/{id:int}")]
        public IActionResult EditAnimal(int id)
        {
            ViewBag.Animal=_animalService.GetAnimal(id);
            return View("~/Views/Admin/EditAnimal.cshtml");
        }
        [Route("Admin/Edit/{id:int}")]
        [HttpPost]
        public IActionResult EditAnimal(Animal animal, IFormFile image, IFormFile bgImage)
        {
            string message = _animalService.EditAnimal(animal, image, bgImage, Path.Combine(_environment.WebRootPath, Folder), Folder);
            ViewBag.message = message;
            _logger.LogWarning("{0} id: {1} Admin: {2}", message, animal.Id, HttpContext.Session.GetString("Name"));
            ViewBag.animals = _animalService.GetAllAnimals(null);
            return View("~/Views/Admin/Admin.cshtml");
        }

        public IActionResult Administrators()
        {
            ViewBag.users = _accountService.GetAllUsers();
            return View();
        }

        public IActionResult RemoveAdmin(int id)
        {
            _accountService.RemoveAdmin(id);
            string message = "Removed an admin! id: "+id;
            ViewBag.message = message;
            _logger.LogWarning(message +" Admin: "+ HttpContext.Session.GetString("Name"));
            return RedirectToAction("Administrators", "Admin");

        }
        [Route("Admin/Ad/{id:int}")]
        
        public IActionResult AddAdmin(int id)
        {
            _accountService.AddAdmin(id);
            string message = "Added an admin! id: " + id;
            ViewBag.message = message;
            _logger.LogWarning(message+" Admin: "+ HttpContext.Session.GetString("Name"));
            return RedirectToAction("Administrators", "Admin");
        }


    }
}

