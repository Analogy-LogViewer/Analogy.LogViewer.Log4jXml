using Logazmic.Core.Log;
using System.Collections.Generic;
using System.IO;

namespace Logazmic.Core.Readers
{
    public interface ILogStreamReader
    {
        string DefaultLogger { get; set; }
        int BufferSize { get; set; }

        IEnumerable<LogMessage> NextLogEvents(Stream stream, out int bytesRead);
    }
}