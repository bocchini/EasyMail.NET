using EasyMail.NET.Models;
using EasyMail.NET.Wrapper;
using MimeKit;

namespace EasyMail.NET.Services;

public class EasyMailService
{
  private readonly ISmtpClientWrapper _client;

  public EasyMailService(ISmtpClientWrapper client)
  {
    _client = client;
    _mineMesseage = new MimeMessage();
  }

  readonly MimeMessage _mineMesseage;

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
      await _client.DisconnectAsync();
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
      await _client.ConnectAsync(MailKit.Security.SecureSocketOptions.StartTls);
      await _client.AuthenticateAsync();
    }
    catch (Exception ex)
    {
      throw new Exception($"Failed to connect to SMTP server: {ex.Message}");
    }
  }
}