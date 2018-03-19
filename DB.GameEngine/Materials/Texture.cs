using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;
using System.IO;
using System.Drawing;
using DB.GameEngine.Utils;

namespace DB.GameEngine.Materials
{
    public class Texture
    {
        public int Id { get; private set; }

        private Texture(int textureId)
        {
            Id = textureId;
        }

        public static Texture Texture2DFromImage(Image image)
        {
            int width = image.Width;
            int height = image.Height;
            float[] data = ImageHelper.ConvertToArray(image);

            GL.CreateTextures(TextureTarget.Texture2D, 1, out int textureId);
            GL.TextureStorage2D(
                textureId,
                1,
                SizedInternalFormat.Rgba32f,
                width,
                height);

            GL.BindTexture(TextureTarget.Texture2D, textureId);
            GL.TextureSubImage2D(textureId,
                0,
                0,
                0,
                width,
                height,
                PixelFormat.Rgba,
                PixelType.Float,
                data);

            return new Texture(textureId);
        }

        public static Texture CubeMapTextureFromImages(Image[] images)
        {
            int textureId = GL.GenTexture();
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.TextureCubeMap, textureId);

            for (int i = 0; i < images.Length; i++)
            {
                int width = images[i].Width;
                int height = images[i].Height;
                float[] data = ImageHelper.ConvertToArray(images[i]);

                GL.TexImage2D(
                    TextureTarget.TextureCubeMapPositiveX + i,
                    0,
                    PixelInternalFormat.Rgba,
                    width,
                    height,
                    0,
                    PixelFormat.Rgba,
                    PixelType.Float,
                    data);
            }

            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);

            return new Texture(textureId);
        }
    }
}
