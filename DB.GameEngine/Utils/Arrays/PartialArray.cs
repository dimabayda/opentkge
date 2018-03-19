using System;
using System.Collections.Generic;
using System.Text;

namespace DB.GameEngine.Utils.Arrays
{
    public class PartialArray<T> : GameArray<T>
    {
        private T[,] array;

        public override int Length { get => array.GetLength(0); }
        public override GameArray<T> Parent { get; protected set; }

        public override T this[int i, int j]
        {
            get
            {
                if (i < 0 || j < 0 || i >= Length || j >= Length)
                {
                    return default;
                }
                return array[i, j];
            }
            set
            {
                if (i < 0 || j < 0 || i >= Length || j >= Length)
                {
                    array[i, j] = default;
                }
                array[i, j] = value;
            }
        }

        public PartialArray(T[,] array)
        {
            this.array = array;
            Parent = this;
        }

        public override GameArray<T> GetSubArray(int row, int column, int size)
        {
            return new SubArray(this, row, column, size);
        }

        public class SubArray : GameArray<T>
        {
            private int startRow, startCol, size;

            public override int Length { get => size; }
            public override GameArray<T> Parent { get; protected set; }

            public SubArray(GameArray<T> parent, int startRow, int startCol, int size)
            {
                Parent = parent;
                this.startRow = startRow;
                this.startCol = startCol;
                this.size = size;
                int length = parent.Length;
            }

            public override T this[int i, int j]
            {
                get
                {
                    return Parent[startRow + i, startCol + j];
                }
                set
                {
                    Parent[startRow + i, startCol + j] = value;
                }
            }

            public override GameArray<T> GetSubArray(int row, int column, int size)
            {
                return new SubArray(this, row, column, size);
            }
        }
    }
}
