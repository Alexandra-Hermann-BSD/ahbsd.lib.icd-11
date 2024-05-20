// ahbsd.lib
//     ahbsd.lib.Logger
//     Logger.cs
// 
//     Copyright 2024 Alexandra Hermann – Beratung, Software, Design
// 
//     Licensed under the Apache License, Version 2.0 (the "License");
//     you may not use this file except in compliance with the License.
//     You may obtain a copy of the License at
// 
//         http://www.apache.org/licenses/LICENSE-2.0
// 
//     Unless required by applicable law or agreed to in writing, software
//     distributed under the License is distributed on an "AS IS" BASIS,
//     WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//     See the License for the specific language governing permissions and
//     limitations under the License.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using ahbsd.lib.EventArguments;
using ahbsd.lib.Extensions;
using ahbsd.lib.Interfaces;


namespace ahbsd.lib.Logger
{
    /// <summary>
    /// A logger class
    /// </summary>
    public class Logger : ILogger
    {
        private static readonly IList<ILogger> currentLoggers;
        private static string logFilePath;
        private static ILogger.State logState;
        private static TextWriter writer;
        private static readonly StringBuilder logBuilder;
        private const string LOG_FORMAT = "{0} [{1}] - [{2}]: {3}";
        private const string LOG_FORMAT_EXCEPTION = 
            "{0} [{1}] - [{2}]: {3}\n\t\tStackTrace:\n------------\n{4}\n------------\n";
        
#pragma warning disable S3963
        static Logger()
        {
            currentLoggers = new List<ILogger>();
            var pathBuilder = new StringBuilder();
            logState = ILogger.State.Error;
            logBuilder = new StringBuilder();

            pathBuilder.Append(Path.GetTempPath());
            pathBuilder.AppendFormat("{0}.log", DateTime.Today.ToShortDateString());
            
            OnLogFilePathChanged += Logger_OnLogFilePathChanged;

            LogFilePath = pathBuilder.ToString();

        }
#pragma warning restore S3963
        
        /// <summary>
        /// Internal constructor.
        /// </summary>
        /// <param name="logType">[optional] The log type</param>
        private Logger(Type logType = null)
        {
            LogType = logType ?? typeof(object);
            currentLoggers.Add(this);
        }

        private static void Logger_OnLogFilePathChanged(object sender, ChangeEventArgs<string> e)
        {
            if (e.NewValue != e.OldValue)
            {
                if (!e.OldValue.IsNullOrWhiteSpace())
                {
                    CloseWriter();
                }

                if (!e.NewValue.IsNullOrWhiteSpace())
                {
                    try
                    {
                        ReSetWriter(e.NewValue);
                    }
                    catch (Exception ex)
                    {
                        MaybeLogToFirstLogger(ex);
                    }
                }
            }
        }

        private static void MaybeLogToFirstLogger(Exception exception)
        {
            if (currentLoggers.Count > 0 && currentLoggers[0] != null)
            {
                var tempLogger = currentLoggers[0];
                tempLogger.AddLog(exception);
            }
        }

        /// <summary>
        /// Resets the current writer.
        /// </summary>
        /// <param name="newPath">The new path of the log file</param>
        /// <returns>Did it work?</returns>
        private static void ReSetWriter(string newPath)
        {
            CloseWriter();

            if (!newPath.IsNullOrWhiteSpace() && !File.Exists(newPath))
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                File.Create(newPath);
            }

            writer = new StreamWriter(newPath, true, Encoding.Unicode, 100);
        }

        private static void CloseWriter()
        {
            if (writer != null)
            {
                writer.Flush();
                writer.Close();
                writer.Dispose();
                writer = null;
            }
        }

        /// <inheritdoc />
        public string Log
        {
            get => logBuilder.ToString();
            protected internal set
            {
                if (writer != null)
                {
                    writer.Flush();
                    foreach (var line in value.Split('\n'))
                    {
                        writer.WriteLine(line);
                    }
                }
                logBuilder.Clear();
                logBuilder.Append(value);
            }
        }

        /// <inheritdoc />
        public Type LogType { get; }

        /// <summary>
        /// Gets or sets the log file path.
        /// </summary>
        /// <value>The log file path</value>
        /// <remarks>If changed by <c>set</c>, <see cref="OnLogFilePathChanged"/> will be called.</remarks>
        public static string LogFilePath
        {
            get => logFilePath;
            set
            {
                if (value != logFilePath)
                {
                    ChangeEventArgs<string> changeEventArgs = new ChangeEventArgs<string>(logFilePath, value);
                    logFilePath = value;
                    OnLogFilePathChanged?.Invoke(new StackFrame(1), changeEventArgs);
                }
            }
        }

        /// <summary>
        /// Happens, when the <see cref="LogFilePath"/> has changed.
        /// </summary>
        public static event ChangeEventHandler<string> OnLogFilePathChanged;

        /// <summary>
        /// Gets or sets the log status.
        /// </summary>
        /// <value>The log status</value>
        /// <remarks>If changed by <c>set</c>, <see cref="OnLogStateChanged"/> will be called.</remarks>
        public static ILogger.State LogState
        {
            get => logState;
            set
            {
                if (value != logState)
                {
                    ChangeEventArgs<ILogger.State> changeEventArgs = new ChangeEventArgs<ILogger.State>(logState, value);
                    logState = value;
                    OnLogStateChanged?.Invoke(new StackFrame(1), changeEventArgs);
                }
            }
        }

        /// <summary>
        /// Happens, when the <see cref="LogState"/> has changed.
        /// </summary>
        public static event ChangeEventHandler<ILogger.State> OnLogStateChanged;

        /// <inheritdoc />
        public void AddLog(string message, ILogger.State state = ILogger.State.Error)
        {
            if (state <= LogState)
            {
                writer?.WriteLine(LOG_FORMAT, DateTime.Now.ToString(CultureInfo.CurrentCulture), LogType.Name, state, message);

                logBuilder.AppendFormat(LOG_FORMAT, DateTime.Now.ToString(CultureInfo.CurrentCulture), LogType.Name, state, message);
                logBuilder.AppendLine();
            }
        }

        /// <inheritdoc />
        public void AddLog(Exception e)
        {
            var errorState = $"{ILogger.State.Error} - {e.GetType().Name}";
            writer?.WriteLine(LOG_FORMAT_EXCEPTION, DateTime.Now.ToString(CultureInfo.CurrentCulture), LogType.Name, 
                errorState, e.Message, e.StackTrace);

            logBuilder.AppendFormat(LOG_FORMAT_EXCEPTION, DateTime.Now.ToString(CultureInfo.CurrentCulture), 
                LogType.Name, errorState, e.Message, e.StackTrace);
            
            logBuilder.AppendLine();
        }

        private static void ReleaseUnmanagedResources()
        {
            if (writer != null && currentLoggers.Count == 0)
            {
                writer.Flush();
                writer.Close();
                writer.Dispose();
            }
        }

        /// <summary>
        /// Disposes the current logger.
        /// </summary>
        /// <param name="disposing">Is the current object disposing?</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // nothing for now
            }
            else
            {
                currentLoggers.Remove(this);
            }
            ReleaseUnmanagedResources();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc />
        ~Logger()
        {
            Dispose(false);
        }

        /// <summary>
        /// Logger Factory.
        /// </summary>
        /// <param name="type">The type of object to log</param>
        /// <returns>Gets a new logger</returns>
        public static ILogger GetLogger(Type type) => new Logger(type);
    }
}