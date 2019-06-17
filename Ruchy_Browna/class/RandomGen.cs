using System;
using System.Security.Cryptography;

namespace Ruchy_Browna
{
    static class RandomGen
    {
        //  tworzymy obiekt klasy RNGCryptoServiceProvider która podaje nam losowaną przez  
        //mechanizmy kryptograficzne
        //  tablice bajtów 
        //  w odróznieniu do metody rand gdzie losujemy liczbe na podstawie czasu tu losujemy liczbę
         // z róznych zmiennych w systemie
        //  czyli tak zwanej entropi systemu
        private static RNGCryptoServiceProvider rg = new RNGCryptoServiceProvider();
        public static double RNGGenerate()
        {
            int max = int.MaxValue;
            //  ustawiamy ile bitów będziemy losować 
            // 8 bit = 1 byte
            //  liczba short int to 32 bit, więc 32bit / 8bit = 4byte
            // dlatego 4 bajty wpisujemy do tablicy
            byte[] rno = new byte[4];
            // losjemy bity z entropi systemu operacyjnego
            rg.GetBytes(rno);
            //   konwertujemy bity do wartości liczbowej
            int randomValue = Math.Abs(BitConverter.ToInt32(rno, 0));
            //  dzielimy wartość przez zwiększoną o 1 maksymalną wartość dla zmiennej short
            double randomValueBellowZero = (double)randomValue / (max + 1);
            //  mnożymy wartość razy 2PI
            double randomValuePI = randomValueBellowZero * 2 * Math.PI;
            // zwracamy wylosowaną wartość
            return randomValuePI;
        }
        public static void RNGDispose()
        {
            rg.Dispose(); // usuwani z pamięci instancji klasy RNGCryptoServiceProvider
        }
    }
}
