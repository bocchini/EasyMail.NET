
using EasyMail.NET.Models;
using EasyMail.NET.Services;
using EasyMail.NET.Wrapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;

namespace EasyMail.NET.Extensions;

public static class ConfiguratonExtensions
{
    public static void AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        ConfiguratonExtensionsEmail(services, configuration);
        services.AddScoped<ISmtpClientWrapper, SmtpClientWrapper>();
    }

    private static void ConfiguratonExtensionsEmail(IServiceCollection services, IConfiguration configuration)
    {
        var username = configuration.GetValue<string>("EasyMail:Configuration:Username");
        var password = configuration.GetValue<string>("EasyMail:Configuration:Password");
        var host = configuration.GetValue<string>("EasyMail:Configuration:Host");
        var port = configuration.GetValue<int>("EasyMail:Configuration:Port");
        var useSsl = configuration.GetValue<bool>("EasyMail:Configuration:EnableSSL");

        var auth = Autentications.Create(username!, password!);
        var config = ServerConfiguration.Create(host!, port, useSsl);

        services.AddScoped<ISmtpClientWrapper>(sp => new SmtpClientWrapper(auth, config));
        services.AddScoped<EasyMailService>();
    }

}
