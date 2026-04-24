using EasyMail.NET.Models;
using EasyMail.NET.Services;

namespace EasyMail.NET;

public sealed class BuilderEmail
{
  private string _fromEmail = string.Empty;
  private string _fromName = string.Empty;
  private string _toEmail = string.Empty;
  private string _toName = string.Empty;
  private string _subject = string.Empty;
  private string _body = string.Empty;
  private bool _isHtml;

  public static BuilderEmail Create()
  {
    var builder = new BuilderEmail();
    return builder;
  }


  public BuilderEmail AddToInformation(string email, string? name = null)
  {
    _toEmail = email;
    _toName = name ?? string.Empty;
    return this;
  }

  public BuilderEmail AddSubject(string subject)
  {
    _subject = subject;
    return this;
  }

  public BuilderEmail AddBody(string body, bool isHtml = false)
  {
    _body = body;
    _isHtml = isHtml;
    return this;
  }

  public InformationToSendEmail Build()
  {
    if (_fromEmail == null || IsValidEmail(_fromEmail))
      throw new InvalidOperationException("From information is required.");
    if (_toEmail == null || IsValidEmail(_toEmail))
      throw new InvalidOperationException("From name is required.");

    return new InformationToSendEmail
    {
      _personFrom = new PersonFrom(_fromEmail, _fromName),
      _personTo = new PersonTo(_toEmail!, _toName)
    }
    ;
  }

  private bool IsValidEmail(string email)
  {
    if (string.IsNullOrWhiteSpace(email)) return false;
    try
    {
      var addr = new System.Net.Mail.MailAddress(email);
      return addr.Address == email;
    }
    catch
    {
      return false;
    }
  }
}
