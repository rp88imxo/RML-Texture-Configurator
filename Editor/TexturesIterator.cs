using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RML.Editor.Utils
{
    public class TexturesIterator
    {
        public IEnumerable<TextureImporter> IterateTexturesAtPath(string texturePath)
        {
            foreach (var guid in AssetDatabase.FindAssets("t:texture", new[] { texturePath }))
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
                if (textureImporter != null)
                {
                    yield return textureImporter;
                }
            }
        }
    }

}