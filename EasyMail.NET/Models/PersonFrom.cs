
namespace EasyMail.NET.Models;

public class PersonFrom(string email, string name)
{
  public required string _email { get; init; } = email;
  public required string _name { get; init; } = name;
}
