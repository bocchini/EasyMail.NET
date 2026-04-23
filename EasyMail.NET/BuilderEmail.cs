using EasyMail.NET.Models;
using EasyMail.NET.Services;

namespace EasyMail.NET;

public sealed class BuilderEmail
{
  private string _fromEmail;
  private string _fromName;
  private string _toEmail;
  private string _toName;
  private string _subject;
  private string _body;
  private bool _isHtml;

  public static BuilderEmail Create()
  {
    var builder = new BuilderEmail();
    return builder;
  }

  public BuilderEmail From(string email, string name)
  {
    _fromEmail = email;
    _fromName = name;
    return this;
  }

  public BuilderEmail To(string email, string? name = null)
  {
    if (!IsValidEmail(email))
      throw new ArgumentException("Invalid email address for To.");
    _toEmail = email;
    _toName = name ?? string.Empty;
    return this;
  }

  public BuilderEmail Subject(string subject)
  {
    _subject = subject;
    return this;
  }

  public BuilderEmail Body(string body, bool isHtml = false)
  {
    _body = body;
    _isHtml = isHtml;
    return this;
  }

  public BuilderEmail Build()
  {
    if (_fromEmail == null)
      throw new InvalidOperationException("From information is required.");
    if (_fromName == null)
      throw new InvalidOperationException("From name is required.");

    return this;
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
