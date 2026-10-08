using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace JovDK.Diagnostics
{
    public static class LogFileExporter
    {
        public static string Export(string directory, DateTimeOffset now, IEnumerable<string> entries, string bootRecord = null)
        {
            if (entries == null) throw new ArgumentNullException(nameof(entries));
            var snapshot = new List<string>(entries);
            var stem = "logs_" + now.UtcDateTime.ToString("yyyy-MM-dd_HH-mm-ss_fff", CultureInfo.InvariantCulture) + "Z";
            for (int suffix = 0; suffix < 10000; suffix++)
            {
                string path = Path.Combine(directory, stem + (suffix == 0 ? "" : "_" + suffix.ToString(CultureInfo.InvariantCulture)) + ".log");
                FileStream stream;
                try { stream = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.Read); }
                catch (IOException) when (File.Exists(path)) { continue; }
                using (stream)
                using (var writer = new StreamWriter(stream, new UTF8Encoding(false)))
                {
                    if (!string.IsNullOrEmpty(bootRecord)) writer.WriteLine(bootRecord);
                    foreach (var entry in snapshot) writer.WriteLine(entry);
                }
                return path;
            }
            throw new IOException("No unique log filename is available for this timestamp.");
        }
    }
}
