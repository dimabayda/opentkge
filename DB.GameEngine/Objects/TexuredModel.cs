using DB.GameEngine.Cameras;
using DB.GameEngine.ComponentModel;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DB.GameEngine.Objects
{
    [DataContract]
    public class TexturedModel : GameObject
    {
        public TexturedModel(string model, string texture)
        {
            //Components.Add(new ObjFileProvider("textureVertexShader.glsl", model));
            //Components.Add(new SimpleMaterial("textureFragmentShader.glsl", texture));
            //Components.Add(new MeshRenderer());
        }
    }
}
