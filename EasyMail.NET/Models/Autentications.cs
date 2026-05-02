using System.Diagnostics.CodeAnalysis;

namespace EasyMail.NET.Models;

[ExcludeFromCodeCoverage]
public sealed class Autentications
{

    public required string Username { get; init; }
    public required string Password { get; init; }

    private Autentications() { }

    public static Autentications Create(string username, string password)
    {
        return new Autentications
        {
            Username = username,
            Password = password
        };
    }
}
