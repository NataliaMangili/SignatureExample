using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;

namespace Helpers.Channels;

public static class ChannelHelperExtension
{
    public static GrpcChannel CreateGrpcChannel(IConfiguration config, string serviceName)
    {
        string channelUrl = GetServiceUrl(config, serviceName);
        if (string.IsNullOrEmpty(channelUrl))
        {
            throw new InvalidOperationException($"Service URL for '{serviceName}' is not valid/configured.");
        }

        return GrpcChannel.ForAddress(channelUrl, CreateGrpcChannelOptions());
    }

    private static string GetServiceUrl(IConfiguration config, string serviceName)
    {
        string? url = config[$"services:{serviceName}"];
        if (string.IsNullOrEmpty(url))
        {
            throw new InvalidOperationException($"Configuration for '{serviceName}Url' is missing or empty.");
        }
        return url;
    }

    private static GrpcChannelOptions CreateGrpcChannelOptions()
    {
        return new GrpcChannelOptions { HttpHandler = CreateHttpClientHandler() };
    }

    private static HttpClientHandler CreateHttpClientHandler()
    {
        return new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
    }
}