using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using OpenTK.Input;
using DB.GameEngine.Objects;
using DB.GameEngine.Utils;
using DB.GameEngine.Cameras;
using DB.GameEngine.Rendering;
using DB.GameEngine.Materials;
using DB.GameEngine.ComponentModel;
using DB.GameEngine.ComponentModel.Shapes;
using DB.GameEngine.Input;
using System.Diagnostics;
using DB.GameEngine.Utils.Arrays;

namespace DB.SimpleGame
{
    public class MyGameWindow : GameWindow
    {
        private Scene scene;
        SkyBox skyBox;
        private DeferredRenderer deferredRenderer;
        private KeyboardMouse keyboardMouse;
        private float globalTime = 0;

        private string[] skyBoxImages = new string[] { "1.png", "2.png", "3.png", "4.png", "5.png", "6.png" };

        public MyGameWindow() : base(800, 600, GraphicsMode.Default, "DB.SimpleGame")
        {
            CursorVisible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
           // Tests();
            GL.Enable(EnableCap.DepthTest);
            //GL.Enable(EnableCap.CullFace);
            //GL.CullFace(CullFaceMode.Back);

            deferredRenderer = new DeferredRenderer(Width, Height);
            skyBox = new SkyBox(skyBoxImages);

            scene = new Scene(Width, Height);
            keyboardMouse = new KeyboardMouse(this);
            keyboardMouse.AttachCamera(scene.Camera);

            GameObject gameObject = new GameObject
            {
                Mesh = RandomTerrainGenerator.GenerateTerrain(5, 20, 0.5f, 5),
                Material = new Material("grass.png"),
                Transformation = new Transformation(),
                Renderer = new BaseRenderer(),
                IsShaded = true
            };

            InstancedObject instancedObject = new InstancedObject("stall.obj", "stall.png", 1000);
            for (int i = 0; i < instancedObject.Count; i++)
            {
                instancedObject[i] = Matrix4.CreateTranslation(0, 20, i * 20);
            }

            //scene.AddChildren(gameObject);
            scene.AddChildren(instancedObject);
            scene.AddChildren(skyBox);
        }

        protected void Tests()
        {
            float[,] arr = new float[,]
            {
                {1,2,3,4,5 },
                {6,7,8,9,10 },
                {11,12,13,14,15 },
                {16,17,18,19,20 },
                {21,22,23,24,25 }
            };
            PartialArray<float> partialArray = new PartialArray<float>(arr);
            foreach(GameArray<float> fArr in partialArray.QuadArrays)
            {
                fArr.Print();
            }
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            Title = (1 / e.Time).ToString();
            globalTime += (float)e.Time;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(Color.White);
            deferredRenderer.Render(scene, globalTime);
            SwapBuffers();
        }
    }
}
