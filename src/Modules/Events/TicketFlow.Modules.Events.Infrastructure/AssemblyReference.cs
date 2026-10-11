using System.Reflection;

namespace TicketFlow.Modules.Events.Infrastructure;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}