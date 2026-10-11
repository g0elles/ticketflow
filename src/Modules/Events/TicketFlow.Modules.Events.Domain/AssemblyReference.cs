using System.Reflection;

namespace TicketFlow.Modules.Events.Domain;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}