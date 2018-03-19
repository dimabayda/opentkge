using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;
using DB.GameEngine.Utils;
using System.Runtime.Serialization;

namespace DB.GameEngine.ComponentModel
{
    [DataContract]
    public class Transformation
    {   
        private Vector3 translation;     
        private Vector3 rotation;
        private float scale;
        private Matrix4 modelMatrix;

        public bool IsChanged { get; set; }

        [DataMember]
        public Vector3 Translation
        {
            get => translation; set
            {
                translation = value;
                IsChanged = true;
            }
        }

        [DataMember]
        public Vector3 Rotation
        {
            get => rotation; set
            {
                rotation = value;
                IsChanged = true;
            }
        }

        [DataMember]
        public float Scale
        {
            get => scale; set
            {
                scale = value;
                IsChanged = true;
            }
        }

        public Matrix4 ModelMatrix
        {
            get
            {
                if (IsChanged)
                {
                    modelMatrix = MatrixHelper.CreateModelMatrix(Translation, Rotation, Scale);
                    IsChanged = false;
                }
                return modelMatrix;
            }
        }

        public Transformation() : this(default) { }

        public Transformation(Vector3 translation = default, Vector3 rotation = default, float scale = 1)
        {
            Translation = translation;
            Rotation = rotation;
            Scale = scale;
            IsChanged = true;
        }

        public void SetRotation(float x = 0, float y = 0, float z = 0)
        {
            Rotation = new Vector3(x, y, z);
        }

        public void SetTranslation(float x = 0, float y = 0, float z = 0)
        {
            Translation = new Vector3(x, y, z);
        }
    }
}
