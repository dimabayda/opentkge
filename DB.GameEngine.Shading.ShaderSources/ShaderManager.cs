using System.IO;
using System.Reflection;

namespace DB.GameEngine.Shading.ShaderSources
{
    public static class ShaderManager
    {
        private static Assembly assembly = Assembly.GetExecutingAssembly();

        public static string GetShaderSourceCode(Shader shader)
        {
            string resourcePath = $"{assembly.GetName().Name}.Shaders.{(string)shader}";
            return new StreamReader(assembly.GetManifestResourceStream(resourcePath)).ReadToEnd();
        }
    }
}
