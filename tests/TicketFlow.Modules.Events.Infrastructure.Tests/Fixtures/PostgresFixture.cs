using Testcontainers.PostgreSql;

using Xunit;

namespace TicketFlow.Modules.Events.Infrastructure.Tests.Fixtures;

public sealed class PostgresFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgresContainer = new PostgreSqlBuilder("postgres:18").Build();
    public string ConnectionString => _postgresContainer.GetConnectionString();
    public async ValueTask InitializeAsync()
    {
        await _postgresContainer.StartAsync();
    }
    public ValueTask DisposeAsync() => _postgresContainer.DisposeAsync();


}