using System;
using System.Collections.Generic;
using System.Text;

namespace DB.GameEngine.Utils
{
    public class TypedVector<T>
    {
        private T[] items;

        public TypedVector(params T[] items)
        {
            this.items = items;
        }

        public T[] Pack()
        {
            return items;
        }
    }
}
