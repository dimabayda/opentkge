using OpenTK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

namespace DB.GameEngine.Utils
{
    public static class MeshLoader
    {
        public static void Load(string text, out float[] verticesArray, out float[] textureArray, out int[] indicesArray, out float[] normalsArray)
        {
            string[] lines = text.Split('\n');
            TypedPackage<float> verticesPackage = new TypedPackage<float>(3);
            TypedPackage<float> textureCoordinatesPackage = new TypedPackage<float>(2);
            TypedPackage<int> indicesPackage = new TypedPackage<int>(1);
            TypedPackage<float> normalsPackage = new TypedPackage<float>(3);
            TypedPackage<int> texturesMask = new TypedPackage<int>(1);
            TypedPackage<int> normalsMask = new TypedPackage<int>(1);

            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    int firstSpaceIndex = line.IndexOf(' ');
                    string command = line.Substring(0, firstSpaceIndex);
                    string data = line.Substring(firstSpaceIndex + 1);
                    switch (command)
                    {
                        case "v":
                            verticesPackage.AddVector(data.ToFloatArray(' '));
                            break;
                        case "vt":
                            float[] arr = data.ToFloatArray(' ');
                            textureCoordinatesPackage.AddVector(arr[0], 1 - arr[1]);
                            break;
                        case "vn":
                            normalsPackage.AddVector(data.ToFloatArray(' '));
                            break;
                        case "f":
                            string[] trianlges = data.Split(' ');
                            foreach (string triangle in trianlges)
                            {
                                int[] triangleData = triangle.ToIntArray('/');
                                indicesPackage.AddVector(triangleData[0] - 1);
                                texturesMask.AddVector(triangleData[1] - 1);
                                normalsMask.AddVector(triangleData[2] - 1);
                            }
                            break;
                    }
                }
            }

            int verticesCount = verticesPackage.Vectors.Count;
            indicesArray = indicesPackage.Pack();
            verticesArray = verticesPackage.Pack();
            textureArray = textureCoordinatesPackage.Pack(indicesArray, texturesMask.Pack(), verticesCount);
            normalsArray = normalsPackage.Pack(indicesArray, normalsMask.Pack(), verticesCount);
        }
    }
}
