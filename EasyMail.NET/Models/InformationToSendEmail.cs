namespace EasyMail.NET.Models;

public sealed class InformationToSendEmail
{
  public InformationToSendEmail() { }
  public InformationToSendEmail(PersonFrom personFrom, PersonTo personTo)
  {
    _personFrom = personFrom;
    _personTo = personTo;
  }

  public required PersonTo _personTo { get; init; }
  public required PersonFrom _personFrom { get; init; }
}
