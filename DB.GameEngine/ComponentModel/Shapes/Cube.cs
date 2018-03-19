using DB.GameEngine.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB.GameEngine.ComponentModel.Shapes
{
    public class Cube: Mesh
    {
        public Cube(float size) : base(GetCubeVertices(size))
        {
        }

        public static float[] GetCubeVertices(float size)
        {
            return new float[]
            {
                -size,  size, -size,
                -size, -size, -size,
                 size, -size, -size,
                 size, -size, -size,
                 size,  size, -size,
                -size,  size, -size,

                -size, -size,  size,
                -size, -size, -size,
                -size,  size, -size,
                -size,  size, -size,
                -size,  size,  size,
                -size, -size,  size,

                 size, -size, -size,
                 size, -size,  size,
                 size,  size,  size,
                 size,  size,  size,
                 size,  size, -size,
                 size, -size, -size,

                -size, -size,  size,
                -size,  size,  size,
                 size,  size,  size,
                 size,  size,  size,
                 size, -size,  size,
                -size, -size,  size,

                -size,  size, -size,
                 size,  size, -size,
                 size,  size,  size,
                 size,  size,  size,
                -size,  size,  size,
                -size,  size, -size,

                -size, -size, -size,
                -size, -size,  size,
                 size, -size, -size,
                 size, -size, -size,
                -size, -size,  size,
                 size, -size,  size
            };
        }
    }
}
