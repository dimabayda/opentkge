using DB.GameEngine.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB.GameEngine.Rendering
{
    public interface IRenderer
    {
        void Render(GameObject gameObject);
    }
}
