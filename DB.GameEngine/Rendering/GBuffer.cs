using DB.GameEngine.ComponentModel;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DB.GameEngine.Rendering
{
    public class GBuffer
    {
        private int fbo;
        private int[] buffers;
        private int depthBuffer;

        public int[] Buffers { get => buffers; }
        public int DepthBuffer { get => depthBuffer; }

        public GBuffer(int windowWidth, int windowHeight, int attachmentsCount)
        {
            InitializeFbo(windowWidth, windowHeight, attachmentsCount, out fbo, out buffers, out depthBuffer);
        }

        public void Fill(IEnumerable<GameObject> gameObjects)
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, fbo);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            foreach(GameObject gameObject in gameObjects)
            {
                gameObject.Draw();
            }
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        private void InitializeFbo(int windowWidth, int windowHeight, int attachmentsCount, out int fbo, out int[] buffers, out int depthBuffer)
        {
            if (attachmentsCount > 15)
            {
                throw new Exception("Count is too big");
            }

            buffers = new int[attachmentsCount];
            DrawBuffersEnum[] drawBuffers = new DrawBuffersEnum[attachmentsCount];

            fbo = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, fbo);
            depthBuffer = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, depthBuffer);
            GL.TexImage2D(
                TextureTarget.Texture2D,
                0,
                PixelInternalFormat.DepthComponent32,
                windowWidth,
                windowHeight,
                0,
                PixelFormat.DepthComponent,
                PixelType.Float,
                IntPtr.Zero
            );
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.FramebufferTexture(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, depthBuffer, 0);
            GL.GenTextures(attachmentsCount, buffers);

            for (int i = 0; i < attachmentsCount; i++)
            {
                GL.BindTexture(TextureTarget.Texture2D, buffers[i]);
                GL.TexImage2D(
                    TextureTarget.Texture2D,
                    0,
                    PixelInternalFormat.Rgba,
                    windowWidth,
                    windowHeight,
                    0,
                    PixelFormat.Rgba,
                    PixelType.Float,
                    IntPtr.Zero
                );
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                GL.FramebufferTexture(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0 + i, buffers[i], 0);
                drawBuffers[i] = DrawBuffersEnum.ColorAttachment0 + i;
            }

            GL.DrawBuffers(drawBuffers.Length, drawBuffers);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }
    }
}
