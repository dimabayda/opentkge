using DB.GameEngine.Cameras;
using DB.GameEngine.Materials;
using DB.GameEngine.Rendering;
using DB.GameEngine.Utils;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DB.GameEngine.ComponentModel
{
    [DataContract]
    public class GameObject
    {
        [DataMember]
        private Camera camera;
        private GameObject parent;
        private List<GameObject> children = new List<GameObject>();

        public Transformation Transformation { get; set; }
        public Material Material { get; set; }
        public Mesh Mesh { get; set; }
        public IRenderer Renderer { get; set; }
        public IList<GameObject> Children { get => children.AsReadOnly(); }
        public bool IsShaded { get; set; } = false;

        public Camera Camera
        {
            get
            {
                if (camera == null && parent != null)
                {
                    return parent.Camera;
                }
                return camera;
            }
            set => camera = value;
        }

        public IEnumerable<GameObject> AllChildren
        {
            get
            {
                if (children.Count > 0)
                {
                    foreach (GameObject child in children)
                    {
                        IEnumerable<GameObject> childNodes = child.AllChildren;
                        foreach (GameObject childNode in childNodes)
                        {
                            yield return childNode;
                        }
                    }
                }
                yield return this;
            }
        }

        public void AddChildren(GameObject gameObject)
        {
            gameObject.parent = this;
            children.Add(gameObject);
        }

        public virtual void Draw()
        {
            if (Renderer != null)
            {
                Renderer.Render(this);
            }
           // children.ForEach(child => child.Draw());
        }
    }
}
