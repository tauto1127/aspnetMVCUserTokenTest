using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace webUserLoginTest.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public DateTime? CreatedDate { get; set; }

    public override string ToString()
    {
        return "id:" + Id + "名前:" + Name + "作成び:" + CreatedDate;
    }
}