// ahbsd.lib.icd-11
//     ahbsd.lib.icd-11.api
//     IClient.cs
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
using System.IO;
using ahbsd.lib.EventHandler;
using ahbsd.lib.icd_11.api.Exceptions;

namespace ahbsd.lib.icd_11.api.Interfaces
{
    /// <summary>
    /// An interface for the ICD-11 Client
    /// </summary>
    public interface IClient
    {
        /// <summary>
        /// Gets or sets the path to the secrets for this client.
        /// </summary>
        /// <value>The path to the secrets for this client</value>
        /// <exception cref="WrongLineAmountException">If the path is existent, but the amount of lines is wrong.</exception>
        /// <exception cref="FileNotFoundException">If the given path can't be found.</exception>
        /// <exception cref="ArgumentNullException">If the given Path is <c>null</c>.</exception>
        string Path { get; set; }

        /// <summary>
        /// Happens, when the <see cref="System.IO.Path"/> has changed.
        /// </summary>
        event ChangeEventHandler<string> PathChanged;
    }
}