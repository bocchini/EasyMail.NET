

namespace EasyMail.NET.Models;

public sealed class ServerConfiguration
{
    public required string Host { get; init; }
    public required int Port { get; init; }
    public bool EnableSSL { get; init; }

    private ServerConfiguration() { }

    public static ServerConfiguration Create(string host, int port, bool enableSSL)
    {
        return new ServerConfiguration
        {
            Host = host,
            Port = port,
            EnableSSL = enableSSL
        };
    }
}
