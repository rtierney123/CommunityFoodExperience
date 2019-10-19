using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utility{
    public static class ResourceHandler 
    {
        public static bool testFilePath(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                Debug.Log("file exists");
                return true;
            }
            else
            {
                Debug.Log("file does not exist");
                return false;
            }
        }

        public static void setImage(Image image, string filePath)
        {
            bool pathExists = ResourceHandler.testFilePath(filePath);
            if (pathExists)
            {
                Debug.Log("set image");
                image.sprite = Resources.Load<Sprite>(filePath);
            }

        }
    }

}

