using System.Data.SqlClient;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TestProject02.Models;
using TestProject02.Services;

namespace TestProject02.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    //private AnimalService _animalService;
    private HomeService _homeService;

    public HomeController(ILogger<HomeController> logger, AnimalService animalService, HomeService homeService)
    {
        _logger = logger;
        //_animalService = animalService;
        _homeService = homeService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Animals(string sortBy, string order, bool change, string species, int page)
    {   
        return View(_homeService.GetDisplayAnimalsData(species, sortBy, page, order, change));
    }

    public IActionResult Donations()
    {
        return View();
    }
    public IActionResult Adoption()
    {
        return View();
    }
    public IActionResult Connections()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
