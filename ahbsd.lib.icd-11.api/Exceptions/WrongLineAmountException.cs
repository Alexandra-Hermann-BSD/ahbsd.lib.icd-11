// ahbsd.lib.icd-11
//     ahbsd.lib.icd-11.api
//     WrongLineAmountException.cs
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
using System.Text;
using ahbsd.lib.Extensions;

namespace ahbsd.lib.icd_11.api.Exceptions
{
    /// <summary>
    /// An <see cref="Exception"/> that happens, if the <see cref="RealLineAmount"/> in't the <see cref="ExpectedLineAmount"/>.
    /// </summary>
    public class WrongLineAmountException : Exception
    {
        /// <summary>
        /// Constructor with a predefined message, the expected amount of lines and the real amount of lines.
        /// </summary>
        /// <param name="message">The predefined message</param>
        /// <param name="expectedLineAmount">The expected amount of lines</param>
        /// <param name="realLineAmount">The real amount of lines</param>
        public WrongLineAmountException(string message, int expectedLineAmount, int realLineAmount) : base(GetMessage(expectedLineAmount, realLineAmount, message))
        {
            ExpectedLineAmount = expectedLineAmount;
            RealLineAmount = realLineAmount;
        }

        /// <summary>
        /// Constructor with the expected amount of lines and the real amount of lines.
        /// </summary>
        /// <param name="expectedLineAmount">The expected amount of lines</param>
        /// <param name="realLineAmount">The real amount of lines</param>
        public WrongLineAmountException(int expectedLineAmount, int realLineAmount) : base(GetMessage(expectedLineAmount, realLineAmount))
        {
            ExpectedLineAmount = expectedLineAmount;
            RealLineAmount = realLineAmount;
        }
        
        /// <summary>
        /// Gets the expected amount of lines.
        /// </summary>
        /// <value>The expected amount of lines</value>
        public int ExpectedLineAmount { get; }
        
        /// <summary>
        /// Gets the real amount of lines.
        /// </summary>
        /// <value>The real amount of lines</value>
        public int RealLineAmount { get; }

        /// <summary>
        /// Gets the message from the given <paramref name="expected"/> number of lines and the given <paramref name="real"/> number of lines.
        /// </summary>
        /// <param name="expected">The given expected number of lines</param>
        /// <param name="real">The given real number of lines</param>
        /// <param name="message">[optional] A message before the generated message</param>
        /// <returns>The generated message</returns>
        private static string GetMessage(int expected, int real, string message = null)
        {
            var builder = new StringBuilder();

            if (!message.IsNullOrWhiteSpace())
            {
                builder.Append(message.TrimEnd());
                builder.Append(' ');
            }

            builder.Append($"{expected} ");
            IsOneOrNot(expected, builder);

            if (expected > real)
            {
                builder.Append($" is greater than the real amount of {real} ");
            }
            else if (real > expected)
            {
                builder.Append($" is smaller than the real amount of {real} ");
            }
            else
            {
                builder.Append($" is equal to the real amount of {real} ");
            }
            
            IsOneOrNot(real, builder);
            builder.Append('.');

            return builder.ToString();
        }

        /// <summary>
        /// Check's whether the given <paramref name="numberOfLines"/> is exact one or not.
        /// If exact one "line" will be added, otherwise "lines".
        /// </summary>
        /// <param name="numberOfLines">The number of lines</param>
        /// <param name="builder">The <see cref="StringBuilder"/> to use</param>
        /// <example>
        /// One line or zero lines or two lines, etc.
        /// </example>
        private static void IsOneOrNot(int numberOfLines, StringBuilder builder) => builder.Append(numberOfLines == 1 ? "line" : "lines");
    }
}