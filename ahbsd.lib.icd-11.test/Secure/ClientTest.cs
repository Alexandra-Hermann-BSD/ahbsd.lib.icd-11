// ahbsd.lib.icd-11
//     ahbsd.lib.icd-11.test
//     ClientTest.cs
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
using ahbsd.lib.EventArgs;
using ahbsd.lib.icd_11.api.Exceptions;
using ahbsd.lib.icd_11.api.Interfaces;
using ahbsd.lib.icd_11.api.Secure;
using Xunit;

namespace ahbsd.lib.icd_11.test.Secure
{
    public class ClientTest
    {
        
        [Fact]
        public void TestClient()
        {
            var path = "secure1.txt";
            var currentPath = Path.GetFullPath(".");
            var sourcePath = Helper.GetSourcePath(currentPath, path);
            IClient client = new Client();
            client.PathChanged += Client_OnPathChanged;
            client.Path = sourcePath;
            
            var currentSender = Client.GetSender();
            var preSender = Client.GetSender(false);
            Assert.NotEqual(preSender, currentSender);
            
            path = "secure2.txt";
            sourcePath = Helper.GetSourcePath(currentPath, path);
            try
            {
                client.Path = sourcePath;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case WrongLineAmountException wrongLineAmountException:
                        Assert.Equal(3, wrongLineAmountException.RealLineAmount);
                        Assert.Equal(2, wrongLineAmountException.ExpectedLineAmount);
                        break;
                    case ArgumentNullException argumentNullException:
                        Assert.Equal("path", argumentNullException.ParamName);
                        break;
                    default:
                        throw e;
                }
            }
        }

        private void Client_OnPathChanged(object sender, ChangeEventArgs<string> e)
        {
            Assert.NotEqual(e.OldValue, e.NewValue);
            Assert.NotEqual(this, sender);
        }
    }
}