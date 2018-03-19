using System;
using System.Collections.Generic;
using System.Text;

namespace DB.GameEngine.Utils.Arrays
{
    public abstract class GameArray<T>
    {
        public abstract int Length { get; }
        public abstract T this[int i, int j] { get; set; }
        public abstract GameArray<T> GetSubArray(int i, int j, int size);
        public abstract GameArray<T> Parent { get; protected set; }

        public T LeftBottom
        {
            get => this[0, 0];
            set => this[0, 0] = value;
        }

        public T LeftMiddle
        {
            get => this[Length / 2, 0];
            set => this[Length / 2, 0] = value;
        }

        public T LeftTop
        {
            get => this[Length - 1, 0];
            set => this[Length - 1, 0] = value;
        }

        public T BottomMiddle
        {
            get => this[0, Length / 2];
            set => this[0, Length / 2] = value;
        }

        public T Middle
        {
            get => this[Length / 2, Length / 2];
            set => this[Length / 2, Length / 2] = value;
        }

        public T TopMiddle
        {
            get => this[Length - 1, Length / 2];
            set => this[Length - 1, Length / 2] = value;
        }

        public T RightBottom
        {
            get => this[0, Length - 1];
            set => this[0, Length - 1] = value;
        }

        public T RightMiddle
        {
            get => this[Length / 2, Length - 1];
            set => this[Length / 2, Length - 1] = value;
        }

        public T RightTop
        {
            get => this[Length - 1, Length - 1];
            set => this[Length - 1, Length - 1] = value;
        }

        public IEnumerable<GameArray<T>> QuadArrays
        {
            get
            {
                if (Length > 1)
                {
                    yield return this.Quad1();
                    yield return this.Quad2();
                    yield return this.Quad3();
                    yield return this.Quad4();
                }
            }
        }
    }
}
