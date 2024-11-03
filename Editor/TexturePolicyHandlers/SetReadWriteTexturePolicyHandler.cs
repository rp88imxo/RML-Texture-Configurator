using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RML.Editor
{
    public class SetReadWriteTexturePolicyHandler : ITexturePolicyHandler
    {
        public bool HandleTexture(TextureImporter textureImporter, TexturePolicyPathConfigEntry configEntry)
        {
            if (textureImporter.isReadable != configEntry.texturePolicyOptions.isReadable)
            {
                textureImporter.isReadable = configEntry.texturePolicyOptions.isReadable;
                return true;
            }

            return false;
        }
    }
}