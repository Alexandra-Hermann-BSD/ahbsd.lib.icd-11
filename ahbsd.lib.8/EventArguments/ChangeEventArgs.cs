// ahbsd.lib
//     ahbsd.lib.EventArguments
//     ChangeEventArgs.cs
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

namespace ahbsd.lib.EventArguments
{
    /// <summary>
    /// Generic <see cref="EventArgs"/> for changing objects of a defined <see cref="Type"/>.
    /// </summary>
    /// <typeparam name="T">The defined <see cref="Type"/></typeparam>
    public class ChangeEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Constructor with the given old value and the given new value.
        /// </summary>
        /// <param name="oldValue">The given old value</param>
        /// <param name="newValue">The given new value</param>
        public ChangeEventArgs(T oldValue, T newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
        
        /// <summary>
        /// Gets the old value.
        /// </summary>
        /// <value>The old value</value>
        public T OldValue { get; }
        
        /// <summary>
        /// Gets the old value.
        /// </summary>
        /// <value>The old value</value>
        public T NewValue { get; }
    }

    /// <summary>
    /// A specific generic event handler for changing objects.
    /// </summary>
    /// <param name="sender">The sending object</param>
    /// <param name="e">The change event arguments</param>
    /// <typeparam name="T">The defined <see cref="Type"/> of the changing object</typeparam>
    public delegate void ChangeEventHandler<T>(object sender, ChangeEventArgs<T> e);
}