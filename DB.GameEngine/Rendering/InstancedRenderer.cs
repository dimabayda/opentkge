using System;
using System.Collections.Generic;
using System.Text;
using DB.GameEngine.ComponentModel;
using DB.GameEngine.Objects;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace DB.GameEngine.Rendering
{
    public class InstancedRenderer : BaseRenderer
    {
        private int modelsLocation;
        private Matrix4[] models;

        public InstancedRenderer(InstancedObject instancedObject)
        {
            GL.BindVertexArray(instancedObject.Mesh.Vao);
            InitializeInstance(3, out modelsLocation);
            GL.BindVertexArray(0);
            models = instancedObject.Models;
        }

        protected override void LoadData(GameObject gameObject)
        {
            base.LoadData(gameObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, modelsLocation);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * 16 * models.Length, models, BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        protected override void Draw(Mesh mesh)
        {
            MakeDrawing(4, () =>
            {
                if (mesh.IsIndicesRender)
                {
                    GL.DrawElementsInstanced(PrimitiveType.Triangles, mesh.IndicesCount, DrawElementsType.UnsignedInt, IntPtr.Zero, models.Length);
                }
                else
                {
                    GL.DrawArraysInstanced(PrimitiveType.Triangles, 0, mesh.VerticesCount, models.Length);
                }
            });
        }

        private void InitializeInstance(int location, out int buffer)
        {
            buffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
            for (int i = 0; i < 4; i++)
            {
                GL.EnableVertexAttribArray(location + i);
                GL.VertexAttribPointer(location + i, 4, VertexAttribPointerType.Float, false, 16 * sizeof(float), i * 4 * sizeof(float));
                GL.VertexAttribDivisor(location + i, 1);
            }
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
}
