using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
//using System.Windows.Forms;

using VehicleGrabberCore.Configuration;

namespace VehicleGrabberCore.Logger
{
    /// <summary>Class to handle Log window entries based on their Log Level</summary>
    public class Logger
    {
        public const string DateTimeFormatString = "yyyy-MM-dd HH:mm:ss.fff";

        /// <summary>Gets or sets the List of LigItems</summary>
        public List<LogItem> LogItems { get; set; }

        /// <summary>Gets or sets the name of the log file</summary>
        private string LogFile { get; set; }

        public Action<string> TriggerLogLine { get; set; }

        //private static System.Object lockThis = new System.Object();

        FileWriter fw = new FileWriter();

        private string DateTimeFormat;


        private string logDir;

        private ConfigurationObj Conf = null;

        /// <summary>Initializes a new instance of the <see cref="Logger"/> class
        /// Log file will be created automatically if not yet exists, else it can be either a fresh new file or append to the existing file.
        /// Default is create a fresh new log file.</summary>
        /// <param name="writeToGui">CallBack function</param>
        /// <param name="append">True to append to existing log file, False to overwrite and create new log file</param>
        public Logger(ConfigurationObj conf, Action<string> writeToGui, bool append = false)
        {
            this.Conf = conf;
            this.DateTimeFormat = DateTimeFormatString;
            this.LogFile = this.GetLogFileName();
            this.LogItems = new List<LogItem>();
            this.TriggerLogLine = writeToGui;
            this.CreateLogFile(append);
        }


        /// <summary>Initializes a new instance of the <see cref="Logger"/> class
        /// Log file will be created automatically if not yet exists, else it can be either a fresh new file or append to the existing file.
        /// Default is create a fresh new log file.</summary>
        /// <param name="conf">ConfigurationObj - configuration object</param>
        /// <param name="append">True to append to existing log file, False to overwrite and create new log file</param>
        public Logger(ConfigurationObj conf, bool append = false)
        {
            this.Conf = conf;
            this.DateTimeFormat = DateTimeFormatString;
            this.LogFile = this.GetLogFileName();
            this.LogItems = new List<LogItem>();
            this.CreateLogFile(append);
        }

        /// <summary>
        /// Log a debug message
        /// </summary>
        /// <param name="text">Message</param>
        public void Debug(string text)
        {
            this.Add(text, DateTime.Now, LogLevel.DEBUG);
        }

        /// <summary>
        /// Log an error message
        /// </summary>
        /// <param name="text">Message</param>
        public void Error(string text)
        {
            this.Add(text, DateTime.Now, LogLevel.ERROR);
        }

        /// <summary>
        /// Log a fatal error message
        /// </summary>
        /// <param name="text">Message</param>
        public void Fatal(string text)
        {
            this.Add(text, DateTime.Now, LogLevel.FATAL);
        }

        /// <summary>
        /// Log an info message
        /// </summary>
        /// <param name="text">Message</param>
        public void Info(string text)
        {
            this.Add(text, DateTime.Now, LogLevel.INFO);
        }

        /// <summary>
        /// Log a trace message
        /// </summary>
        /// <param name="text">Message</param>
        public void Trace(string text)
        {
            this.Add(text, DateTime.Now, LogLevel.TRACE);
        }

        /// <summary>
        /// Log a waning message
        /// </summary>
        /// <param name="text">Message</param>
        public void Warning(string text)
        {
            this.Add(text, DateTime.Now, LogLevel.WARNING);
        }

        /// <summary>Generate the log file name</summary>
        /// <returns>string - complete file name of the log file</returns>
        private string GetLogFileName()
        {
            this.logDir = this.Conf.LogDirectory.Trim();
            if (this.logDir.Equals(string.Empty))
            {
                this.Conf.LogDirectory =
                    Path.Combine(GetAppPath(Assembly.GetExecutingAssembly().Location), "Log");
                this.logDir = this.Conf.LogDirectory;
            }

            return Path.Combine(
                this.logDir, string.Format("SevenFiles_{0}.log", DateTime.Now.ToString("yyyy-MM-dd")));

            //Assembly.GetExecutingAssembly().GetName().Name + "_" + this.Conf.ReportPeriod +"_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".log");
        }

        private static string GetAppPath(string location)
        {
            return location.Substring(0, location.LastIndexOf(Path.DirectorySeparatorChar));
        }

        /// <summary>Add a log line. This is logged as INFO and with current Date Time</summary>
        /// <param name="msg">string - The message to be logged</param>
        public void Add(string msg)
        {
            DateTime timestamp = DateTime.Now;
            this.Add(msg, timestamp);
        }

        /// <summary>Add a log line. This is logged as INFO</summary>
        /// <param name="msg">string - The message to be logged</param>
        /// <param name="timestamp">DateTime information</param>
        public void Add(string msg, DateTime timestamp)
        {
            LogLevel level = LogLevel.INFO;
            this.Add(msg, timestamp, level);
        }

