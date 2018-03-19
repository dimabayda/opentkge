using DB.GameEngine.Utils.Arrays;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DB.GameEngine.Utils
{
    public static class GameEngineExtensions
    {
        public static float[] Pack(this Vector3 vector)
        {
            return new float[] { vector.X, vector.Y, vector.Z };
        }

        public static float[] Pack(this Vector2 vector)
        {
            return new float[] { vector.X, vector.Y };
        }

        public static float NextFloat(this Random random, float maxValue = 1)
        {
            return (float)(random.NextDouble() - 0.5) * maxValue;
        }

        public static float ToFloat(this string str)
        {
            CultureInfo cultureInfo = CultureInfo.CurrentCulture.Clone() as CultureInfo;
            cultureInfo.NumberFormat.CurrencyDecimalSeparator = ".";
            return float.Parse(str, NumberStyles.Any, cultureInfo);
        }

        public static float[] ToFloatArray(this string str, char separator)
        {
            string[] parts = str.Split(separator);
            return parts.ToFloatArray();
        }

        public static float[] ToFloatArray(this string[] strArray)
        {
            float[] arr = new float[strArray.Length];
            for(int i = 0; i < strArray.Length; i++)
            {
                arr[i] = strArray[i].ToFloat();
            }
            return arr;
        }

        public static int ToInt(this string str)
        {
            return int.Parse(str);
        }

        public static int[] ToIntArray(this string str, char separator)
        {
            string[] parts = str.Split(separator);
            return parts.ToIntArray();
        }

        public static int[] ToIntArray(this string[] strArray)
        {
            int[] arr = new int[strArray.Length];
            for (int i = 0; i < strArray.Length; i++)
            {
                arr[i] = strArray[i].ToInt();
            }
            return arr;
        }
    }
}
