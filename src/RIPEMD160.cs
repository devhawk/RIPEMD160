// Copyright (c) Harry Pierson. All rights reserved.
// Licensed under the MIT license. 
// See LICENSE file in the project root for full license information.

using System;
using System.Security.Cryptography;

namespace DevHawk.Security.Cryptography
{
    public sealed partial class RIPEMD160 : HashAlgorithm
    {
        public new const int HashSize = RIPEMD160HashProvider.RequiredBufferLength;

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
