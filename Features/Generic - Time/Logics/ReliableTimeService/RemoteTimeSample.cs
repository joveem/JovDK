using System;
using JovDK.SerializingTools.Json;
namespace JovDK.Generic.TimeManagement
{
    public static class RemoteTimeSample
    {
        public const int TimeoutSeconds = 10;
        public static bool TryRead(bool transportSucceeded, string json, out DateTime utc)
        {
            utc = default;
            if (!transportSucceeded || string.IsNullOrWhiteSpace(json)) return false;
            try
            {
                var response = json.DeserializeJsonToObject<WorldTimeResponse>();
                if (response == null || response.DateTime.Year < 2020 || response.DateTime.Kind == DateTimeKind.Unspecified) return false;
                utc = response.DateTime.ToUniversalTime(); return true;
            }
            catch (Exception) { return false; }
        }
    }
}
