namespace JovDK.Diagnostics
{
    // Explicit opt-in: the project bootstrap owns lifecycle, not every Core consumer.
    public static class SessionLogContext
    {
        public static string BootRecord { get; private set; }
        public static void Reset() => BootRecord = null;
        public static bool TryCapture(string record)
        {
            if (string.IsNullOrEmpty(record) || BootRecord != null) return false;
            BootRecord = record;
            return true;
        }
    }
}
