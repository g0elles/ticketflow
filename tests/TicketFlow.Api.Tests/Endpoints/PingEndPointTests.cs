using System.Net;

using Microsoft.AspNetCore.Mvc.Testing;

namespace TicketFlow.Api.Tests.Endpoints;

public sealed class PingEndPointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public PingEndPointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient(); 
    }
    
    [Fact]
    public async Task? GetPing_WhenApiIsRunning()
    {
        var response = await _client.GetAsync("/ping", CancellationToken.None);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}