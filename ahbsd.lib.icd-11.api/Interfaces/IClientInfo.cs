// ahbsd.lib.icd-11
//     ahbsd.lib.icd-11.api
//     IClientInfo.cs
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

namespace ahbsd.lib.icd_11.api.Interfaces
{
    /// <summary>
    /// Interface for secure client info.
    /// </summary>
    public interface IClientInfo
    {
        /// <summary>
        /// Gets the client ID.
        /// </summary>
        /// <value>The client ID</value>
        string ClientId { get; }
        
        /// <summary>
        /// Gets the client key.
        /// </summary>
        /// <value>The client key</value>
        string ClientKey { get; }
    }
}