using EasyMail.NET.Interfaces;
using EasyMail.NET.Models;
using EasyMail.NET.Services;
using EasyMail.NET.Wrapper;
using MimeKit;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace EasyMail.NET.Tests.Services;

public class EasyMailServiceTests
{
    private readonly ISmtpClientWrapper _smtpClientMock;
    private readonly IEasyMailService _service;

    public EasyMailServiceTests()
    {
        _smtpClientMock = Substitute.For<ISmtpClientWrapper>();
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

        await _smtpClientMock.ConnectAsync();
        await _smtpClientMock.AuthenticateAsync();
        _smtpClientMock.SendAsync(Arg.Any<MimeMessage>())
            .Returns(Task.FromResult("Success: Email sent successfully."));

        var result = await _service.SendEmail(info);

        Assert.Contains("Success: Email sent successfully.", result);
    }

    [Fact]
    public async Task SendEmail_ShouldCreateHtmlPart_WhenIsHtmlIsTrue()
    {
        var info = new InformationToSendEmail
        {
            Subject = "Test",
            Body = "<h1>Hi</h1>",
            IsHtml = true,
            _personFrom = new PersonFrom("S", "s@t.com"),
            _personTo = new PersonTo("R", "r@t.com")
        };

        await _smtpClientMock.ConnectAsync();
        await _smtpClientMock.AuthenticateAsync();
        _smtpClientMock.SendAsync(Arg.Any<MimeMessage>())
            .Returns(Task.FromResult("Success: Email sent successfully."));
        await _service.SendEmail(info);

        await _smtpClientMock.Received(1).SendAsync(Arg.Is<MimeMessage>(m => ((TextPart)m.Body).ContentType.MediaSubtype == "html"));
    }

    [Fact]
    public async Task SendAsync_ShouldReturnFailureMessage_WhenConnectionFails()
    {
        var message = new MimeMessage();
        await _smtpClientMock.ConnectAsync();
        _smtpClientMock.AuthenticateAsync()
       .ThrowsAsync(new System.Net.Sockets.SocketException());
        var result = await _service.SendAsync(message);
        Assert.StartsWith("Failed:", result);
        await _smtpClientMock.Received(1).DisconnectAsync();
    }
}
