using Microsoft.AspNetCore.Mvc;
using webUserLoginTest.Const;
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
        return View(new UserIndexViewModel(_context));
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
    public IActionResult Register(String name_box, String password_box)
    {
        DateTime _created = DateTime.Now;
        byte[] _salt = PasswordUtil.PasswordUtil.GetInitialPasswordSalt(_created.ToString());
        _context.Users.Add(new User()
        {
            Name = name_box,
            PasswordHash = PasswordUtil.PasswordUtil.GetPasswordHashFromPepper(_salt, password_box, PasswordSalt.Salt),
            PasswordSalt = _salt,
            CreatedDate = _created,
        });
        _context.SaveChanges();
        return View("UserDetail", new SignupViewModel(){Name = name_box,Password = "aiueo"});
    }
}