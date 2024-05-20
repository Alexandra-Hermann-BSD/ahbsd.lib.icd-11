// ahbsd.lib.icd-11
//     ahbsd.lib.icd-11.test
//     TestException.cs
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
using ahbsd.lib.Interfaces;

namespace ahbsd.lib.icd_11.test.Exceptions
{
    public class TestException : Exception
    {
        public TestException(string message, ILogger.State state) : base(message) => State = state;
        
        public ILogger.State State { get; }
    }
}