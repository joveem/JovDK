// system / unity
using System;

namespace JovDK.Core
{
    /// <summary>
    /// Identifiable contract for objects that expose a Guid Id.
    /// </summary>
    public interface IIdentifiable
    {
        Guid Id { get; }
    }
}
