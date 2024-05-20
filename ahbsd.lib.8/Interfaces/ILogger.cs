// ahbsd.lib
//     ahbsd.lib.Interfaces
//     ILogger.cs
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
using ahbsd.lib.EventArguments;

namespace ahbsd.lib.Interfaces
{
    /// <summary>
    /// Interface for a logger.
    /// </summary>
    public interface ILogger : IDisposable
    {
        /// <summary>
        /// Gets the current log.
        /// </summary>
        /// <value>The current log</value>
        string Log { get; }
        
        /// <summary>
        /// Gets the current Log type of the log.
        /// </summary>
        /// <value>The current Log type of the log</value>
        Type LogType { get; }
        
        /// <summary>
        /// Adds a log entry with the given message and optional a log state - (by default only errors).
        /// </summary>
        /// <param name="message">The given message</param>
        /// <param name="state">[optional] The log state</param>
        void AddLog(string message, State state = State.Error);

        /// <summary>
        /// Adds a log entry with a given <see cref="Exception"/>.
        /// </summary>
        /// <param name="e">The given exception</param>
        /// <remarks>The <see cref="State"/> will be automatically <see cref="State.Error"/>.</remarks>
        void AddLog(Exception e);
        
        /// <summary>
        /// The logging status.
        /// </summary>
        public enum State
        {
            /// <summary>
            /// Log only on errors.
            /// </summary>
            Error = 0,
            
            /// <summary>
            /// Log more for debugging.
            /// </summary>
            Debugging = 1,
            
            /// <summary>
            /// Log everything.
            /// </summary>
            Info = 2,
        }
    }
}