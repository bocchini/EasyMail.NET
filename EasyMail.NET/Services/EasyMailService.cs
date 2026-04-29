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

  public async Task<string> SendAsync(MimeMessage mimeMessage)
  {
    try
    {
      await ClientConnectAsync();
      await _client.SendAsync(mimeMessage);
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

  public async Task<string> SendEmail(InformationToSendEmail informationToSendEmail)
  {

    _mineMesseage.From.Add(new MailboxAddress(informationToSendEmail._personFrom._name, informationToSendEmail._personFrom._email));
    _mineMesseage.To.Add(new MailboxAddress(informationToSendEmail._personTo._name, informationToSendEmail._personTo._email));
    _mineMesseage.Subject = informationToSendEmail.Subject;

    if (informationToSendEmail.IsHtml)
    {
      _mineMesseage.Body = new TextPart("html")
      {
        Text = informationToSendEmail.Body
      };
    }
    else
    {
      _mineMesseage.Body = new TextPart("plain")
      {
        Text = informationToSendEmail.Body
      };
    }

    return await SendAsync(_mineMesseage);
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