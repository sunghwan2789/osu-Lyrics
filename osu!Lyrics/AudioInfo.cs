﻿using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace osu_Lyrics
{
    internal abstract class AudioInfo
    {
        protected long RawPosition = 0;

        protected int BitRate = 0;

        public double Time(long position)
        {
            return BitRate > 0 ? 8.0 * (position - RawPosition) / BitRate : 0;
        }

        public string Hash { get; protected set; }

        protected void SetHash(Stream s)
        {
            s.Seek(RawPosition, SeekOrigin.Begin);
            var buff = new byte[0x28000];
            var read = s.Read(buff, 0, buff.Length);
            Hash = string.Join("", MD5.Create().ComputeHash(buff, 0, read).Select(i => i.ToString("x2")));
        }
    }
}