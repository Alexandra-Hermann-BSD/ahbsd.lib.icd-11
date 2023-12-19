// ahbsd.lib.icd-11
//     ahbsd.lib.icd-11.test
//     WrongLineAmountExceptionTest.cs
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
using ahbsd.lib.Extensions;
using ahbsd.lib.icd_11.api.Exceptions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Xunit;

namespace ahbsd.lib.icd_11.test.Exceptions
{
    public class WrongLineAmountExceptionTest
    {
        [Theory]
        [InlineData(2, 0)]
        [InlineData(2, 1)]
        [InlineData(2, 3)]
        [InlineData(0, 2)]
        [InlineData(1, 2)]
        [InlineData(3, 2)]
        [InlineData(3, 3)]
        public void TestException(int expected, int amount)
        {
            if (amount != expected)
            {
                try
                {
                    throw new WrongLineAmountException(expected, amount);
                }
                catch (Exception e)
                {
                    Assert.IsType<WrongLineAmountException>(e);

                    if (e is WrongLineAmountException wrongLineAmountException)
                    {
                        Assert.Equal(expected, wrongLineAmountException.ExpectedLineAmount);
                        Assert.Equal(amount, wrongLineAmountException.RealLineAmount);
                        var lines = expected == 1 ? "line" : "lines";
                        Assert.StartsWith($"{expected} {lines} is", wrongLineAmountException.Message);
                    }
                }
            }
        }
        [Theory]
        [InlineData(null, 2, 0)]
        [InlineData("", 2, 1)]
        [InlineData("This ", 2, 3)]
        [InlineData("   ", 0, 2)]
        [InlineData("Blabla", 1, 2)]
        [InlineData(" Blabla ", 3, 2)]
        [InlineData(null, 3, 3)]
        public void TestExceptionWithMessage(string message, int expected, int amount)
        {
            if (amount != expected)
            {
                try
                {
                    throw new WrongLineAmountException(message, expected, amount);
                }
                catch (Exception e)
                {
                    Assert.IsType<WrongLineAmountException>(e);

                    if (e is WrongLineAmountException wrongLineAmountException)
                    {
                        Assert.Equal(expected, wrongLineAmountException.ExpectedLineAmount);
                        Assert.Equal(amount, wrongLineAmountException.RealLineAmount);
                        var lines = expected == 1 ? "line" : "lines";
                        var expectedMessage = !message.IsNullOrWhiteSpace() ? $"{message.TrimEnd()} " : string.Empty;
                        Assert.StartsWith($"{expectedMessage}{expected} {lines} is", wrongLineAmountException.Message);
                    }
                }
            }
        }
    }
}