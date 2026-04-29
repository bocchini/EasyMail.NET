using EasyMail.NET.Models;

namespace EasyMail.NET.Interfaces;

public interface IEasyMail
{
  Task<string> SendEmail(InformationToSendEmail informationToSendEmail);
}
