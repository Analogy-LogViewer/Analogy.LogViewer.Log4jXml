namespace Logazmic.Core.Log
{
    using System;

    [Serializable]
    public enum LogLevel
    {
        None = -1,
        Verbose,
        Trace,
        Debug,
        Info,
        Notice,
        Warn,
        Error,
        Severe,
        Critical,
        Alert,
        Fatal,
        Emergency
    }
}