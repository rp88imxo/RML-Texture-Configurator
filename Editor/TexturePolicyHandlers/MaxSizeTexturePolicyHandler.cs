using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RML.Editor
{
    public class MaxSizeTexturePolicyHandler : ITexturePolicyHandler
    {
        public bool HandleTexture(TextureImporter textureImporter, TexturePolicyPathConfigEntry configEntry)
        {
            var settings = textureImporter.GetPlatformTextureSettings("Switch");

            if (settings.maxTextureSize != configEntry.texturePolicyOptions.maxSize)
            {
                settings.maxTextureSize = configEntry.texturePolicyOptions.maxSize;

                settings.overridden = true;
                textureImporter.SetPlatformTextureSettings(settings);

                return true;
            }

            return false;
        }
    }
}