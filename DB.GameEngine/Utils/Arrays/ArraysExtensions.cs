using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DB.GameEngine.Utils.Arrays
{
    public static class ArraysExtensions
    {
        public static GameArray<T> Quad1<T>(this GameArray<T> array)
        {
            return array.GetSubArray(0, 0, GetMidLength(array));
        }

        public static GameArray<T> Quad2<T>(this GameArray<T> array)
        {
            return array.GetSubArray(0, array.Length / 2, GetMidLength(array));
        }

        public static GameArray<T> Quad3<T>(this GameArray<T> array)
        {
            return array.GetSubArray(array.Length / 2, 0, GetMidLength(array));
        }

        public static GameArray<T> Quad4<T>(this GameArray<T> array)
        {
            return array.GetSubArray(array.Length / 2, array.Length / 2, GetMidLength(array));
        }

        public static void Print<T>(this GameArray<T> array)
        {
            Debug.WriteLine("-------------");
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    Debug.Write($" {array[i, j]}");
                }
                Debug.WriteLine("");
            }
            Debug.WriteLine("-------------");
        }

        private static int GetMidLength<T>(GameArray<T> array)
        {
            return array.Length > 2 ? array.Length / 2 + 1 : 1;
        }
    }
}
