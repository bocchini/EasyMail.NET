using System.Diagnostics.CodeAnalysis;

namespace EasyMail.NET.Models;

[ExcludeFromCodeCoverage]
public class PersonFrom(string email, string name)
{
  public string _email { get; init; } = email;
  public string _name { get; init; } = name;
}
