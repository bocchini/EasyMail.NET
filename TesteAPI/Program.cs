using EasyMail.NET;
using EasyMail.NET.Extensions;
using EasyMail.NET.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddConfiguration(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

object value = app.MapPost("/sendmail", async () =>
{
    var message = BuilderEmail
        .Create()
        .From("", "")
        .To;
    var emailAdress = "";

    message.From(emailAdress, "Ameri");
    message.To("", "Alessandro");
    message.Subject("Teste de email");
    message.Body("Olá, este é um email de teste enviado usando a EasyMail.NET!");

    var result = await message.SendAsync();
    return result;
})
.WithName("EmailSend");

app.Run();


