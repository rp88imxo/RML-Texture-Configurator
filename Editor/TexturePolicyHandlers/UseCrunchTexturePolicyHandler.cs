using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RML.Editor
{
    public class UseCrunchTexturePolicyHandler : ITexturePolicyHandler
    {
        private readonly int _crunchCompressionQuality;

        public UseCrunchTexturePolicyHandler(int crunchCompressionQuality = 75)
        {
            _crunchCompressionQuality = crunchCompressionQuality;
        }

        public bool HandleTexture(TextureImporter textureImporter, TexturePolicyPathConfigEntry configEntry)
        {
            var settings = textureImporter.GetPlatformTextureSettings("Switch");

            if (settings.crunchedCompression != configEntry.texturePolicyOptions.useCrunchCompression)
            {
                settings.crunchedCompression = configEntry.texturePolicyOptions.useCrunchCompression;
                settings.compressionQuality = _crunchCompressionQuality;

                settings.overridden = true;
                textureImporter.SetPlatformTextureSettings(settings);

                return true;
            }

            return false;
        }
    }
}