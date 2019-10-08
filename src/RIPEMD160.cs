// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

// Contributed to .NET Foundation by Darren R. Starr - Conscia Norway AS

// Updated to .NET Standard 2.1 by Harry Pierson (aka DevHawk)

namespace System.Security.Cryptography
{
    public abstract class RIPEMD160 : System.Security.Cryptography.HashAlgorithm
    {
        public RIPEMD160()
        {
        }

        public new static RIPEMD160 Create()
        {
            return new RIPEMD160Managed();
        }

        public new static RIPEMD160 Create(string hashname)
        {
            return new RIPEMD160Managed();            
        }
    }
}
