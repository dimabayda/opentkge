using OpenTK;
using DB.GameEngine.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace DB.GameEngine.Cameras
{
    [DataContract]
    public class PerspectiveCamera : Camera
    {
        private float fieldOfViewAngle;

        [DataMember]
        public float FieldOfViewAngle
        {
            get => fieldOfViewAngle;

            set
            {
                fieldOfViewAngle = value;
                ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(fieldOfViewAngle), Aspect, 0.1f, 1000f);
            }
        }

        [DataMember]
        public float Aspect { get; private set; }

        public PerspectiveCamera()
        {
        }

        public PerspectiveCamera(float width, float height)
        {
            Aspect = width / height;
            FieldOfViewAngle = 60f;
            Position = new Vector3(0, 0, 50f);
        }
    }
}
