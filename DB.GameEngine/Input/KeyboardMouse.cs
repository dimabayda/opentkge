using DB.GameEngine.Cameras;
using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB.GameEngine.Input
{
    public class KeyboardMouse
    {
        private GameWindow gameWindow;
        private Camera camera;
        private Point center;
        private Point windowCenter;

        private const float moveSpeed = 10.0f;
        private const float mouseSpeed = 0.05f;

        public KeyboardMouse(GameWindow gameWindow)
        {
            this.gameWindow = gameWindow;
            gameWindow.UpdateFrame += OnUpdateFrame;
            windowCenter = new Point(gameWindow.Width / 2, gameWindow.Height / 2);
            center = gameWindow.PointToScreen(windowCenter);
        }

        private void OnUpdateFrame(object sender, FrameEventArgs e)
        {
            float delta = (float)e.Time;
            if (camera != null)
            {
                ProcessKeyboard(delta);
                ProcessMouse(delta);
            }
        }

        private void ProcessKeyboard(float delta)
        {
            float speed = moveSpeed;

            if (gameWindow.Keyboard[Key.Space])
            {
                speed *= 4;
            }

            if (gameWindow.Keyboard[Key.W])
            {
                camera.Position += camera.Direction * delta * speed;
            }
            else
            {
                if (gameWindow.Keyboard[Key.S])
                {
                    camera.Position -= camera.Direction * delta * speed;
                }
            }
            if (gameWindow.Keyboard[Key.D])
            {
                camera.Position += camera.Right * delta * speed;
            }
            else
            {
                if (gameWindow.Keyboard[Key.A])
                {
                    camera.Position -= camera.Right * delta * speed;
                }
            }
            if (gameWindow.Keyboard[Key.Escape])
            {
                gameWindow.Close();
            }
        }

        private void ProcessMouse(float delta)
        {
            int xPos = gameWindow.Mouse.X;
            int yPos = gameWindow.Mouse.Y;
            Mouse.SetPosition(center.X, center.Y);
            camera.HorizontalAngle += mouseSpeed * delta * (windowCenter.X - xPos);
            camera.VerticalAngle += mouseSpeed * delta * (windowCenter.Y - yPos);
        }

        public void AttachCamera(Camera camera)
        {
            this.camera = camera;
        }
    }
}
