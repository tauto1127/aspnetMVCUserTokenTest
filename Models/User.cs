using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace webUserLoginTest.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public DateTime? CreatedDate { get; set; }
    
}