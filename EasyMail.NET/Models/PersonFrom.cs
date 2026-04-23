
namespace EasyMail.NET.Models;

public sealed class PersonFrom
{
  public PersonFrom(string email, string name)
  {
    _email = email;
    _name = name;
  }

  private PersonFrom() { }

  public required string _email { get; set; }
  public required string _name { get; set; }
}
