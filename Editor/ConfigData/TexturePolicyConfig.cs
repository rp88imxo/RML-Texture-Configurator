using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace RML.Editor
{
    [Serializable]
    public class TexturePolicyOptions
    {
        public int maxSize = 1024;
        public TextureImporterCompression textureImporterCompression = TextureImporterCompression.CompressedLQ;
        public bool useStreaming = false;
        public bool useCrunchCompression = false;
        public bool isReadable = false;
        public bool mipMapEnabled = true;
    }

    [Serializable]
    public class TexturePolicyPathConfigEntry
    {
        public string path;
        public List<string> excludePaths;

        public List<string> excludeTexturesPrefixes = new List<string>();

        public TexturePolicyOptions texturePolicyOptions;
    }

    [CreateAssetMenu(fileName = "Texture Policy Config", menuName = "RML/Data/Texture Policy/Texture Policy Config")]
    public class TexturePolicyConfig : ScriptableObject
    {
        [SerializeField] private List<TexturePolicyPathConfigEntry> policyPathConfigEntries;

        public IReadOnlyList<TexturePolicyPathConfigEntry> PolicyPathConfigEntries => policyPathConfigEntries;
    }
}