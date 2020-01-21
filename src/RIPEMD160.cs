// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

// Contributed to .NET Foundation by Darren R. Starr - Conscia Norway AS

// Updated to .NET Standard 2.1 by Harry Pierson (aka DevHawk)

namespace System.Security.Cryptography
{
    public sealed class RIPEMD160 : HashAlgorithm
    {
        private readonly RIPEMD160HashProvider hashProvider;

        public RIPEMD160()
        {
            hashProvider = new RIPEMD160HashProvider();
            HashSizeValue = RIPEMD160HashProvider.RequiredBufferLength;
        }

        public override void Initialize()
        {
        }

        protected override void HashCore(byte[] array, int ibStart, int cbSize)
        {
            hashProvider.AppendHashData(array.AsSpan(ibStart, cbSize));
        }

        protected override void HashCore(ReadOnlySpan<byte> source)
        {
            hashProvider.AppendHashData(source);
        }

        protected override byte[] HashFinal()
        {
            return hashProvider.FinalizeHashAndReset();
        }

        protected override bool TryHashFinal(Span<byte> destination, out int bytesWritten)
        {
            return hashProvider.TryFinalizeHashAndReset(destination, out bytesWritten);
        }
    }
}
