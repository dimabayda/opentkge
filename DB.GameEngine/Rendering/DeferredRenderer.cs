using DB.GameEngine.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;
using DB.GameEngine.Shading;
using DB.GameEngine.Shading.ShaderSources;
using DB.GameEngine.ComponentModel.Shapes;
using DB.GameEngine.Objects;
using System.Linq;

namespace DB.GameEngine.Rendering
{
    public class DeferredRenderer
    {
        private GBuffer gBuffer;
        private ShaderProgram shaderProgram;
        private Mesh quadMesh;

        public DeferredRenderer(int windowWidth, int windowHeight)
        {
            gBuffer = new GBuffer(windowWidth, windowHeight, 4);
            shaderProgram = new ShaderProgram(VertexShader.Deferred, FragmentShader.Deferred);
            quadMesh = new Square();
        }

        public void Render(GameObject gameObject, float time)
        {
            //IEnumerable<GameObject> shadedObjects = gameObject.AllChildren.Where(child => child.IsShaded);
            //IEnumerable<GameObject> nonShadedObjects = gameObject.AllChildren.Where(child => !child.IsShaded);
            //foreach (GameObject nonShaded in nonShadedObjects)
            //{
            //    nonShaded.Draw();
            //}
            gBuffer.Fill(gameObject.AllChildren);
            GL.UseProgram(shaderProgram.Program);
            shaderProgram["tColor"].Load(0);
            shaderProgram["tNormal"].Load(1);
            shaderProgram["tPosition"].Load(2);
            shaderProgram["tTexture"].Load(3);
            //shaderProgram["depthm"].Load(4);

            //shaderProgram["time"].Load(time);

            GL.BindVertexArray(quadMesh.Vao);
            GL.EnableVertexAttribArray(0);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, gBuffer.Buffers[0]);
            GL.ActiveTexture(TextureUnit.Texture1);
            GL.BindTexture(TextureTarget.Texture2D, gBuffer.Buffers[1]);
            GL.ActiveTexture(TextureUnit.Texture2);
            GL.BindTexture(TextureTarget.Texture2D, gBuffer.Buffers[2]);
            GL.ActiveTexture(TextureUnit.Texture3);
            GL.BindTexture(TextureTarget.Texture2D, gBuffer.Buffers[3]);
            //GL.ActiveTexture(TextureUnit.Texture4);
            //GL.BindTexture(TextureTarget.Texture2D, depthTexture);
            GL.DrawArrays(PrimitiveType.Triangles, 0, quadMesh.VerticesCount);

            GL.DisableVertexAttribArray(0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);
        }
    }
}
