using System.Reflection;

namespace TicketFlow.BuildingBlocks;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}