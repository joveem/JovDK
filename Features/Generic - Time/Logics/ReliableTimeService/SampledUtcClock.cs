using System;

namespace JovDK.Generic.TimeManagement
{
    public sealed class SampledUtcClock
    {
        readonly Func<double> _elapsed;
        DateTime _utc;
        double _sample;
        public SampledUtcClock(Func<double> elapsed) { _elapsed = elapsed ?? throw new ArgumentNullException(nameof(elapsed)); }
        public void SetSample(DateTime utc) { _utc = utc.ToUniversalTime(); _sample = _elapsed(); }
        public DateTime UtcNow => _utc.AddSeconds(System.Math.Max(0, _elapsed() - _sample));
    }
}
