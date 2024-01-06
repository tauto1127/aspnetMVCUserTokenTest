using webUserLoginTest.Data;

namespace webUserLoginTest.Models.ViewModel
{
    public class UserIndexViewModel
    {
        private readonly UserContext _context;
        public IList<User> Users { get; set; }

        public UserIndexViewModel(UserContext context)
        {
            this._context = context;
            Users = _context.Users.Take(10).ToList();
        }
    }
}
