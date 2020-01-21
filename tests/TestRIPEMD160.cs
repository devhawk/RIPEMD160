using System;
using System.Security.Cryptography;
using Xunit;

namespace RIPEMD160Tests
{
    public class TestRIPEMD160
    {
        static Lazy<RIPEMD160> ripemd160 = new Lazy<RIPEMD160>(() => new RIPEMD160());

        // from https://stackoverflow.com/a/9995303
        static byte[] StringToByteArrayFastest(string hex)
        {
            static int GetHexVal(char hex)
            {
                int val = (int)hex;
                //For uppercase A-F letters:
                //return val - (val < 58 ? 48 : 55);
                //For lowercase a-f letters:
                return val - (val < 58 ? 48 : 87);
                //Or the two combined, but a bit slower:
                //return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
            }

            if (hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits");

            byte[] arr = new byte[hex.Length >> 1];

            for (int i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            }

            return arr;
        }

        static void ComputeHashTest(string text, byte[] expected)
        {
            Assert.Equal(20, expected.Length);

            byte[] asciiBytes = System.Text.Encoding.ASCII.GetBytes(text);
            byte[] actual = ripemd160.Value.ComputeHash(asciiBytes);
            Assert.Equal(expected, actual);
        }

        static void TryComputeHashTest(string text, byte[] expected)
        {
            Assert.Equal(20, expected.Length);

            byte[] asciiBytes = System.Text.Encoding.ASCII.GetBytes(text);

            Span<byte> actual = stackalloc byte[20];
            Assert.True(ripemd160.Value.TryComputeHash(asciiBytes, actual, out var bytesWritten));
            Assert.Equal(20, bytesWritten);
            Assert.True(actual.SequenceEqual(expected));
        }


        // test data for RosettaCode test from http://rosettacode.org/wiki/RIPEMD-160#C.23
        [Fact]
        public void RosettaCode()
        {
            var text = "Rosetta Code";
            var expected = new byte[20] { 0xb3, 0xbe, 0x15, 0x98, 0x60, 0x84, 0x2c, 0xeb, 0xaa, 0x71, 0x74, 0xc8, 0xff, 0xf0, 0xaa, 0x9e, 0x50, 0xa5, 0x19, 0x9f };
            ComputeHashTest(text, expected);
            TryComputeHashTest(text, expected);
        }

        // test data for remaining tests from http://homes.esat.kuleuven.be/~bosselae/ripemd160.html
        [Fact]
        public void EmptyString()
        {
            var text = "";
            var expected = StringToByteArrayFastest("9c1185a5c5e9fc54612808977ee8f548b2258d31");
            ComputeHashTest(text, expected);
            TryComputeHashTest(text, expected);
        }

        [Fact]
        public void a()
        {
            var text = "a";
            var expected = StringToByteArrayFastest("0bdc9d2d256b3ee9daae347be6f4dc835a467ffe");
            ComputeHashTest(text, expected);
            TryComputeHashTest(text, expected);
        }

        [Fact]
        public void abc()
        {
            var text = "abc";
            var expected = StringToByteArrayFastest("8eb208f7e05d987a9b044a8e98c6b087f15a0bfc");
            ComputeHashTest(text, expected);
            TryComputeHashTest(text, expected);
        }
        
        [Fact]
        public void message_digest()
        {
            var text = "message digest";
            var expected = StringToByteArrayFastest("5d0689ef49d2fae572b881b123a85ffa21595f36");
            ComputeHashTest(text, expected);
            TryComputeHashTest(text, expected);
        }

        [Fact]
        public void abcdefghijklmnopqrstuvwxyz()
        {
            var text = "abcdefghijklmnopqrstuvwxyz";
            var expected = StringToByteArrayFastest("f71c27109c692c1b56bbdceb5b9d2865b3708dbc");
            ComputeHashTest(text, expected);
            TryComputeHashTest(text, expected);
        }

        [Fact]
        public void abcdbcdecdefdefgefghfghighijhijkijkljklmklmnlmnomnopnopq()
        {
            var text = "abcdbcdecdefdefgefghfghighijhijkijkljklmklmnlmnomnopnopq";
            var expected = StringToByteArrayFastest("12a053384a9c0c88e405a06c27dcf49ada62eb2b");
            ComputeHashTest(text, expected);
            TryComputeHashTest(text, expected);
        }

        [Fact]
        public void ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789()
        {
            var text = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var expected = StringToByteArrayFastest("b0e20b6e3116640286ed3a87a5713079b21f5189");
            ComputeHashTest(text, expected);
            TryComputeHashTest(text, expected);
        }

        [Fact]
        public void eight_times_1234567890()
        {
            var text = "12345678901234567890123456789012345678901234567890123456789012345678901234567890";
            var expected = StringToByteArrayFastest("9b752e45573d4b39f4dbd3323cab82bf63326bfb");
            ComputeHashTest(text, expected);
            TryComputeHashTest(text, expected);
        }
    }
}
