using EasyMail.NET.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

namespace EasyMail.NET;

public class EasyMailService : IEasyMail
{
  readonly MimeMessage _mineMesseage;
  readonly SmtpClient _client;

  public EasyMailService()
  {
    _mineMesseage = new MimeMessage();
    _client = new SmtpClient();
  }

  public IEasyMail Body(string content, bool isHtml = false)
  {
    if (isHtml)
    {
      _mineMesseage.Body = new TextPart("html")
      {
        Text = content
      };
    }
    else
    {
      _mineMesseage.Body = new TextPart("plain")
      {
        Text = content
      };
    }
    return this;
  }

  public IEasyMail From(string email, string? name = null)
  {
    _mineMesseage.From.Add(new MailboxAddress(name ?? email, email));
    return this;
  }

  public async Task<string> SendAsync()
  {
    try
    {
      await _client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
      await _client.AuthenticateAsync("", "");
      await _client.SendAsync(_mineMesseage, new CancellationToken());
      return "Success: Email sent successfully.";
    }
    catch (Exception ex)
    {
      return $"Failed: {ex.Message}";
    }
    finally
    {
      await _client.DisconnectAsync(true);
    }
  }

  public IEasyMail Subject(string subject)
  {
    _mineMesseage.Subject = subject;
    return this;
  }

  public IEasyMail To(string email, string? name = null)
  {
    _mineMesseage.To.Add(new MailboxAddress(name ?? string.Empty, email));
    return this;
  }

  public async Task<string> SendAsync(string email, string password)
  {
    try
    {
      await _client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
      await _client.AuthenticateAsync(email, password);
      await _client.SendAsync(_mineMesseage, new CancellationToken());
      return "Success: Email sent successfully.";
    }
    catch (Exception ex)
    {
      return $"Failed: {ex.Message}";
    }
    finally
    {
      await _client.DisconnectAsync(true);
    }
  }
}