        /// <summary>Add a log line with given parameters</summary>
        /// <param name="msg">string - The message to be logged</param>
        /// <param name="timestamp">DateTime information</param>
        /// <param name="level">the log level based on LogLevel object (eg. LogLevel.INFO)</param>
        public void Add(string msg, DateTime timestamp, LogLevel level)
        {

            LogItem item = new LogItem(msg, timestamp, level);
            this.LogItems.Capacity++;
            this.LogItems.Add(item);
            this.WriteLineToFile(FormatLogLine(item));
            this.WriteToLogWindow(item);
        }

        /// <summary>Clear the internal log object</summary>
        public void ClearLog()
        {
            this.LogItems.Clear();
        }

        /// <summary>Get all log lines based on the LogLevel settings</summary>
        /// <param name="levels">String list of LogLevels to be displayed</param>
        /// <returns>List of string with log lines</returns>
        private List<string> GetLogLines(List<string> levels = null)
        {

            if (levels == null)
            {
                throw new ArgumentNullException(nameof(levels));
            }

            List<string> logLines = new List<string>();
            List<LogItem> newList = this.LogItems.FindAll(item => levels.Contains(item.Level.ToString()));
            foreach (LogItem item in newList)
            {
                logLines.Capacity++;
                logLines.Add(FormatLogLine(item));
            }

            return logLines;
        }



        /// <summary>Redraw the log window with respective log level entries</summary>
        /// <returns>List of string with log lines</returns>
        public List<string> GetLogLines()
        {
            return this.GetLogLines(GetLogLevelsTodisplay());
        }

        /// <summary>Write the current log line to log window only when log level fits the selection</summary>
        /// <param name="item">LogItem object</param>
        public void WriteToLogWindow(LogItem item)
        {
            if (this.TriggerLogLine != null)
            {
                if (GetLogLevelsTodisplay().Contains(item.Level.ToString()))
                {
                    this.TriggerLogLine(FormatLogLine(item));
                    //Application.DoEvents();
                }
            }
        }









        /// <summary>Format a line that should be logged</summary>
        /// <param name="item">LogItem object</param>
        /// <returns>Return the fhe formatted line</returns>
        private static string FormatLogLine(LogItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return string.Format(
                "{0}    {1}     {2}",
                item.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss:fff"),
                item.Level.ToString(),
                item.Message);
        }

        /// <summary>
        /// Write a line of formatted log message into a log file
        /// </summary>
        /// <param name="text">Formatted log message</param>
        /// <param name="append">True to append, False to overwrite the file</param>
        /// <exception cref="System.IO.IOException"></exception>
        private void WriteLineToFile(string text, bool append = true)
        {
            try
            {
                {
                    this.fw.WriteData(this.LogFile, text, append);
                }

            }
#pragma warning disable 168
            catch (Exception e)
#pragma warning restore 168
            {
                //this.Error(string.Format("{0}: {1}", e.Source, e.Message));
                //throw;
            }
        }

        /// <summary>Get the currently enabled LogLevels for displaying</summary>
        /// <returns>List of string with all selected log levels</returns>
        private List<string> GetLogLevelsTodisplay()
        {
            List<string> strLevel = new List<string>(6);
            if (this.Conf.ShowLogDebug)
            {
                strLevel.Add(LogLevel.DEBUG.ToString());
            }

            if (this.Conf.ShowLogError)
            {
                strLevel.Add(LogLevel.ERROR.ToString());
            }

            if (this.Conf.ShowLogFatal)
            {
                strLevel.Add(LogLevel.FATAL.ToString());
            }

            if (this.Conf.ShowLogInfo)
            {
                strLevel.Add(LogLevel.INFO.ToString());
            }

            if (this.Conf.ShowLogWarning)
            {
                strLevel.Add(LogLevel.WARNING.ToString());
            }

            return strLevel;
        }

        /// <summary>Create or rewrite the log file</summary>
        /// <param name="append">Flag if log should be appanded or not</param>
        private void CreateLogFile(bool append)
        {
            // check and create mssing directory first            
            Directory.CreateDirectory(Path.GetDirectoryName(this.LogFile));
            // Log file header line

            string logHeader = this.LogFile + " is created.";
            LogItem item = new LogItem(logHeader, DateTime.Now, LogLevel.INFO);

            if (!File.Exists(this.LogFile))
            {
                this.WriteLineToFile(FormatLogLine(item), false);
                this.WriteToLogWindow(item);
            }
            else
            {
                if (append == false)
                {
                    this.WriteLineToFile(FormatLogLine(item), false);
                    this.WriteToLogWindow(item);
                }
            }
        }


    }

    /// <summary>Class of LogItem object</summary>
    public class LogItem
    {
        /// <summary>Initializes a new instance of the <see cref="LogItem"/> class</summary>
        /// <param name="msg">The log message</param>
        /// <param name="timestamp">The date time information</param>
        /// <param name="level">The log level information</param>
        public LogItem(string msg, DateTime timestamp, LogLevel level = LogLevel.INFO)
        {
            this.Message = msg;
            this.TimeStamp = timestamp;
            this.Level = level;
        }

        /// <summary>Gets or sets the log level</summary>
        public LogLevel Level { get; set; }

        /// <summary>Gets or sets the date time information</summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>Gets or sets the log message</summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// Supported log level
    /// </summary>
    [Flags]
    public enum LogLevel
    {
        TRACE,
        INFO,
        DEBUG,
        WARNING,
        ERROR,
        FATAL
    }
}
