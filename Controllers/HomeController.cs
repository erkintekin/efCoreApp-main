using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using efCoreApp.Models;

namespace efCoreApp.Controllers;

public class HomeController : Controller
{


    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }


}
