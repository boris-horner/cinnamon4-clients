// Copyright 2012,2024 texolution GmbH
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not 
// use this file except in compliance with the License. You may obtain a copy of 
// the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT 
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
// License for the specific language governing permissions and limitations under 
// the License.
using System;

namespace C4ServerConnector.Exceptions
{
    [Serializable]
    public class InternalServerErrorException : Exception
    {
        // Default constructor
        public InternalServerErrorException() { }

        // Constructor that accepts a custom message
        public InternalServerErrorException(string message)
            : base(message) { }

        // Constructor that accepts a custom message and an inner exception
        public InternalServerErrorException(string message, Exception innerException)
            : base(message, innerException) { }

        // Protected constructor to de-serialize data
        protected InternalServerErrorException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
