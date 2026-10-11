using System.Reflection.Metadata;

using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnitV3;

using Xunit;

using static ArchUnitNET.Fluent.ArchRuleDefinition;
using ArchModel = ArchUnitNET.Domain.Architecture;

namespace TicketFlow.Architecture.Tests;

public sealed class LayerDependencyTests
{
    // TODO: static readonly Architecture built with new ArchLoader()
    //       .LoadAssemblies(...the 6 AssemblyReference.Assembly values...)
    //       .Build();
private static readonly ArchModel ArchitectureModel = new ArchLoader()
    .LoadAssemblies(TicketFlow.Api.AssemblyReference.Assembly,
                    TicketFlow.BuildingBlocks.AssemblyReference.Assembly,
                    TicketFlow.Modules.Events.Contracts.AssemblyReference.Assembly,
                    TicketFlow.Modules.Events.Domain.AssemblyReference.Assembly,
                    TicketFlow.Modules.Events.Application.AssemblyReference.Assembly,
                    TicketFlow.Modules.Events.Infrastructure.AssemblyReference.Assembly)
    .Build();

    // Example layer definition (write one per project):
    private static readonly IObjectProvider<IType> DomainLayer =
        Types().That().ResideInAssembly(TicketFlow.Modules.Events.Domain.AssemblyReference.Assembly).As("Events Domain");

    private static readonly IObjectProvider<IType> ApplicationLayer =
        Types().That().ResideInAssembly(TicketFlow.Modules.Events.Application.AssemblyReference.Assembly).As("Events Application");

    private static readonly IObjectProvider<IType> ContractsLayer =
        Types().That().ResideInAssembly(TicketFlow.Modules.Events.Contracts.AssemblyReference.Assembly).As("Events Contracts");

    private static readonly IObjectProvider<IType> InfrastructureLayer =
        Types().That().ResideInAssembly(TicketFlow.Modules.Events.Infrastructure.AssemblyReference.Assembly).As("Events Infrastructure");

    private static readonly IObjectProvider<IType> BuildingBlocksLayer =
        Types().That().ResideInAssembly(TicketFlow.BuildingBlocks.AssemblyReference.Assembly).As("Building Blocks");

    private static readonly IObjectProvider<IType> ApiLayer =
        Types().That().ResideInAssembly(TicketFlow.Api.AssemblyReference.Assembly).As("API");    

    // Tests come in Task 4

    [Fact]
    public void Domain_ShouldNotDependOn_ApplicationInfrastructureContractsOrApi()
    {
        IArchRule rule = Types().That().Are(DomainLayer)
        .Should().NotDependOnAny(ApplicationLayer)
        .AndShould().NotDependOnAny(InfrastructureLayer)
        .AndShould().NotDependOnAny(ContractsLayer)
        .AndShould().NotDependOnAny(ApiLayer);
     rule.Check(ArchitectureModel);
    }

    [Fact]
    public void Application_ShouldNotDependOn_InfrastructureOrApi()
    {
        IArchRule rule = Types().That().Are(ApplicationLayer)
        .Should().NotDependOnAny(InfrastructureLayer)
        .AndShould().NotDependOnAny(ApiLayer);
     rule.Check(ArchitectureModel);
    }

    [Fact]
    public void Infrastructure_ShouldNotDependOn_Api(){
        IArchRule rule = Types().That().Are(InfrastructureLayer)
        .Should().NotDependOnAny(ApiLayer);
     rule.Check(ArchitectureModel);
    }

    [Fact]
    public void Contracts_ShouldNotDependOn_AnyOtherLayer()
    {
        IArchRule rule = Types().That().Are(ContractsLayer)
        .Should().NotDependOnAny(DomainLayer)
        .AndShould().NotDependOnAny(ApplicationLayer)
        .AndShould().NotDependOnAny(InfrastructureLayer)
        .AndShould().NotDependOnAny(ApiLayer);
     rule.Check(ArchitectureModel);
    }

    [Fact]
    public void BuildingBlocks_ShouldNotDependOn_AnyOtherLayer()
    {
        IArchRule rule = Types().That().Are(BuildingBlocksLayer)
        .Should().NotDependOnAny(DomainLayer)
        .AndShould().NotDependOnAny(ApplicationLayer)
        .AndShould().NotDependOnAny(InfrastructureLayer)
        .AndShould().NotDependOnAny(ContractsLayer)
        .AndShould().NotDependOnAny(ApiLayer);
     rule.Check(ArchitectureModel);
    }

    [Fact]
    public void Api_ShouldNotDependOn_Domain()
    {
        IArchRule rule = Types().That().Are(ApiLayer)
        .Should().NotDependOnAny(DomainLayer);
     rule.Check(ArchitectureModel);
    }

    [Fact]
    public void DomainAndBuildingBlocks_ShouldNotDependOn_EfCoreASPNetCoreOrNpgsql()
    {
        IObjectProvider<IType> EfCoreLayer = Types().That().ResideInAssembly("Microsoft.EntityFrameworkCore").As("EF Core");
        IObjectProvider<IType> AspNetCoreLayer = Types().That().ResideInAssembly("Microsoft.AspNetCore").As("ASP.NET Core");
        IObjectProvider<IType> NpgsqlLayer = Types().That().ResideInAssembly("Npgsql").As("Npgsql");

        IArchRule rule = Types().That().Are(DomainLayer)
        .Should().NotDependOnAny(EfCoreLayer)
        .AndShould().NotDependOnAny(AspNetCoreLayer)
        .AndShould().NotDependOnAny(NpgsqlLayer);

        rule.Check(ArchitectureModel);

        rule = Types().That().Are(BuildingBlocksLayer)
        .Should().NotDependOnAny(EfCoreLayer)
        .AndShould().NotDependOnAny(AspNetCoreLayer)
        .AndShould().NotDependOnAny(NpgsqlLayer);

        rule.Check(ArchitectureModel);
    }

}