using System.Reflection;

namespace TicketFlow.Modules.Events.Contracts;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}