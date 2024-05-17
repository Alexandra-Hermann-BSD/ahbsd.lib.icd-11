// ahbsd.lib.icd-11
//     ahbsd.lib.icd-11.test
//     ClientInfoTest.cs
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
using ahbsd.lib.icd_11.api.Exceptions;
using ahbsd.lib.icd_11.api.Interfaces;
using ahbsd.lib.icd_11.api.Secure;
using Xunit;

namespace ahbsd.lib.icd_11.test.Secure
{
    public class ClientInfoTest
    {
        [Theory]
        [InlineData("secure1.txt")]
        [InlineData("secure2.txt")]
        [InlineData("secure3.txt")]
        [InlineData(null)]
        [InlineData("")]
        public void TestInterfaceClientInfo(string path)
        {
            IClientInfo clientInfo = null;
            IClient client = null;
            var currentPath = Path.GetFullPath(".");
            var sourcePath = Helper.GetSourcePath(currentPath, path);

            try
            {
                clientInfo = ClientInfo.GetClientInfo(sourcePath);
            }
            catch (Exception e)
            {
                AssertException(path, e);
            }

            if (clientInfo != null)
            {
                Assert.Equal("1", clientInfo.ClientId);
                Assert.Equal("passwort", clientInfo.ClientKey);
            }
            else
            {
                Assert.Null(clientInfo);
            }
            
            try
            {
                client = new Client(sourcePath);
            }
            catch (Exception e)
            {
                AssertException(path, e);
            }

            if (client != null)
            {
                Assert.Equal(sourcePath, client.Path);
            }
        }

        private static void AssertException(string path, Exception exception)
        {
            switch (exception)
            {
                case WrongLineAmountException wrongLineAmountException:
                    Assert.Equal(3, wrongLineAmountException.RealLineAmount);
                    Assert.Equal(2, wrongLineAmountException.ExpectedLineAmount);
                    break;
                case ArgumentNullException argumentNullException:
                    Assert.Equal("path", argumentNullException.ParamName);
                    break;
                case FileNotFoundException fileNotFoundException:
                    if (path != null)
                        Assert.Contains(path, fileNotFoundException.FileName ?? string.Empty);
                    else
                        Assert.Null(path);
                    break;
                default:
                    throw exception;
            }
        }
    }
}