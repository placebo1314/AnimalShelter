using System;
using System.Collections.Generic;
using System.Security.Claims;
using TestProject02.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using AnimalShelter.Interfaces;
using AnimalShelter.Services;
using Microsoft.AspNetCore.Mvc.ViewEngines;

public class AccountController : Controller
{
    private IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(RegisterModel register)
    {
        try
        {
            _accountService.AddUser(register);
        }
        catch (Exception)
        {
            ModelState.AddModelError("Email", "Already taken.");
            return View();
        }

        //_emailService.SendEmail(register);
        return RedirectToAction("Login");

    }

    [HttpPost]
    public async Task<IActionResult> LoginAsync(LoginModel login, string returnUrl)
    {
        var user = _accountService.Login(login);
        if (user != null)
        {
            HttpContext.Session.SetString("Name", user.Name);
            HttpContext.Session.SetString("Admin", user.Admin);

            if(HttpContext.Session.GetString("Admin") == "Y")
                return RedirectToAction("Admin", "Admin");
            return View("~/Views/Home/Index.cshtml");
        }
        ModelState.AddModelError("Email", "Invalid Information.");
        ModelState.AddModelError("Password", "Invalid Information.");
        return View();
    }

    public ActionResult LogOut()
    {
        _accountService.Logout(HttpContext.Session);
        return RedirectToAction("Index", "Home");
    }

}