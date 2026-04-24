namespace EasyMail.NET.Models;

public class PersonTo
{
  public required string _email { get; set; }
  public required string _name { get; set; }

  private PersonTo()
  { }

  public PersonTo(string email, string name)
  {
    _email = email;
    _name = name;
  }
}
