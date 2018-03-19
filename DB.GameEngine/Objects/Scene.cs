using DB.GameEngine.Cameras;
using DB.GameEngine.ComponentModel;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DB.GameEngine.Objects
{
    [DataContract]
    public class Scene : GameObject
    {
        public Scene(int screenWidth, int screenHeight)
        {
            Camera = new PerspectiveCamera(screenWidth, screenHeight);
        }
    }
}
