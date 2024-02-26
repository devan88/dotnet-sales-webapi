namespace Dotnet.Sales.WebApi.Configurations
{
    public sealed record CorsPolicy
    {
        public string[] Origins { get; init; } = Array.Empty<string>();

    }
}
