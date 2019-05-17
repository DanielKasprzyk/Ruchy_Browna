using System;
using System.Security.Cryptography;

namespace Ruchy_Browna
{
    static class RandomGen
    {
        private static RNGCryptoServiceProvider rg = new RNGCryptoServiceProvider();
        public static double RNGGenerate()
        {
            byte[] rno = new byte[2];
            rg.GetBytes(rno);
            short randomValue = Math.Abs(BitConverter.ToInt16(rno, 0));
            double randomValueBellowZero = (double)randomValue / (short.MaxValue + 1);
            double randomValuePI = randomValueBellowZero * 2 * Math.PI;
            return randomValuePI;
        }
        public static void RNGDispose()
        {
            rg.Dispose();
        }
    }
}
