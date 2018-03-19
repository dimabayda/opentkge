using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using DB.GameEngine.Shading.ShaderSources;

namespace DB.GameEngine.Shading
{
    public class ShaderProgram
    {
        public int Program { get; private set; }
        private Dictionary<string, UniformLocation> uniformLocations = new Dictionary<string, UniformLocation>();

        public UniformLocation this[string uniformName]
        {
            get
            {
                if (uniformLocations.ContainsKey(uniformName))
                {
                    return uniformLocations[uniformName];
                }
                int location = GetUniformLocation(uniformName);
                UniformLocation uniformLocation = new UniformLocation(location);
                uniformLocations.Add(uniformName, uniformLocation);
                return uniformLocation;
            }
        }

        public ShaderProgram(VertexShader vertexShader, FragmentShader fragmentShader)
        {
            string vertexShaderSourceCode = ShaderManager.GetShaderSourceCode(vertexShader);
            string fragmentShaderSourceCode = ShaderManager.GetShaderSourceCode(fragmentShader);
            int vertexShaderId = Compile(ShaderType.VertexShader, vertexShaderSourceCode);
            int fragmentShaderId = Compile(ShaderType.FragmentShader, fragmentShaderSourceCode);

            Program = GL.CreateProgram();
            GL.AttachShader(Program, vertexShaderId);
            GL.AttachShader(Program, fragmentShaderId);
            GL.LinkProgram(Program);

            GL.DeleteShader(vertexShaderId);
            GL.DeleteShader(fragmentShaderId);
        }

        protected int GetUniformLocation(string variableName)
        {
            int location = GL.GetUniformLocation(Program, variableName);
            if (location < 0)
            {
                throw new Exception("Unknown variable name!");
            }
            return location;
        }

        private int Compile(ShaderType shaderType, string shaderSourceCode)
        {
            int id = GL.CreateShader(shaderType);
            GL.ShaderSource(id, shaderSourceCode);
            GL.CompileShader(id);
            GL.GetShader(id, ShaderParameter.CompileStatus, out int result);
            if (result == 0)
            {
                GL.GetShaderInfoLog(id, out string info);
                throw new Exception(info);
            }
            return id;
        }
    }
}
