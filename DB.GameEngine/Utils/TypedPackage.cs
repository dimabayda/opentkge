using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB.GameEngine.Utils
{
    public class TypedPackage<T>
    {
        private int size;

        public List<TypedVector<T>> Vectors { get; private set; } = new List<TypedVector<T>>();

        public TypedPackage(int size)
        {
            this.size = size;
        }

        public void AddVector(params T[] items)
        {
            if (items.Length == size)
            {
                Vectors.Add(new TypedVector<T>(items));
            }
            else
            {
                throw new Exception($"Wrong items count, must be {size}!");
            }
        }

        public T[] Pack()
        {
            T[] arr = new T[Vectors.Count * size];
            for (int i = 0; i < Vectors.Count; i++)
            {
                Array.Copy(Vectors[i].Pack(), 0, arr, i * size, size);
            }
            return arr;
        }

        public T[] Pack(int[] mask)
        {
            T[] arr = new T[mask.Length * size];
            for (int i = 0; i < mask.Length; i++)
            {
                int index = mask[i];
                Array.Copy(Vectors[index].Pack(), 0, arr, i * size, size);
            }
            return arr;
        }

        public T[] Pack(int[] indices, int[] mask, int count)
        {
            T[] arr = new T[count * size];
            for (int i = 0; i < mask.Length; i++)
            {
                int vecIndex = mask[i];
                int indice = indices[i];
                Array.Copy(Vectors[vecIndex].Pack(), 0, arr, indice * size, size);
            }
            return arr;
        }
    }
}
