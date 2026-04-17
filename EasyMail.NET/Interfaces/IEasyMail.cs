namespace EasyMail.NET.Interfaces;

public interface IEasyMail
{
  IEasyMail To(string email, string? name = null);
  IEasyMail Subject(string subject);
  IEasyMail Body(string content, bool isHtml = false);
  IEasyMail From(string email, string? name = null);
  Task<string> SendAsync();
}
