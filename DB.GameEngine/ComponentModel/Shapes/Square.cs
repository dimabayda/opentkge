using System;
using System.Collections.Generic;
using System.Text;

namespace DB.GameEngine.ComponentModel.Shapes
{
    public class Square : Mesh
    {
        public Square() : base(GetSquareVertices())
        {
        }

        public static float[] GetSquareVertices()
        {
            return new float[]
            {
                -1.0f, -1.0f, 0.0f,
                1.0f, -1.0f, 0.0f,
                -1.0f,  1.0f, 0.0f,
                -1.0f,  1.0f, 0.0f,
                1.0f, -1.0f, 0.0f,
                1.0f,  1.0f, 0.0f,
            };
        }
    }
}
