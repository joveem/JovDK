using System;
namespace JovDK.Diagnostics
{
    [Serializable]
    public sealed class PlayerBuildIdentity
    {
        public string buildId = "unavailable", source = "unavailable", builtUtc = "unavailable";
        public string tool = "unavailable", version = "unavailable", identifier = "unavailable", platform = "unavailable";
        public int bundleCode;
        public bool development;
    }
}
