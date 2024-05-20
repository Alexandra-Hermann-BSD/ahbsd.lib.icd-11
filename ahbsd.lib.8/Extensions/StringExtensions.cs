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

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;

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
        [ContractAnnotation("null=>true", true)]
        public static bool IsNullOrEmpty([NotNullWhen(false)]this string value) => string.IsNullOrEmpty(value);

        /// <summary>
        /// the same as <see cref="string.IsNullOrWhiteSpace(string)"/> but as extension.
        /// </summary>
        /// <param name="value">The calling <see cref="string"/></param>
        /// <returns>Is the current string <c>null</c> or <c>white space</c>?</returns>
        [ContractAnnotation("null=>true", true)]
        public static bool IsNullOrWhiteSpace([NotNullWhen(false)]this string value) => string.IsNullOrWhiteSpace(value);

        /// <summary>
        /// Gets a list of strings in a string, which are seperated by the optional given <paramref name="separator"/> or
        /// ',' if no seperator is given.
        /// </summary>
        /// <param name="value">The calling <see cref="string"/> object</param>
        /// <param name="separator">[object] The given separator</param>
        /// <returns>A list of the found strings.</returns>
        /// <remarks>
        /// If the given <paramref name="value"/> is <c>null</c> or empty, the returned list will be empty (but not <c>null</c>).
        /// </remarks>
        [JetBrains.Annotations.NotNull]
        public static IList<string> GetStringList([MaybeNull]this string value, char separator = ',')
        {
            List<string> result = new List<string>();

            if (!value.IsNullOrWhiteSpace())
            {
                result.AddRange(value.Split(separator).Where(s => !s.IsNullOrWhiteSpace()).Select(part => part.Trim()));
            }
            
            return result;
        }

        /// <summary>
        /// Gets a list of <see cref="int"/> values in a string, which are seperated by the optional given <paramref name="separator"/> or
        /// ',' if no seperator is given.
        /// </summary>
        /// <param name="value">The calling <see cref="string"/> object</param>
        /// <param name="separator">[object] The given separator</param>
        /// <returns>A list of the found <see cref="int"/> values.</returns>                                                                                          
        /// <remarks>                                                                                                                                
        /// If the given <paramref name="value"/> is <c>null</c> or empty, the returned list will be empty (but not <c>null</c>).                    
        /// </remarks>                                                                                                                               
        [JetBrains.Annotations.NotNull]
        public static IList<int> GetIntList([MaybeNull] this string value, char separator = ',')
        {
            List<int> result = new List<int>();

            foreach (var part in value.GetStringList(separator).Where(s => int.TryParse(s, out _)))
            {
                if (int.TryParse(part, out var i))
                {
                    result.Add(i);
                }
            }
            
            return result;
        }
    }
}