using DB.GameEngine.Cameras;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB.GameEngine.Utils
{
    public static class MatrixHelper
    {
        public static Matrix4 CreateMVPMatrix(Matrix4 modelMatrix, Matrix4 viewMatrix, Matrix4 projectionMatrix)
        {
            return modelMatrix * viewMatrix * projectionMatrix;
        }

        public static Matrix4 CreateVPMatrix(Camera camera)
        {
            return camera.ViewMatrix * camera.ProjectionMatrix;
        }

        public static Matrix4 CreateModelMatrix(Vector3 translation,
            Vector3 rotation, float scale)
        {
            return Matrix4.CreateScale(scale)
                * CreateRotationMatrix(rotation)
                * Matrix4.CreateTranslation(translation);
        }

        public static Matrix4 CreateRotationMatrix(Vector3 rotation)
        {
            return Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Z))
                * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotation.Y))
                * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotation.X));
        }
    }
}
