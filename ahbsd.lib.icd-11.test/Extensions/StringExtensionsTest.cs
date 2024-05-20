// ahbsd.lib.icd-11
//     ahbsd.lib.icd-11.test
//     StringExtensionsTest.cs
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
using ahbsd.lib.Extensions;
using JetBrains.Annotations;
using Xunit;

namespace ahbsd.lib.icd_11.test.Extensions
{
    [TestSubject(typeof(StringExtensions))]
    public class StringExtensionsTest
    {

        [Theory]
        [InlineData("", true)]
        [InlineData(null, true)]
        [InlineData(" ", false)]
        [InlineData("Test", false)]
        public void Test_IsNullOrEmpty(string value, bool expectedResult)
        {
            Assert.Equal(expectedResult, value.IsNullOrEmpty());
        }

        [Theory]
        [InlineData("", true)]
        [InlineData(null, true)]
        [InlineData(" ", true)]
        [InlineData("Test", false)]
        public void Test_IsNullOrWhitespace(string value, bool expectedResult)
        {
            Assert.Equal(expectedResult, value.IsNullOrWhiteSpace());
        }

        [Theory]
        [InlineData("0,1 ,2, 3, 4", 5)]
        [InlineData("0,1 ,2, , 4", 4)]
        [InlineData("0,1 ,2,, 4", 4)]
        public void Test_GetStringListCount(string value, int expectedCount)
        {
            var result = value.GetStringList();
            Assert.Equal(expectedCount, result.Count);
        }
        
        [Theory]
        [InlineData("0,1 ,2, 3, 4", ',')]
        [InlineData("0.1.2. 3 . 4..", '.')]
        [InlineData("0;1 ;2; 3; 4", ';')]
        public void Test_GetStringListByDelimiter(string value, char delimiter)
        {
            IList<string> expectedResult = new List<string> {"0", "1", "2", "3", "4"};
            IList<string> result = value.GetStringList(delimiter);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("0,1 ,2, 3, 4", 5)]
        [InlineData("0,1 ,2, , 4", 4)]
        [InlineData("0,1 ,2,, 4", 4)]
        public void Test_GetIntListCount(string value, int expectedCount)
        {
            var result = value.GetIntList();
            Assert.Equal(expectedCount, result.Count);
        }
        
        [Theory]
        [InlineData("0,1 ,2, 3, 4", ',')]
        [InlineData("0,1 ,2, 3, 4, hallo,", ',')]
        [InlineData("0.1.2. 3 . 4..", '.')]
        [InlineData("0;1 ;2; 3; 4", ';')]
        public void Test_GetIntListByDelimiter(string value, char delimiter)
        {
            IList<int> expectedResult = new List<int> {0, 1, 2, 3, 4};
            IList<int> result = value.GetIntList(delimiter);
            Assert.Equal(expectedResult, result);
        }
    }
}