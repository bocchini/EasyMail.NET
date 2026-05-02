using AutoBogus;
using EasyMail.NET.Models;

namespace EasyMail.Net.Unit.Tests.Builders;

public class FromEmailBuilder : AutoFaker<PersonFrom>
{
  public FromEmailBuilder()
  { }

  public FromEmailBuilder WithName(string? name = null)
  {
    RuleFor(p => p._name, f => name ?? f.Person.FullName);
    return this;
  }

  public FromEmailBuilder WithEmail(string? email = null)
  {
    RuleFor(p => p._email, f => email ?? f.Internet.Email());
    return this;
  }
}
