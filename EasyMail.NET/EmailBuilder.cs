using EasyMail.NET.Models;

namespace EasyMail.NET;

public sealed class EmailBuilder
{
  private string _fromEmail = string.Empty;
  private string _fromName = string.Empty;
  private string _toEmail = string.Empty;
  private string _toName = string.Empty;
  private string _subject = string.Empty;
  private string _body = string.Empty;
  private bool _isHtml;

  public static EmailBuilder Create()
  {
    var builder = new EmailBuilder();
    return builder;
  }

  public EmailBuilder AddFromInformation(string email, string? name = null)
  {
    _fromEmail = email;
    _fromName = string.IsNullOrWhiteSpace(name) ? email : name;
    return this;
  }

  public EmailBuilder AddToInformation(string email, string? name = null)
  {
    _toEmail = email;
    _toName = string.IsNullOrWhiteSpace(name) ? email : name;
    return this;
  }

  public EmailBuilder AddSubject(string subject)
  {
    _subject = subject;
    return this;
  }

  public EmailBuilder AddBody(string body, bool isHtml = false)
  {
    _body = body;
    _isHtml = isHtml;
    return this;
  }

  public InformationToSendEmail Build()
  {
    if (_fromEmail == null || !IsValidEmail(_fromEmail))
      throw new InvalidOperationException("From information is required.");
    if (_toEmail == null || !IsValidEmail(_toEmail))
      throw new InvalidOperationException("From name is required.");

    return new InformationToSendEmail
    {
      _personFrom = new PersonFrom(_fromEmail, _fromName),
      _personTo = new PersonTo(_toEmail!, _toName),
      Body = _body,
      Subject = _subject,
      IsHtml = _isHtml,
    };
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
