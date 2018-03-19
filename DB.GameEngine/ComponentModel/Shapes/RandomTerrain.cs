using DB.GameEngine.Utils;
using DB.GameEngine.Utils.Arrays;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DB.GameEngine.ComponentModel.Shapes
{
    public static class RandomTerrainGenerator
    {
        private static Random random = new Random();

        public static Mesh GenerateTerrain(int dimension, float height, float step, float textureResolution)
        {
            TypedPackage<float> verticesPackage = new TypedPackage<float>(3);
            TypedPackage<float> textureCoordinatesPackage = new TypedPackage<float>(2);
            TypedPackage<int> indicesPackage = new TypedPackage<int>(3);
            TypedPackage<float> normalsPackage = new TypedPackage<float>(3);
            int size = (int)Math.Pow(2, dimension) + 1;
            GameArray<float> heightGrid = GenerateHeightGrid(size, height);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    verticesPackage.AddVector(i * step, heightGrid[i, j], j * step);
                    textureCoordinatesPackage.AddVector(i / textureResolution, j / textureResolution);

                    Vector3 left = new Vector3(-1, 0, heightGrid[i - 1, j]);
                    Vector3 right = new Vector3(1, 0, heightGrid[i + 1, j]);
                    Vector3 up = new Vector3(0, -1, heightGrid[i, j - 1]);
                    Vector3 down = new Vector3(0, 1, heightGrid[i, j + 1]);
                    Vector3 tangent = right - left;
                    Vector3 bitangent = down - up;
                    Vector3 normal = Vector3.Cross(tangent, bitangent).Normalized();
                    normalsPackage.AddVector(normal.X, normal.Y, normal.Z);

                    if (i < size - 1 & j < size - 1)
                    {
                        indicesPackage.AddVector(i * size + j, i * size + j + 1, (i + 1) * size + j);
                        indicesPackage.AddVector((i + 1) * size + j + 1, i * size + j + 1, (i + 1) * size + j);
                    }
                }
            }
            return new Mesh(verticesPackage.Pack(), indicesPackage.Pack(), textureCoordinatesPackage.Pack(), normalsPackage.Pack());
        }

        public static PartialArray<float> GenerateHeightGrid(int size, float height)
        {
            PartialArray<float> array = new PartialArray<float>(new float[size, size])
            {
                LeftBottom = random.NextFloat(height),
                LeftTop = random.NextFloat(height),
                RightBottom = random.NextFloat(height),
                RightTop = random.NextFloat(height)
            };

            DiamondSquare(array, height);
            GenerateRecursive(array, height);
            return array;
        }

        private static void GenerateRecursive(GameArray<float> array, float height)
        {
            foreach (GameArray<float> arr in array.QuadArrays)
            {
                DiamondSquare(arr, height);
            }
            foreach (GameArray<float> arr in array.QuadArrays)
            {
                GenerateRecursive(arr, height);
            }
        }

        private static void DiamondSquare(GameArray<float> array, float height)
        {
            //array.Middle = (array.LeftBottom + array.LeftTop + array.RightBottom + array.RightTop) / 4 + random.NextFloat(height);
            //array.LeftMiddle = (array.LeftTop + array.Middle + array.LeftBottom) / 4 + random.NextFloat(height) / 2;
            //array.TopMiddle = (array.LeftTop + array.Middle + array.RightTop) / 4 + random.NextFloat(height) / 2;
            //array.BottomMiddle = (array.LeftBottom + array.Middle + array.RightBottom) / 4 + random.NextFloat(height) / 2;
            //array.RightMiddle = (array.RightTop + array.Middle + array.RightBottom) / 4 + random.NextFloat(height) / 2;

            array.Middle = (array.LeftBottom + array.LeftTop + array.RightBottom + array.RightTop) / 4 + random.NextFloat(height);
            array.LeftMiddle = (array.LeftTop + array.Middle + array.LeftBottom + array.Parent.LeftMiddle) / 4 + random.NextFloat(height) / 2;
            array.TopMiddle = (array.LeftTop + array.Middle + array.RightTop + array.Parent.TopMiddle) / 4 + random.NextFloat(height) / 2;
            array.BottomMiddle = (array.LeftBottom + array.Middle + array.RightBottom + array.Parent.BottomMiddle) / 4 + random.NextFloat(height) / 2;
            array.RightMiddle = (array.RightTop + array.Middle + array.RightBottom + array.Parent.RightMiddle) / 4 + random.NextFloat(height) / 2;
        }
    }
}
