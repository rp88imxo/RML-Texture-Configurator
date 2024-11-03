using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RML.Editor
{
    public class MipMapTexturePolicyHandler : ITexturePolicyHandler
    {
        public bool HandleTexture(TextureImporter textureImporter, TexturePolicyPathConfigEntry configEntry)
        {
            if (textureImporter.mipmapEnabled != configEntry.texturePolicyOptions.mipMapEnabled)
            {
                textureImporter.mipmapEnabled = configEntry.texturePolicyOptions.mipMapEnabled;
                return true;
            }

            return false;
        }
    }
}