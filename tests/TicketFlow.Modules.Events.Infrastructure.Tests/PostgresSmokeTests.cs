using Npgsql;

using TicketFlow.Modules.Events.Infrastructure.Tests.Fixtures;

using Xunit;

namespace TicketFlow.Modules.Events.Infrastructure.Tests;

public sealed class PostgresSmokeTests
{
    private readonly PostgresFixture _postgresFixture;
    public PostgresSmokeTests(PostgresFixture fixture) => _postgresFixture = fixture;

    [Fact]
    public async Task Database_WhenContainerIsRunning_AnswersQueries()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await using var connection = new NpgsqlConnection(_postgresFixture.ConnectionString);
        await connection.OpenAsync(cancellationToken);
        await using var command = new NpgsqlCommand("SELECT 1;", connection);
        var result = await command.ExecuteScalarAsync(cancellationToken);
        
        Assert.Equal(1, result);
    }
}