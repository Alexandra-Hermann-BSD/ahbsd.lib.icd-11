// ahbsd.lib.icd-11
//     ahbsd.lib.icd-11.api
//     Client.cs
// 
//     Copyright 2023 Alexandra Hermann – Beratung, Software, Design
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
using System.Diagnostics;
using System.IO;
using System.Reflection;
using ahbsd.lib.EventArguments;
using ahbsd.lib.Extensions;
using ahbsd.lib.icd_11.api.Exceptions;
using ahbsd.lib.icd_11.api.Interfaces;
using ahbsd.lib.Interfaces;

namespace ahbsd.lib.icd_11.api.Secure
{
    /// <summary>
    /// A client for the ICD-11 Communication
    /// </summary>
    public class Client : IClient
    {
        private static readonly ILogger logger = Logger.Logger.GetLogger(typeof(Client));
        /// <summary>
        /// The inner client info
        /// </summary>
        private IClientInfo clientInfo;
        /// <summary>
        /// The selected path to the client info
        /// </summary>
        private string path;

        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public Client()
        {
            path = null;
            clientInfo = null;
            PathChanged += OnPathChanged;
        }

        /// <summary>
        /// Constructor with a given path to the secret settings for the client.
        /// </summary>
        /// <param name="path">The given path</param>
        /// <exception cref="WrongLineAmountException">If the <paramref name="path"/> is existent, but the amount of lines is wrong.</exception>
        /// <exception cref="FileNotFoundException">If the given <paramref name="path"/> can't be found.</exception>
        /// <exception cref="ArgumentNullException">If the given <paramref name="path"/> is <c>null</c>.</exception>
        public Client(string path) : this()
        {
            Path = path;
        }
        
        /// <summary>
        /// If the <see cref="Path"/> has changed, the <see cref="Info"/> will be changed as well.
        /// </summary>
        /// <param name="sender">The sending object</param>
        /// <param name="e">EventArguments</param>
        private void OnPathChanged(object sender, ChangeEventArgs<string> e) 
            => clientInfo = !e.NewValue.IsNullOrWhiteSpace() ? ClientInfo.GetClientInfo(e.NewValue) : null;

        #region implementation of IClient

        /// <inheritdoc />
        public event ChangeEventHandler<string> PathChanged;

        /// <inheritdoc />
        public string Path
        {
            get => path;
            set
            {
                if (value != path)
                {
                    ChangeEventArgs<string> changeEventArgs = new ChangeEventArgs<string>(path, value);
                    path = value;
                    var sender = GetSender(false);
                    PathChanged?.Invoke(sender, changeEventArgs);
                }
            }
        }
        
        #endregion

        /// <summary>
        /// Gets the sending object.
        /// </summary>
        /// <param name="current">[optional] current sender or sender before</param>
        /// <returns>The calling object or <c>this</c></returns>
        public static MethodBody GetSender(bool current = true)
        {
            MethodBody result = null;
            StackFrame frame = null;

            var nr = current ? 1 : 2;
            var calls = new StackTrace();
            StackFrame[] frames = calls.GetFrames();

            if (frames?.Length >= nr)
            {
                frame = frames[nr];
            }

            try
            {
                if (frame != null && frame.HasMethod() && frame.GetILOffset() > 0)
                {
                    var methodBase = frame.GetMethod();

                    if (methodBase?.IsAbstract == false)
                    {
                        result = methodBase?.GetMethodBody();
                    }
                }
            }
            catch (Exception e)
            {
                // currently ignore exception
                result = null;
                logger.AddLog(e);
            }

            return result;
        }

        /// <summary>
        /// Gets the secret info for this client.
        /// </summary>
        /// <value>The secret info for this client</value>
        protected internal IClientInfo Info => clientInfo;
    }
}