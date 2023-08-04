using System;
using System.Collections.Generic;
using Analogy.Interfaces;

namespace Analogy.LogViewer.Log4jXml
{
    static class ChangeLogList
    {
        public static IEnumerable<AnalogyChangeLog> GetChangeLog()
        {
            return new List<AnalogyChangeLog>{
                new AnalogyChangeLog("Initial version", AnalogChangeLogType.None, "Lior Banai", new DateTime(2020, 10, 22), "")
            };
        }
    }
}
