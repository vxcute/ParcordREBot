using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gee.External.Capstone.Arm64;
using Discord;
using Discord.Commands;
using Gee.External.Capstone;
using SharpDisasm.Translators;
using SharpDisasm;
using SharpDisasm.Udis86;
using Discord.WebSocket;
using System.Net.Http;
using static Emojies.Emojs;
using paracordbot.Bot.Utilities;
using System.IO;

namespace paracordbot.Bot.Utilities
{
    class Utilities
    {

        public static readonly Func<string, string> Hex2Bin = (Hex) => { string Bin = string.Empty; Bin = Convert.ToString(Convert.ToInt64(Hex, 16), 2); return Bin; };
        public static readonly Func<string, string> Bin2Hex = (Bin) => { string Hex = string.Empty; Hex = Convert.ToString(Convert.ToInt64(Bin, 2), 16); return Hex; };
        public static readonly Func<string, string> Dec2Bin = (Dec) => { string Bin = string.Empty; Bin = Convert.ToString(Convert.ToInt64(Dec, 10), 2); return Bin; };
        public static readonly Func<string, string> Bin2Dec = (Bin) => { string Dec = string.Empty; Bin = Convert.ToString(Convert.ToInt64(Dec, 2), 10); return Dec; };
        public static readonly Func<string, string> Base64Decode = (B64) => { string decoded = string.Empty; decoded = Encoding.UTF8.GetString(Convert.FromBase64String(B64)); return decoded; };
        public static readonly Func<string, string> Ascii2Hex = (Ascii) => string.Join(",", Ascii.Select(h => $"0x{(int)h:X}"));
        public static readonly Func<string, int> Hex2Dec = (Hex) => { int Dec = int.Parse(Hex, System.Globalization.NumberStyles.HexNumber); return Dec; };
        public static readonly Func<string, string> Dec2Hex = (Dec) => { string Hex = Convert.ToInt32(Dec).ToString("X"); return Hex; };

        public static string Hex2Ascii(string Hex)
        {
            string ascii = String.Empty;

            for (int i = 0; i < Hex.Length; i += 2)

            {
                string Char2Convert = Hex.Substring(i, 2);
                int n = Convert.ToInt32(Char2Convert, 16);
                char c = (char)n;
                ascii += c.ToString();
            }
            return ascii;
        }


        public static string Base64Encode(string data)
        {
            string encoded = Convert.ToBase64String(Encoding.ASCII.GetBytes(data));
            return encoded;
        }

        public static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static string[] BytesToCByteArray(string hex)
        {
            byte[] bytes = HexStringToByteArray(hex);
            string[] bhex = new string[bytes.Length];
            for (int i = 0; i < bytes.Length; i++)
                bhex[i] = bytes[i].ToString("X");
            return bhex;
        }

 

        public static string Base64Decoded(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            string n = Encoding.ASCII.GetString(bytes);
            return n;
        }

        public static string XOR(string data, string key)
        {
            StringBuilder Builder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                Builder.Append((char)data[i] ^ key[(i % key.Length)]);
            return Builder.ToString();
        }

        public static string Dec2Ascii(string dec)
        {
            string ascii = string.Empty;
            for (int i = 0; i < dec.Length; i += 3)
                ascii += (char)Convert.ToByte(dec.Substring(i, 3));
            return ascii;
        }
    }
}
