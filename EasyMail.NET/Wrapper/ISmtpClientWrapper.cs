using System;
using MimeKit;

namespace EasyMail.NET.Wrapper;

public interface ISmtpClientWrapper
{
  Task ConnectAsync(MailKit.Security.SecureSocketOptions options = MailKit.Security.SecureSocketOptions.Auto, CancellationToken cancellationToken = default);
  Task AuthenticateAsync(CancellationToken cancellationToken = default);
  Task<string> SendAsync(MimeMessage message, CancellationToken cancellationToken = default);
  Task DisconnectAsync();
}
