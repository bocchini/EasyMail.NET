using EasyMail.NET.Models;
using MimeKit;

namespace EasyMail.NET.Interfaces;

public interface IEasyMailService
{
  Task<string> SendAsync(MimeMessage mimeMessage);
  Task<string> SendEmail(InformationToSendEmail informationToSendEmail);

}
