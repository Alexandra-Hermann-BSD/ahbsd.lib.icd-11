// ahbsd.lib.icd-11
//     ahbsd.lib.icd-11.test
//     LoggerTest.cs
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
using ahbsd.lib.Interfaces;
using JetBrains.Annotations;
using Xunit;

namespace ahbsd.lib.icd_11.test.Interfaces
{
    [TestSubject(typeof(ILogger))]
    public class LoggerTest
    {

        [Fact]
        public void TestLogger()
        {
            var logger = Logger.Logger.GetLogger(typeof(LoggerTest));
            Logger.Logger.OnLogStateChanged += Logger_OnLogStateChanged;
            Logger.Logger.OnLogFilePathChanged += Logger_OnLogFilePathChanged;

            Logger.Logger.LogFilePath = string.Empty;
            Logger.Logger.LogState = ILogger.State.Info;

            Exception e = new ArgumentNullException("No Argument", "There is no argument");
            
            logger.AddLog("Test log", ILogger.State.Info);
            logger.AddLog("Debug Log", ILogger.State.Debugging);
            logger.AddLog("Error log");
            logger.AddLog(e);

            Logger.Logger.LogState = ILogger.State.Debugging;
            
            logger.AddLog("Test log", ILogger.State.Info);
            logger.AddLog("Debug Log", ILogger.State.Debugging);
            logger.AddLog("Error log");
            logger.AddLog(e);

            Logger.Logger.LogState = ILogger.State.Error;
            
            logger.AddLog("Test log", ILogger.State.Info);
            logger.AddLog("Debug Log", ILogger.State.Debugging);
            logger.AddLog("Error log");
            logger.AddLog(e);

            Assert.NotEmpty(logger.Log);
        }

        private static void Logger_OnLogFilePathChanged(object sender, ChangeEventArgs<string> e)
        {
            Assert.NotEqual(e.OldValue, e.NewValue);
        }

        private static void Logger_OnLogStateChanged(object sender, ChangeEventArgs<ILogger.State> e)
        {
            Assert.NotEqual(e.NewValue, e.OldValue);
        }
    }
}