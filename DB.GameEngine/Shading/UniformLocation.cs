using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB.GameEngine.Shading
{
    public struct UniformLocation
    {
        public int Location { get; private set; }

        public UniformLocation(int location)
        {
            Location = location;
        }

        public void Load(Matrix4 matrix4)
        {
            GL.UniformMatrix4(Location, false, ref matrix4);
        }

        public void Load(int num)
        {
            GL.Uniform1(Location, num);
        }

        public void Load(float num)
        {
            GL.Uniform1(Location, num);
        }
    }
}
