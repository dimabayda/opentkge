using DB.GameEngine.Utils;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DB.GameEngine.Cameras
{
    [DataContract]
    public abstract class Camera
    {
        private Vector3 position;
        private Vector3 direction;
        private Vector3 up;
        private Vector3 right;
        private float horizontalAngle = MathHelper.Pi;
        private float verticalAngle = 0;

        public Matrix4 ViewMatrix { get; protected set; }
        public Matrix4 ProjectionMatrix { get; protected set; }

        [DataMember]
        public Vector3 Position
        {
            get => position;

            set
            {
                position = value;
                CalculateViewMatrix();
            }
        }

        [DataMember]
        public float HorizontalAngle
        {
            get => horizontalAngle;

            set
            {
                horizontalAngle = value;
                CalculateViewMatrix();
            }
        }

        [DataMember]
        public float VerticalAngle
        {
            get => verticalAngle;

            set
            {
                if (value < MathHelper.PiOver2 && value > -MathHelper.PiOver2)
                {
                    verticalAngle = value;
                    CalculateViewMatrix();
                }
            }
        }

        public Vector3 Direction
        {
            get => direction;
        }

        public Vector3 Up
        {
            get => up;
        }

        public Vector3 Right
        {
            get => right;
        }

        private void CalculateViewMatrix()
        {
            direction = new Vector3(
                (float)Math.Cos(verticalAngle) * (float)Math.Sin(horizontalAngle),
                (float)Math.Sin(verticalAngle),
                (float)Math.Cos(verticalAngle) * (float)Math.Cos(horizontalAngle));
            right = new Vector3(
                (float)Math.Sin(horizontalAngle - MathHelper.PiOver2),
                0,
                (float)Math.Cos(horizontalAngle - MathHelper.PiOver2));
            up = Vector3.Cross(right, direction);
            ViewMatrix = Matrix4.LookAt(position, position + direction, up);
        }
    }
}
