using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utility{
    public static class ResourceHandler 
    {
        public static bool testFilePath(string filePath)
        {
            /*
            string mainPath = Application.dataPath;
            filePath = mainPath + filePath;
            */
            Debug.Log(filePath);
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

        public static Sprite setImage(Image image, string filePath)
        {
            Debug.Log(filePath);
         
            Sprite sprite = Resources.Load<Sprite>(filePath);
            
            if (sprite == null)
            {
                Debug.Log("null sprite");
            }
            else
            {
                int imageScale = 60;
                float imageWidth = (float)sprite.bounds.size.x * imageScale;
                float imageHeight = (float)sprite.bounds.size.y * imageScale;
                image.sprite = sprite;
                image.rectTransform.sizeDelta = new Vector2(imageWidth, imageHeight);
                return image.sprite;
            }
            return sprite;
            

        }
    }

}

