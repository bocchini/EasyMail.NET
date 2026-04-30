using System;
using EasyMail.NET.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace EasyMail.NET.Wrapper;

public class SmtpClientWrapper : ISmtpClientWrapper
{
  readonly ISmtpClient _client;
  private readonly (string username, string password, string Host, int Port, bool UseSsl) _config;

  public SmtpClientWrapper(Autentications autentications, ServerConfiguration config)
  {
    _client = new SmtpClient();
    _config = (autentications.Username, autentications.Password, config.Host, config.Port, config.EnableSSL);
  }

  public async Task AuthenticateAsync(CancellationToken cancellationToken = default)
  {
    await _client.AuthenticateAsync(_config.username, _config.password, cancellationToken);
  }

  public async Task ConnectAsync(SecureSocketOptions options = SecureSocketOptions.Auto, CancellationToken cancellationToken = default)
  {
    await _client.ConnectAsync(_config.Host, _config.Port, options, cancellationToken);
  }

  public async Task<string> SendAsync(MimeMessage message, CancellationToken cancellationToken = default)
  {
    return await _client.SendAsync(message, cancellationToken);
  }

  public async Task DisconnectAsync()
  {
    await _client.DisconnectAsync(true);
  }
}
