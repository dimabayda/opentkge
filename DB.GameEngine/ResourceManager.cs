using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;

namespace DB.GameEngine
{
    public static class ResourceManager
    {
        private static Assembly assembly = Assembly.GetEntryAssembly();

        public static Image GetImage(string imageName)
        {     
            string resourcePath = $"{assembly.GetName().Name}.Images.{imageName}";
            return Image.FromStream(assembly.GetManifestResourceStream(resourcePath));
        }

        public static Image[] GetImages(string folder, string[] imageNames)
        {
            int length = imageNames.Length;
            Image[] images = new Image[length];
            for (int i = 0; i < length; i++)
            {
                string resourcePath = $"{assembly.GetName().Name}.Images.{folder}.{imageNames[i]}";
                images[i] = Image.FromStream(assembly.GetManifestResourceStream(resourcePath));
            }
            return images;
        }

        public static string GetObjFileText(string objFileName)
        {
            string resourcePath = $"{assembly.GetName().Name}.Models.{objFileName}";
            return new StreamReader(assembly.GetManifestResourceStream(resourcePath)).ReadToEnd();
        }
    }
}
