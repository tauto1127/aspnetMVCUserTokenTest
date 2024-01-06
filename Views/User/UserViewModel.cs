namespace webUserLoginTest.Views.User
{
    public class UserViewModel
    {
        public IEnumerable<Models.User> Users { get; set; }

        public UserViewModel(IEnumerable<Models.User> users)
        {
            this.Users = users;
        }

    }
}