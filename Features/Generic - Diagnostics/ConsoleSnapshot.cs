using System;
using System.Collections.Generic;
namespace JovDK.Diagnostics
{
    // Optional bridge: the existing console owns retention; readers obtain a copy on the Unity thread.
    public static class ConsoleSnapshot
    {
        public static Func<IReadOnlyList<string>> Source { get; set; }
        // Opt-in presentation policy. Collection and retention are independent of this permission.
        public static bool PresentationAllowed { get; private set; }
        public static event Action<bool> PresentationChanged;
        public static event Action SessionReset;
        public static void SetPresentationAllowed(bool allowed)
        {
            if (PresentationAllowed == allowed) return;
            PresentationAllowed = allowed;
            if (PresentationChanged != null)
                foreach (Action<bool> listener in PresentationChanged.GetInvocationList())
                    try { listener(allowed); } catch { /* Diagnostic UI cannot interrupt gameplay. */ }
        }
        public static void Reset()
        {
            Source = null;
            SetPresentationAllowed(false);
            // Surviving collectors rebind when entering Play without domain/scene reload.
            if (SessionReset != null)
                foreach (Action listener in SessionReset.GetInvocationList())
                    try { listener(); } catch { /* A failed collector must not block session initialization. */ }
        }
        public static IReadOnlyList<string> Read() => Source?.Invoke() ?? Array.Empty<string>();
    }
}
