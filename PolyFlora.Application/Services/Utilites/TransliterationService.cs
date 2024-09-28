using AnyAscii;
using System.Buffers;
using System.Text.RegularExpressions;
using System.Text;

namespace PolyFlora.Application.Services.Utilites
{
    public class TransliterationService
    {
        public string ToUrlFriendly(string str)
        {
            ArrayBufferWriter<byte> writer = new ArrayBufferWriter<byte>(str.Length);
            foreach (Rune r in str.EnumerateRunes())
            {
                r.Transliterate(writer);
            }
            var lstring = Encoding.ASCII.GetString(writer.WrittenSpan).ToLowerInvariant();
            var optzstring = Regex.Replace(lstring, @"[^a-z0-9]+", "-").Trim('-');
            return optzstring;
        }
    }
}
