using Bogus;
using EasyMail.Net.Unit.Tests.Builders;
using EasyMail.NET;
using EasyMail.NET.Models;

namespace EasyMail.Net.Unit.Tests;

public class EmailBuilderTests
{
  private EmailBuilder _builder;
  private readonly Faker faker = new("pt_BR");


  public EmailBuilderTests()
  {
    _builder = EmailBuilder.Create();
  }

  [Fact]
  public void Should_CreateMessageToSend()
  {
    var toEmail = new ToEmailBuilder().WithEmail().WithName().Generate();
    var fromEmail = new FromEmailBuilder().WithEmail().WithName().Generate();
    var body = faker.Lorem.Lines(3);
    var subject = faker.Lorem.Sentence();

    var message = _builder
      .AddFromInformation(fromEmail._email, fromEmail._name)
      .AddToInformation(toEmail._email, toEmail._name)
      .AddBody(body, true)
      .AddSubject(subject)
      .Build();

    var expect = new InformationToSendEmail
    {
      _personFrom = new PersonFrom(fromEmail._email, fromEmail._name),
      _personTo = new PersonTo(toEmail._email, toEmail._name),
      Body = body,
      Subject = subject,
      IsHtml = true
    };

    Assert.Equivalent(expect, message);
  }

  [Fact]
  public void Should_CreateMessageToSend_WhenNameToIsEmpty()
  {
    var toEmail = new ToEmailBuilder().WithEmail().Generate();
    var fromEmail = new FromEmailBuilder().WithEmail().WithName().Generate();
    var body = faker.Lorem.Lines(3);
    var subject = faker.Lorem.Sentence();

    var message = _builder
      .AddFromInformation(fromEmail._email, fromEmail._name)
      .AddToInformation(toEmail._email, string.Empty)
      .AddBody(body, true)
      .AddSubject(subject)
      .Build();

    var expect = new InformationToSendEmail
    {
      _personFrom = new PersonFrom(fromEmail._email, fromEmail._name),
      _personTo = new PersonTo(toEmail._email, toEmail._email),
      Body = body,
      Subject = subject,
      IsHtml = true
    };

    Assert.Equivalent(expect, message);
  }


  [Fact]
  public void Should_CreateMessageToSend_WhenNameFromIsEmpty()
  {
    var toEmail = new ToEmailBuilder().WithEmail().WithName().Generate();
    var fromEmail = new PersonFrom(faker.Internet.Email(), string.Empty);

    var body = faker.Lorem.Lines(3);
    var subject = faker.Lorem.Sentence();

    var message = _builder
      .AddFromInformation(fromEmail._email, fromEmail._name)
      .AddToInformation(toEmail._email, toEmail._name)
      .AddBody(body, true)
      .AddSubject(subject)
      .Build();

    var expect = new InformationToSendEmail
    {
      _personFrom = new PersonFrom(fromEmail._email, fromEmail._email),
      _personTo = new PersonTo(toEmail._email, toEmail._name),
      Body = body,
      Subject = subject,
      IsHtml = true
    };

    Assert.Equivalent(expect, message);
  }

  [Fact]
  public void Should_CreateMessageToSend_WhenIsHtmlFalse()
  {
    var toEmail = new ToEmailBuilder().WithEmail().WithName().Generate();
    var fromEmail = new FromEmailBuilder().WithEmail().WithName().Generate();

    var body = faker.Lorem.Lines(3);
    var subject = faker.Lorem.Sentence();

    var message = _builder
      .AddFromInformation(fromEmail._email, fromEmail._name)
      .AddToInformation(toEmail._email, toEmail._name)
      .AddBody(body)
      .AddSubject(subject)
      .Build();

    var expect = new InformationToSendEmail
    {
      _personFrom = new PersonFrom(fromEmail._email, fromEmail._name),
      _personTo = new PersonTo(toEmail._email, toEmail._name),
      Body = body,
      Subject = subject,
      IsHtml = false
    };

    Assert.Equivalent(expect, message);
  }

  [Fact]
  public void Should_CreateMessageToSend_WhenIsHtmlTrue()
  {
    var toEmail = new ToEmailBuilder().WithEmail().WithName().Generate();
    var fromEmail = new FromEmailBuilder().WithEmail().WithName().Generate();

    var body = faker.Lorem.Lines(3);
    var subject = faker.Lorem.Sentence();

    var message = _builder
      .AddFromInformation(fromEmail._email, fromEmail._name)
      .AddToInformation(toEmail._email, toEmail._name)
      .AddBody(body, true)
      .AddSubject(subject)
      .Build();

    var expect = new InformationToSendEmail
    {
      _personFrom = new PersonFrom(fromEmail._email, fromEmail._name),
      _personTo = new PersonTo(toEmail._email, toEmail._name),
      Body = body,
      Subject = subject,
      IsHtml = true
    };

    Assert.Equivalent(expect, message);
  }

  [Fact]
  public void Should_NotCreateMessageToSend_WhenEmailIsInvalidToEmail()
  {
    var toEmail = new ToEmailBuilder().WithEmail(faker.Random.AlphaNumeric(5)).WithName().Generate();
    var fromEmail = new FromEmailBuilder().WithEmail().WithName().Generate();

    var body = faker.Lorem.Lines(3);
    var subject = faker.Lorem.Sentence();

    var result = Assert.Throws<InvalidOperationException>(() =>
    _builder
      .AddFromInformation(fromEmail._email, fromEmail._name)
      .AddToInformation(toEmail._email, toEmail._name)
      .AddBody(body)
      .AddSubject(subject)
      .Build());

    result.Message.Contains("From information is required.");
  }

  [Fact]
  public void Should_NotCreateMessageToSend_WhenEmailIsInvalidFromEmail()
  {
    var toEmail = new ToEmailBuilder().WithEmail().WithName().Generate();
    var fromEmail = new FromEmailBuilder().WithEmail(faker.Random.AlphaNumeric(5)).WithName().Generate();

    var body = faker.Lorem.Lines(3);
    var subject = faker.Lorem.Sentence();

    var result = Assert.Throws<InvalidOperationException>(() =>
    _builder
      .AddFromInformation(fromEmail._email, fromEmail._name)
      .AddToInformation(toEmail._email, toEmail._name)
      .AddBody(body)
      .AddSubject(subject)
      .Build());

    result.Message.Contains("From information is required.");
  }
}
