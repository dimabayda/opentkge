using DB.GameEngine.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;
using DB.GameEngine.Shading;
using DB.GameEngine.Shading.ShaderSources;

namespace DB.GameEngine.Materials
{
    public class Material
    {
        private ShaderProgram shaderProgram;

        public Texture Texture { get; private set; }
        public TextureTarget TextureTarget { get; private set; }
        public ShaderProgram ShaderProgram
        {
            get
            {
                if (shaderProgram == null)
                {
                    shaderProgram = new ShaderProgram(VertexShader, FragmentShader);
                }
                return shaderProgram;
            }
        }
        public VertexShader VertexShader { get; set; }
        public FragmentShader FragmentShader { get; set; }

        public Material(string[] images, string folder)
        {
            Texture = Texture.CubeMapTextureFromImages(ResourceManager.GetImages(folder, images));
            TextureTarget = TextureTarget.TextureCubeMap;
            VertexShader = VertexShader.FillBuffer;
            FragmentShader = FragmentShader.CubeMapTexture;
        }

        public Material(string image)
        {
            Texture = Texture.Texture2DFromImage(ResourceManager.GetImage(image));
            TextureTarget = TextureTarget.Texture2D;
            VertexShader = VertexShader.FillBuffer;
            FragmentShader = FragmentShader.FillBuffer;
        }
    }
}
