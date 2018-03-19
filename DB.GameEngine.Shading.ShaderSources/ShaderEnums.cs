using System;
using System.Collections.Generic;
using System.Text;

namespace DB.GameEngine.Shading.ShaderSources
{
    public abstract class Shader
    {
        private string path;

        protected Shader(string path)
        {
            this.path = path;
        }

        public static explicit operator string(Shader shader)
        {
            return shader.path;
        }
    }

    public sealed class VertexShader : Shader
    {
        public static readonly VertexShader Deferred = new VertexShader("deferredVertexShader.c");
        public static readonly VertexShader FillBufferInstanced = new VertexShader("fillBufferInstancedVertexShader.c");
        public static readonly VertexShader FillBuffer = new VertexShader("fillBufferVertexShader.c");

        private VertexShader(string path) : base(path)
        {
        }
    }

    public sealed class FragmentShader: Shader
    {
        public static readonly FragmentShader CubeMapTexture = new FragmentShader("cubeMapTextureFragmentShader.c");
        public static readonly FragmentShader Deferred = new FragmentShader("deferredFragmentShader.c");
        public static readonly FragmentShader FillBuffer = new FragmentShader("fillBufferFragmentShader.c");

        private FragmentShader(string path) : base(path)
        {
        }
    }
}
