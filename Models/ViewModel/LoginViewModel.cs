using System.ComponentModel.DataAnnotations;

namespace webUserLoginTest.Models.ViewModel;

public class LoginViewModel
{
    public LoginViewModel(bool isValid)
    {
       IsValid = isValid;
    }
    public LoginViewModel(){}

    [Display(Name = "名前")] public string Name { get; set; } = "";
    public string Password { get; set; } = "";
    public bool IsValid { get; set; } = true;
    
}