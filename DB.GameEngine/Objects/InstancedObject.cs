using DB.GameEngine.ComponentModel;
using DB.GameEngine.Materials;
using DB.GameEngine.Rendering;
using DB.GameEngine.Shading.ShaderSources;
using DB.GameEngine.Utils;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB.GameEngine.Objects
{
    public class InstancedObject : GameObject
    {
        private Matrix4[] models;

        public Matrix4 this[int index]
        {
            get
            {
                return models[index];
            }
            set
            {
                models[index] = value;
            }
        }

        public Matrix4[] Models { get => models; }

        public int Count { get; private set; }

        public InstancedObject(string fileName, string textureName, int numInstances)
        {
            models = new Matrix4[numInstances];
            Count = numInstances;
            Material = new Material(textureName)
            {
                VertexShader = VertexShader.FillBufferInstanced
            };
           // MatrixCreator = () => Camera.ViewMatrix;
            Mesh = Mesh.CreateFromObj(fileName);
            Renderer = new InstancedRenderer(this);
        }
    }
}
