using System.Runtime.InteropServices;
using System.Text;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webUserLoginTest.Const;
using webUserLoginTest.Data;
using webUserLoginTest.Models;
using webUserLoginTest.Models.ViewModel;
using webUserLoginTest.Util;

namespace webUserLoginTest.Controllers
{
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

        public IActionResult LoginView()
        {
            var sessionid = Request.Cookies["sessionid"];
            if (sessionid == null) return View("LoginView", new LoginViewModel());
            Console.WriteLine("んん？");
            foreach (var b in Encoding.Unicode.GetBytes(System.Net.WebUtility.UrlDecode(sessionid)))
            {
                Console.WriteLine(b);
            }
            var userId = Sessions.GetUserId(Encoding.Unicode.GetBytes(System.Net.WebUtility.UrlDecode(sessionid)));
            if (userId == -1) return View("LoginView", new LoginViewModel());
            User user = _context.Users.Find(userId);
            return View("UserDetail",
                new SignupViewModel() { Name = user.Name, Password = user.PasswordHash.ToString() });
        }

        public IActionResult SignUpView()
        {
            var _signUpViewModel = new SignupViewModel() { Name = "ta", Password = "" };
            return View(_signUpViewModel);
        }

        [HttpPost]
        public IActionResult Register(String name_box, String password_box)
        {
            DateTime _created = DateTime.Now;
            byte[] _salt = PasswordUtil.GetInitialPasswordSalt(_created.ToString());
            User user = new User()
            {
                Name = name_box,
                PasswordHash = PasswordUtil.GetPasswordHashFromPepper(_salt, password_box, PasswordSalt.Salt),
                PasswordSalt = _salt,
                CreatedDate = _created,
            };
            var res = _context.Users.Add(user);
            _context.SaveChanges();
            Response.Headers["Set-Cookie"] = "sessionid=" + System.Net.WebUtility.UrlEncode(System.Text.Encoding.Unicode.GetString(Sessions.Add(res.Entity)));
            return View("UserDetail", new SignupViewModel() { Name = name_box, Password = "aiueo" });
        }

        [HttpPost]
        public async Task<IActionResult> Login(String name_box, String password_box)
        {
            User? user = await _context.Users.SingleOrDefaultAsync(s => s.Name == name_box);
            if (user == null) return View("LoginView", new LoginViewModel(isValid: false));
            var passwordHash =
                PasswordUtil.GetPasswordHashFromPepper(user.PasswordSalt, password_box, PasswordSalt.Salt);
            if (Encoding.Unicode.GetString(passwordHash) == Encoding.Unicode.GetString(user.PasswordHash))
            {
                Response.Headers["Set-Cookie"] = "sessionid=" +
                                                 System.Net.WebUtility.UrlEncode(
                                                     Encoding.Unicode.GetString(Sessions.Add(user)));
                return View("UserDetail", new SignupViewModel() { Name = user.Name, Password = "aiueo" });
            }
            else
            {
                return View("LoginView", new LoginViewModel(isValid: false));
            }
        }

        [HttpGet]
        public string Test()
        {
            const string secret = "秘密鍵文字列";
            var jwtToken = new JwtBuilder()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(secret)
                .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(3).ToUnixTimeSeconds())
                .AddClaim("email", "tata@tata.com")
                .AddClaim("data1", "hello")
                .Encode();
            Console.WriteLine(jwtToken);
            return jwtToken;
        }
    }
}