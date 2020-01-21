// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

// Contributed to .NET Foundation by Darren R. Starr - Conscia Norway AS

// Updated to .NET Standard 2.1 by Harry Pierson (aka DevHawk)

namespace System.Security.Cryptography
{
    public sealed partial class RIPEMD160
    {
        public sealed class IncrementalHash
        {
            private readonly RIPEMD160HashProvider hashProvider;

            public const int HashSize = RIPEMD160HashProvider.RequiredBufferLength;

            public IncrementalHash()
            {
                hashProvider = new RIPEMD160HashProvider();
            }

            public void AppendData(ReadOnlySpan<byte> data)
            {
                hashProvider.AppendHashData(data);
            }

            public void AppendData(byte[] data)
            {
                if (data == null)
                {
                    throw new ArgumentNullException(nameof(data));
                }

                AppendData(data.AsSpan());
            }

            public void AppendData(byte[] data, int offset, int count)
            {
                if (data == null)
                    throw new ArgumentNullException(nameof(data));
                if (offset < 0)
                    throw new ArgumentOutOfRangeException(nameof(offset));
                if (count < 0 || (count > data.Length))
                    throw new ArgumentOutOfRangeException(nameof(count));
                if ((data.Length - count) < offset)
                    throw new ArgumentException();

                AppendData(data.AsSpan(offset, count));
            }

            public bool TryGetHashAndReset(Span<byte> destination, out int bytesWritten)
            {
                return hashProvider.TryFinalizeHashAndReset(destination, out bytesWritten);
            }

            public byte[] GetHashAndReset()
            {
                return hashProvider.FinalizeHashAndReset();
            }
        }
    }
}
