using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CassandraWebExam.Models;

namespace CassandraWebExam.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IDbContext _context;

    public HomeController(ILogger<HomeController> logger,IDbContext dbContext)
    {
        _logger = logger;
        _context = dbContext;
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Login(LoginModels loginModels)
    {
      bool result=  _context.LoginUser(loginModels);

        if(result)
        {
            return RedirectToAction("UserPanel");
        }
        TempData["Error"] = "Hatalı kullanıcı adı veya şifre";

        return View();
    }
    

}

