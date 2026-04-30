using EasyMail.NET.Models;
using EasyMail.NET.Services;
using EasyMail.NET.Wrapper;
using MailKit.Security;
using MimeKit;
using NSubstitute;

namespace EasyMail.NET.Tests.Services;

public class EasyMailServiceTests
{
    private readonly ISmtpClientWrapper _smtpClientMock;
    private readonly EasyMailService _service;
    private readonly Autentications _auth;
    private readonly ServerConfiguration _config;

    public EasyMailServiceTests()
    {
        _smtpClientMock = Substitute.For<ISmtpClientWrapper>();
        _auth = Autentications.Create("user", "password");
        _config = ServerConfiguration.Create("smtp.test.com", 587, true);

        _service = new EasyMailService(_smtpClientMock);
    }

    [Fact]
    public async Task SendEmail_ShouldReturnSuccess_WhenEmailIsSentCorrectly()
    {
        var info = new InformationToSendEmail
        {
            Subject = "Test Subject",
            Body = "Hello World",
            IsHtml = false,
            _personFrom = new PersonFrom("Sender", "from@test.com"),
            _personTo = new PersonTo("Receiver", "to@test.com")
        };

        await _smtpClientMock.ConnectAsync(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<SecureSocketOptions>(), default).;
        var result = await _service.SendEmail(info);

        Assert.Contains("Success: Email sent successfully.", result);
        await _smtpClientMock.Received(1).ConnectAsync(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<SecureSocketOptions>(), default);
        await _smtpClientMock.Received(1).AuthenticateAsync(Arg.Any<string>(), Arg.Any<string>(), default);
        await _smtpClientMock.Received(1).SendAsync(Arg.Any<MimeMessage>(), default, null);
    }

    // [Fact]
    // public async Task SendEmail_ShouldCreateHtmlPart_WhenIsHtmlIsTrue()
    // {  
    //     var info = new InformationToSendEmail
    //     {
    //         Subject = "Test",
    //         Body = "<h1>Hi</h1>",
    //         IsHtml = true,
    //         _personFrom = new PersonFrom("S", "s@t.com"), 
    //         _personTo = new PersonTo("R","r@t.com")
    //     };

    //     await _service.SendEmail(info);

    //     _smtpClientMock.Verify(x => x.SendAsync(
    //         Arg.Is<MimeMessage>(m => ((TextPart)m.Body).ContentType.MediaSubtype == "html"),
    //         default, null), Times.Once);
    // }

    // [Fact]
    // public async Task SendAsync_ShouldReturnFailureMessage_WhenConnectionFails()
    // {  
    //     var message = new MimeMessage();
    //     _smtpClientMock
    //         .Authenticated(x => x.ConnectAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<SecureSocketOptions>(), default))
    //         .ThrowsAsync(new System.Net.Sockets.SocketException());
    //     var result = await _service.SendAsync(message);
    //     Assert.StartsWith("Failed:", result);
    //     _smtpClientMock.Verify(x => x.DisconnectAsync(true, default), Times.Once);
    // }
}
