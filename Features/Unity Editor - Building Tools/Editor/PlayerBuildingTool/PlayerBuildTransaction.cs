using System;
using System.IO;
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("JovDK.BuildingTools.EditorTests")]
namespace JovDK.Unity.Editor.Build
{
    internal sealed class PlayerBuildTransaction : IDisposable
    {
        public static bool Active { get; private set; }
        public static string Output { get; private set; }
        bool _disposed;
        internal PlayerBuildTransaction(string output)
        {
            if (Active) throw new InvalidOperationException("PlayerBuildingTool transaction already active.");
            Output = Path.GetFullPath(output); Active = true;
        }
        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true; Output = null; Active = false;
        }
    }
}
