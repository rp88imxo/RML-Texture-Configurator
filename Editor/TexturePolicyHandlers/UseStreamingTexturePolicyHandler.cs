using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RML.Editor
{
    public class UseStreamingTexturePolicyHandler : ITexturePolicyHandler
    {
        public bool HandleTexture(TextureImporter textureImporter, TexturePolicyPathConfigEntry configEntry)
        {
            if (textureImporter.textureType is not (TextureImporterType.Default or TextureImporterType.NormalMap))
            {
                return false;
            }

            if (textureImporter.streamingMipmaps != configEntry.texturePolicyOptions.useStreaming)
            {
                textureImporter.streamingMipmaps = configEntry.texturePolicyOptions.useStreaming;
                return true;
            }

            return false;
        }
    }
}