using DB.GameEngine.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace DB.GameEngine.ComponentModel
{
    [DataContract]
    public class Mesh
    {
        public int Vao { get; private set; }
        [DataMember]
        public float[] Vertices { get; private set; }
        [DataMember]
        public float[] Normals { get; private set; }
        [DataMember]
        public int[] Indices { get; private set; }
        [DataMember]
        public float[] TextureCoordinates { get; private set; }
        public int VerticesCount { get; private set; }
        public int IndicesCount { get; private set; }
        public bool IsIndicesRender { get; private set; } = false;

        public Mesh(float[] vertices, int[] indices = null, float[] textureCoordinates = null, float[] normals = null)
        {
            Vertices = vertices;
            VerticesCount = Vertices.Length / 3;
            if (indices != null)
            {
                Indices = indices;
                IndicesCount = Indices.Length;
                IsIndicesRender = true;
            }
            if (textureCoordinates != null)
            {
                TextureCoordinates = textureCoordinates;
            }
            if (normals != null)
            {
                Normals = normals;
            }
            InitializeVao();
        }

        public static Mesh CreateFromObj(string fileName)
        {
            string objFileText = ResourceManager.GetObjFileText(fileName);
            MeshLoader.Load(objFileText, out var vertices, out var textureCoordinates, out var indices, out var normals);
            return new Mesh(vertices, indices, textureCoordinates, normals);
            //return new Mesh(vertices, indices);
        }

        private void InitializeVao()
        {
            Vao = GL.GenVertexArray();
            GL.BindVertexArray(Vao);
            InitializeBuffer(0, 3, Vertices);
            InitializeBuffer(1, 3, Normals);
            InitializeBuffer(2, 2, TextureCoordinates);
            InitializeIndicesBuffer(Indices);
            GL.BindVertexArray(0);
        }

        private void InitializeBuffer(int index, int size, float[] data)
        {
            if (data != null)
            {
                int vbo = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
                GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * data.Length, data, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(index, size, VertexAttribPointerType.Float, false, 0, 0);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            }
        }

        private void InitializeIndicesBuffer(int[] indices)
        {
            if (indices != null)
            {
                int vbo = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, vbo);
                GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(int), indices, BufferUsageHint.StaticDraw);
            }
        }
    }
}
