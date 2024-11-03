using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RML.Editor
{
    public interface ITexturePolicyHandler
    {
        public bool HandleTexture(TextureImporter textureImporter, TexturePolicyPathConfigEntry configEntry);
    }
}