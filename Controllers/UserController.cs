using Microsoft.AspNetCore.Mvc;
using webUserLoginTest.Data;
using webUserLoginTest.Models;
using webUserLoginTest.Models.ViewModel;
using webUserLoginTest.Views.User;

namespace webUserLoginTest.Controllers;

public class UserController : Controller
{
    private readonly UserContext _context;
    public UserController(UserContext context)
    {
        _context = context;
    }
    // GET
    public IActionResult Index()
    {
        return View(new UserViewModel(_context.Users));
    }

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult SignUp()
    {
        var _signUpViewModel = new SignupViewModel() { Name = "ta", Password = "" };
        return View(_signUpViewModel);
    }

    [HttpPost]
    public IActionResult Register(String name_box)
    {
        return View("UserDetail", new SignupViewModel(){Name = name_box,Password = "aiueo"});
    }
}