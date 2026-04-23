using EasyMail.NET.Interfaces;
using EasyMail.NET.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace EasyMail.NET.Services;

public class EasyMailService : IEasyMail
{
  private readonly (string username, string password, string Host, int Port, bool UseSsl) _config;
  readonly MimeMessage _mineMesseage;
  readonly SmtpClient _client;

  public EasyMailService(Autentications auth, ServerConfiguration config)
  {
    _config = (auth.Username, auth.Password, config.Host, config.Port, config.EnableSSL);
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
      await ClientConnectAsync();
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
      await ClientConnectAsync();

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

  private async Task ClientConnectAsync()
  {
    try
    {
      await _client.ConnectAsync(_config.Host, _config.Port, MailKit.Security.SecureSocketOptions.StartTls);
      await _client.AuthenticateAsync(_config.username, _config.password);
    }
    catch (Exception ex)
    {
      throw new Exception($"Failed to connect to SMTP server: {ex.Message}");
    }
  }
}