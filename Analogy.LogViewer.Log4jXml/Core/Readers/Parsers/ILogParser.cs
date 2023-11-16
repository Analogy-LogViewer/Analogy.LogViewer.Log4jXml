using Logazmic.Core.Log;
using System.Collections.Generic;

namespace Logazmic.Core.Readers.Parsers
{
    public interface ILogParser
    {
        LogMessage ParseLogEvent(string logEvent);
        IEnumerable<LogEventParseItem> SplitToLogEventParseItems(string text);
    }
}