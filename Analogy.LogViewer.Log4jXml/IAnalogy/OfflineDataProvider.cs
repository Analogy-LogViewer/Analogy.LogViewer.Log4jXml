using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Analogy.Interfaces;
using Analogy.Interfaces.DataTypes;
using Analogy.LogViewer.Log4jXml.Parser;

namespace Analogy.LogViewer.Log4jXml.IAnalogy
{
    public class OfflineDataProvider : Analogy.LogViewer.Template.OfflineDataProvider
    {
        public override Image? SmallImage { get; set; } = null;
        public override Image? LargeImage { get; set; } = null;
        public override string? OptionalTitle { get; set; } = "Log4jXml Parser";
        public override string FileOpenDialogFilters { get; set; } = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
        public override IEnumerable<string> SupportFormats { get; set; } = new List<string> { "*.txt" };
        public override string? InitialFolderFullPath { get; set; } = null;
        public override Guid Id { get; set; } = new Guid("f17bf58c-01b7-49b7-9515-cf642fc021ac");
        private PlainTextLogFileLoader parser;
        private SplitterLogParserSettings LogParserSettings { get; set; }

        public OfflineDataProvider()
        {
            LogParserSettings = new SplitterLogParserSettings
            {
                Splitter = "|",
                SupportedFilesExtensions = new List<string> {"*.txt"},
                Maps = new Dictionary<int, AnalogyLogMessagePropertyName>
                {
                    {0, AnalogyLogMessagePropertyName.Date},
                    {1, AnalogyLogMessagePropertyName.Level},
                    {2, AnalogyLogMessagePropertyName.Source},
                    {3, AnalogyLogMessagePropertyName.Text}
                },
                IsConfigured = true
            };
            parser = new PlainTextLogFileLoader(LogParserSettings);
        }
        public override Task<IEnumerable<AnalogyLogMessage>> Process(string fileName, CancellationToken token,
            ILogMessageCreatedHandler messagesHandler)
            => parser.Process(fileName, token, messagesHandler);
    }
}
