using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UGTest.Models;
using UGTest.Context;

namespace UGTest.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MyDbContext db;
    public HomeController(ILogger<HomeController> logger,MyDbContext _db)
    {
        _logger = logger;
        db = _db;
    }

    public IActionResult Index()
    {
        var customers = db.MusteriTanimTables.ToList();
        return View(customers);
      
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