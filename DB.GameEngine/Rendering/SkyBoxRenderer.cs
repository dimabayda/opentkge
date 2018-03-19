using DB.GameEngine.ComponentModel;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB.GameEngine.Rendering
{
    public class SkyBoxRenderer : BaseRenderer
    {
        protected override void LoadData(GameObject gameObject)
        {
            gameObject.Material.ShaderProgram["modelMatrix"].Load(Matrix4.Identity);
            if (gameObject.Camera != null)
            {
                gameObject.Material.ShaderProgram["viewMatrix"].Load(gameObject.Camera.ViewMatrix.ClearTranslation());
                gameObject.Material.ShaderProgram["projectionMatrix"].Load(gameObject.Camera.ProjectionMatrix);
            }
        }
    }
}
