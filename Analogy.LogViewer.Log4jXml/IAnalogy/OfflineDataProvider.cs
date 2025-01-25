using Analogy.Interfaces;
using Analogy.Interfaces.DataTypes;
using Logazmic.Core.Log;
using Logazmic.Core.Readers;
using Logazmic.Core.Receiver;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Analogy.LogViewer.Log4jXml.IAnalogy
{
    public class OfflineDataProvider : Analogy.LogViewer.Template.OfflineDataProvider
    {
        public override Image? SmallImage { get; set; }
        public override Image? LargeImage { get; set; }
        public override string? OptionalTitle { get; set; } = "Log4jXml Parser";
        public override string FileOpenDialogFilters { get; set; } = "log4jxml|*.log4jxml;*.log4j;*.xml;*.xml.*|Flat|*.log";
        public override IEnumerable<string> SupportFormats { get; set; } = new List<string> { "*.log4jxml", "*.log", "*.xml", "*.xml.*" };
        public override string? InitialFolderFullPath { get; set; }
        public override Guid Id { get; set; } = new Guid("f17bf58c-01b7-49b7-9515-cf642fc021ac");
        private readonly ILogReaderFactory _logReaderFactory;

        public OfflineDataProvider()
        {
            _logReaderFactory = new LogReaderFactory();
        }
        public override async Task<IEnumerable<IAnalogyLogMessage>> Process(string fileName, CancellationToken token,
            ILogMessageCreatedHandler messagesHandler)
        {
            List<IAnalogyLogMessage> messages = new List<IAnalogyLogMessage>();
            void FileReceiverNewMessages(object sender, IReadOnlyCollection<Logazmic.Core.Log.LogMessage> e)
            {
                var newMessages = new List<IAnalogyLogMessage>();
                foreach (LogMessage log in e)
                {
                    AnalogyLogMessage m = new AnalogyLogMessage(log.Message, GetLogLevel(log.LogLevel), AnalogyLogClass.General,
                        log.CallSiteClass, "", "", 0, 0, null, "", log.CallSiteMethod, log.SourceFileName, (int)log.SourceFileLineNr);
                    FillProperties(m, log);
                    newMessages.Add(m);
                }
                messages.AddRange(newMessages);
                messagesHandler.AppendMessages(newMessages, fileName);
            }

            void FileReceiverNewMessage(object sender, Logazmic.Core.Log.LogMessage log)
            {
                AnalogyLogMessage m = new AnalogyLogMessage(log.Message, GetLogLevel(log.LogLevel), AnalogyLogClass.General,
                        log.CallSiteClass, "", "", 0, 0, null, "", log.CallSiteMethod, log.SourceFileName, (int)log.SourceFileLineNr);
                messages.Add(m);
                messagesHandler.AppendMessage(m, fileName);
            }
            
            //supporting rolling files like *.xml.1, *.xml.2 ... *.xml.n
            string extension = Path.GetExtension(fileName);
            if (int.TryParse(extension.Substring(1), out _))
            {
                extension = Path.GetExtension(fileName.Substring(0, fileName.Length - extension.Length));
            }

            var fileReceiver = new FileReceiver
            {
                LogReaderFactory = _logReaderFactory,
                FileToWatch = fileName,
                LogFormat = _logReaderFactory.GetLogFormatByFileExtension(extension),
            };

            try
            {
                var tcs = new TaskCompletionSource<IEnumerable<IAnalogyLogMessage>>();
                fileReceiver.NewMessage += FileReceiverNewMessage;
                fileReceiver.NewMessages += FileReceiverNewMessages;
                fileReceiver.OnDoneReadingFile += (s, e) =>
                {
                    tcs.SetResult(messages);
                };
                fileReceiver.Initialize();
                var result = await tcs.Task.ConfigureAwait(false);
                return result;
            }
            catch (Exception e)
            {
                return messages;
            }
            finally
            {
                fileReceiver.NewMessage -= FileReceiverNewMessage;
                fileReceiver.NewMessages -= FileReceiverNewMessages;
            }
        }

        private static void FillProperties(AnalogyLogMessage m, LogMessage log)
        {
            m.Date = log.TimeStamp;
            m.Source = log.LoggerName;
            m.AddOrReplaceAdditionalProperty("Original level", log.LogLevel.ToString());
            foreach (KeyValuePair<string, string> prop in log.Properties)
            {
                switch (prop.Key)
                {
                    case "log4japp":
                        m.Module = prop.Value;
                        continue;
                    case "log4net:UserName":
                        m.User = prop.Value;
                        continue;
                    case "log4net:HostName":
                    case "log4jmachinename":
                        m.MachineName = prop.Value;
                        continue;
                }
            }
        }

        private static AnalogyLogLevel GetLogLevel(Logazmic.Core.Log.LogLevel level)
        {
            switch (level)
            {
                case LogLevel.None:
                    return AnalogyLogLevel.None;
                case LogLevel.Trace:
                case LogLevel.Verbose:
                    return AnalogyLogLevel.Trace;
                case LogLevel.Debug:
                    return AnalogyLogLevel.Debug;
                case LogLevel.Info:
                case LogLevel.Notice:
                    return AnalogyLogLevel.Information;
                case LogLevel.Warn:
                    return AnalogyLogLevel.Warning;
                case LogLevel.Error:
                case LogLevel.Severe:
                    return AnalogyLogLevel.Error;
                case LogLevel.Critical:
                case LogLevel.Fatal:
                case LogLevel.Alert:
                case LogLevel.Emergency:
                    return AnalogyLogLevel.Critical;
                default:
                    return AnalogyLogLevel.Unknown;
            }
        }
    }
}