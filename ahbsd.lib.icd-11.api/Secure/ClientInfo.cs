// ahbsd.lib.icd-11
//     ahbsd.lib.icd-11.api
//     ClientInfo.cs
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ahbsd.lib.Extensions;
using ahbsd.lib.icd_11.api.Exceptions;
using ahbsd.lib.icd_11.api.Interfaces;

namespace ahbsd.lib.icd_11.api.Secure
{
    /// <summary>
    /// A simple class for secure client info.
    /// </summary>
    public sealed class ClientInfo : IClientInfo
    {
        /// <summary>
        /// A constructor with the client ID and the client key.
        /// </summary>
        /// <param name="clientId">The client ID</param>
        /// <param name="clientKey">The client key</param>
        internal ClientInfo(string clientId, string clientKey)
        {
            ClientId = clientId;
            ClientKey = clientKey;
        }
        
        #region implementation of IClientInfo
        /// <inheritdoc />
        public string ClientId { get; private set; }
        /// <inheritdoc />
        public string ClientKey { get; private set; }
        #endregion

        /// <summary>
        /// Gets the <see cref="IClientInfo"/> by a given path to the secrets file.
        /// </summary>
        /// <param name="path">The path to the secrets file</param>
        /// <returns>The generated client info</returns>
        /// <exception cref="WrongLineAmountException">If the found file hasn't exactly two lines.</exception>
        /// <exception cref="ArgumentNullException">If the given <paramref name="path"/> is <c>null</c></exception>
        /// <exception cref="FileNotFoundException">If the given <paramref name="path"/> is wrong</exception>
        public static IClientInfo GetClientInfo(string path)
        {
            IClientInfo result;

            if (!path.IsNullOrWhiteSpace() && File.Exists(path))
            {
                IList<string> lines = File.ReadLines(path).ToList();

                if (lines.Count == 2)
                {
                    result = new ClientInfo(lines[0].Trim(), lines[1]);
                }
                else
                {
                    throw new WrongLineAmountException($"The file in the given path '{path}' hasn't 2 lines, but {lines.Count}; ", 
                        2, lines.Count);
                }
            }
            else
            {
                var pathInfo = path == null ? "is null" : $"'{path}' doesn't contains a path";
                throw new ArgumentNullException(nameof(path), $"The given path {pathInfo}");
            }

            return result;
        }
    }
}