using System;
using System.Collections.Generic;
namespace JovDK.Diagnostics
{
    // Optional bridge: the existing console owns retention; readers obtain a copy on the Unity thread.
    public static class ConsoleSnapshot
    {
        public static Func<IReadOnlyList<string>> Source { get; set; }
        public static IReadOnlyList<string> Read() => Source?.Invoke() ?? Array.Empty<string>();
    }
}
