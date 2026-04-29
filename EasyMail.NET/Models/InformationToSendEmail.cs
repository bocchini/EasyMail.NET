namespace EasyMail.NET.Models;

public sealed class InformationToSendEmail
{
  public InformationToSendEmail() { }
  public InformationToSendEmail(PersonFrom personFrom, PersonTo personTo, bool isHtml, string body)
  {
    _personFrom = personFrom;
    _personTo = personTo;
    IsHtml = isHtml;
    Body = body;
  }

  public required PersonTo _personTo { get; init; }
  public required PersonFrom _personFrom { get; init; }
  public required string Subject { get; init; }

  public required string Body { get; init; }

  public required bool IsHtml { get; init; } = false;
}
