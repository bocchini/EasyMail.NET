using EasyMail.NET;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;
using EasyMail.NET.Extensions;
using EasyMail.NET.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfiguration(builder.Configuration);

// Add OpenAPI services
builder.Services.AddOpenApi();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
  app.MapScalarApiReference(options =>
    {
      options.Title = "EasyMail.NET";
      options.Theme = ScalarTheme.Mars; // Options include Dark, Light, Mars, etc.
    });
}

app.MapGet("/", () => "Hello World!");

app.MapPost("/EnviarCupom", async ([FromServices] IEasyMailService service) =>
{
  var emailFrom = "market@email.com";
  var nameFrom = "Market";
  var emailCliente = "americo.bocchini@gmail.com";

  var message = EmailBuilder
   .Create()
   .AddToInformation(emailCliente, "Nome do Cliente")
   .AddFromInformation(emailFrom, nameFrom)
   .AddSubject("Seu Cupom de Desconto Chegou! 🎁")
   .AddBody("<h1>Olá!</h1><p>Use o código <strong>PRIMEIRACOMPRA</strong> para 10% OFF.</p>", true)
   .Build();

  var result = await service.SendEmail(message);

  return TypedResults.Created(result);

});

app.Run();
