using AutoBogus;
using EasyMail.NET.Models;

namespace EasyMail.Net.Unit.Tests.Builders;

public class ToEmailBuilder : AutoFaker<PersonTo>
{

  public ToEmailBuilder()
  { }

  public ToEmailBuilder WithName(string? name = null)
  {
    RuleFor(p => p._name, n => name ?? n.Person.FullName);
    return this;
  }

  public ToEmailBuilder WithEmail(string? email = null)
  {
    RuleFor(p => p._email, e => email ?? e.Internet.Email());
    return this;
  }
}
