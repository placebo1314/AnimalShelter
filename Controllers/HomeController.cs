using System.Diagnostics;
using AnimalShelter.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TestProject02.Models;

namespace AnimalShelter.Controllers;

public class HomeController : Controller
{
    private IHomeService _homeService;

    public HomeController(IHomeService homeService)
    {
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
