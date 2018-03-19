using DB.GameEngine.Cameras;
using DB.GameEngine.ComponentModel;
using DB.GameEngine.ComponentModel.Shapes;
using DB.GameEngine.Materials;
using DB.GameEngine.Rendering;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DB.GameEngine.Objects
{
    [DataContract]
    public class SkyBox : GameObject
    {
        public SkyBox(string[] images)
        {
            if (images.Length != 6)
            {
                throw new Exception("Wrong images count");
            }
            Material = new Material(images, "SkyBox");
            Mesh = new Cube(500);
            Renderer = new SkyBoxRenderer();
        }
    }
}
