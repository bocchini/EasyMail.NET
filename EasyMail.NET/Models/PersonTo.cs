namespace EasyMail.NET.Models;

public class PersonTo(string email, string name)
{
  public string _email { get; set; } = email;
  public string _name { get; set; } = name;
}
