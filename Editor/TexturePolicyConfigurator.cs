using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RML.Editor.Utils;
using UnityEditor;
using UnityEngine;

namespace RML.Editor
{
    public class TexturePolicyConfigurator
    {
        private readonly TexturesIterator _texturesIterator;
        private readonly List<ITexturePolicyHandler> _texturePolicyHandlers;

        public TexturePolicyConfigurator()
        {
            _texturesIterator = new TexturesIterator();
            _texturePolicyHandlers = new List<ITexturePolicyHandler>()
            {
                new FormatTexturePolicyHandler(new AstcFormatTextureStrategy()),
                new MaxSizeTexturePolicyHandler(),
                new UseCrunchTexturePolicyHandler(),
                new UseStreamingTexturePolicyHandler(),
                new MipMapTexturePolicyHandler()
            };
        }

        public void HandleTexturePolicy(TexturePolicyConfig texturePolicyConfig)
        {
            if (texturePolicyConfig == null)
            {
                Debug.LogError("Config is null!");
                return;
            }

            var entries = texturePolicyConfig.PolicyPathConfigEntries;

            try
            {
                AssetDatabase.StartAssetEditing();

                foreach (var entry in entries)
                {
                    foreach (var textureImporter in _texturesIterator.IterateTexturesAtPath(entry.path))
                    {
                        if (entry.excludePaths.Any(x => textureImporter.assetPath.Contains(x)))
                        {
                            Debug.Log(
                                $"[TexturePolicyEditor]: Texture {textureImporter.assetPath} is skipped because it is exclude path!");
                            continue;
                        }

                        if (entry.excludeTexturesPrefixes.Any(x => textureImporter.assetPath.Contains(x)))
                        {
                            Debug.Log(
                                $"[TexturePolicyEditor]: Texture {textureImporter.assetPath} is skipped because it has excluded texture prefix!");
                            continue;
                        }

                        var isChanged = false;
                        foreach (var texturePolicyHandler in _texturePolicyHandlers)
                        {
                            isChanged |= texturePolicyHandler.HandleTexture(textureImporter, entry);
                        }

                        if (isChanged)
                        {
                            AssetDatabase.ImportAsset(textureImporter.assetPath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error processing texture configuration! Error: {ex.Message}");
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }
        }
    }
}