// ahbsd.lib
//     ahbsd.lib.Extensions
//     StringExtensions.cs
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

using System.Diagnostics.CodeAnalysis;

namespace ahbsd.lib.Extensions
{
    /// <summary>
    /// Extensions for <see cref="string"/>
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// The same as <see cref="string.IsNullOrEmpty(string)"/> but as extension.
        /// </summary>
        /// <param name="value">The calling <see cref="string"/></param>
        /// <returns>Is the current string <c>null</c> or <see cref="string.Empty"/>?</returns>
        public static bool IsNullOrEmpty([NotNullWhen(false)]this string value) => string.IsNullOrEmpty(value);

        /// <summary>
        /// the same as <see cref="string.IsNullOrWhiteSpace(string)"/> but as extension.
        /// </summary>
        /// <param name="value">The calling <see cref="string"/></param>
        /// <returns>Is the current string <c>null</c> or <c>white space</c>?</returns>
        public static bool IsNullOrWhiteSpace([NotNullWhen(false)]this string value) => string.IsNullOrWhiteSpace(value);
    }
}