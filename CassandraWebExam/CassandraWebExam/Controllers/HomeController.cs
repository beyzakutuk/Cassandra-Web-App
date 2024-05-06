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
    [HttpGet]
    public IActionResult UserPanel()
    {
        var response = _context.UserList();


        (List<GetUsersModels>, int) tuple = (response, response.Count());
        if (response.Any())
        {
            return View(tuple);
        }
        return View();
    }
    [HttpGet]
    public IActionResult UserAdd()
    {
        return View();
    }
    [HttpPost]
    public IActionResult UserAdd(UserModel userModel)
    {
        var bools = _context.UserAdd(userModel);
        if (!bools)
        {
            TempData["Fail"] = "Bu kullanıcı zaten var";
            return View();
        }

        return RedirectToAction("UserPanel");
    }
    [HttpGet]
    public IActionResult UserDelete()
    {
        return View();
    }
    [HttpPost]
    public IActionResult UserDelete(UserDeleteModel userDeleteModel)
    {
        var returnbool = _context.UserDelete(userDeleteModel);

        if (returnbool)
            return RedirectToAction("Login");

        TempData["Fails"] = "bu kullanıcı yok";
        return View();
    }

    [HttpGet]
    public IActionResult UserUpdate()
    {
        return View();
    }
    [HttpPost]
    public IActionResult UserUpdate(UserUpdateModels userUpdateModels)
    {
        var returnBool = _context.UserUpdate(userUpdateModels);
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Register(UserModel userModel)
    {
        _context.UserAdd(userModel);
        return RedirectToAction("UserPanel");
    }
}

