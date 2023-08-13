using System.ComponentModel.DataAnnotations;

namespace webUserLoginTest.Models.ViewModel;

public class SignupViewModel
{
    [Display(Name = "名前")]
    public string Name { get; set; }
    public string Password { get; set; }
}