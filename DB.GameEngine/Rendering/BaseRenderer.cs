using DB.GameEngine.ComponentModel;
using DB.GameEngine.Materials;
using DB.GameEngine.Objects;
using DB.GameEngine.Shading;
using DB.GameEngine.Utils;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DB.GameEngine.Rendering
{
    public class BaseRenderer : IRenderer
    {
        public void Render(GameObject gameObject)
        {
            Material material = gameObject.Material;
            Mesh mesh = gameObject.Mesh;
            GL.UseProgram(material.ShaderProgram.Program);        
            LoadData(gameObject);
            BindTextures(material);
            GL.BindVertexArray(mesh.Vao);
            Draw(mesh);
            GL.BindVertexArray(0);
            GL.UseProgram(0);
        }

        protected virtual void LoadData(GameObject gameObject)
        {
            if (gameObject.Transformation != null)
            {
                gameObject.Material.ShaderProgram["modelMatrix"].Load(gameObject.Transformation.ModelMatrix);
            }
            if (gameObject.Camera != null)
            {
                gameObject.Material.ShaderProgram["viewMatrix"].Load(gameObject.Camera.ViewMatrix);
                gameObject.Material.ShaderProgram["projectionMatrix"].Load(gameObject.Camera.ProjectionMatrix);
            }
        }

        protected virtual void BindTextures(Material material)
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(material.TextureTarget, material.Texture.Id);
        }

        protected virtual void Draw(Mesh mesh)
        {
            MakeDrawing(3, () =>
            {
                if (mesh.IsIndicesRender)
                {
                    GL.DrawElements(PrimitiveType.Triangles, mesh.IndicesCount, DrawElementsType.UnsignedInt, 0);
                }
                else
                {
                    GL.DrawArrays(PrimitiveType.Triangles, 0, mesh.VerticesCount);
                }
            });
        }

        protected void MakeDrawing(int attributesCount, Action drawMethod)
        {
            for (int i = 0; i < attributesCount; i++)
            {
                GL.EnableVertexAttribArray(i);
            }
            drawMethod.Invoke();
            for (int i = 0; i < attributesCount; i++)
            {
                GL.DisableVertexAttribArray(i);
            }
        }
    }
}
