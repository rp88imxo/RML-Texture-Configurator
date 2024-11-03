using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RML.Editor
{
    public interface IFormatTextureStrategy
    {
        public bool FormatTexture(TextureImporter textureImporter, TexturePolicyPathConfigEntry configEntry);
    }

    public class DefaultFormatTextureStrategy : IFormatTextureStrategy
    {
        public bool FormatTexture(TextureImporter textureImporter, TexturePolicyPathConfigEntry configEntry)
        {
            var settings = textureImporter.GetPlatformTextureSettings("Switch");

            if (settings.textureCompression != configEntry.texturePolicyOptions.textureImporterCompression)
            {
                settings.textureCompression = configEntry.texturePolicyOptions.textureImporterCompression;

                settings.overridden = true;
                textureImporter.SetPlatformTextureSettings(settings);

                return true;
            }

            return false;
        }
    }

    public class AstcFormatTextureStrategy : IFormatTextureStrategy
    {
        public bool FormatTexture(TextureImporter textureImporter, TexturePolicyPathConfigEntry configEntry)
        {
            var settings = textureImporter.GetPlatformTextureSettings("Switch");

            var requiredFormat =
                ImporterCompressionToFormat(configEntry.texturePolicyOptions.textureImporterCompression);

            if (settings.format != requiredFormat)
            {
                settings.format = requiredFormat;
                settings.overridden = true;
                textureImporter.SetPlatformTextureSettings(settings);

                return true;
            }

            return false;
        }

        private TextureImporterFormat ImporterCompressionToFormat(TextureImporterCompression textureImporterCompression)
        {
            return textureImporterCompression switch
            {
                TextureImporterCompression.Uncompressed => TextureImporterFormat.ASTC_4x4,
                TextureImporterCompression.Compressed => TextureImporterFormat.ASTC_8x8,
                TextureImporterCompression.CompressedHQ => TextureImporterFormat.ASTC_6x6,
                TextureImporterCompression.CompressedLQ => TextureImporterFormat.ASTC_12x12,
                _ => throw new ArgumentOutOfRangeException(nameof(textureImporterCompression),
                    textureImporterCompression,
                    null)
            };
        }
    }


    public class FormatTexturePolicyHandler : ITexturePolicyHandler
    {
        private readonly IFormatTextureStrategy _formatTextureStrategy;

        public FormatTexturePolicyHandler(IFormatTextureStrategy formatTextureStrategy)
        {
            _formatTextureStrategy = formatTextureStrategy;
        }

        public bool HandleTexture(TextureImporter textureImporter, TexturePolicyPathConfigEntry configEntry)
        {
            return _formatTextureStrategy.FormatTexture(textureImporter, configEntry);
        }
    }
}