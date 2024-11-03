using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RML.Editor
{
    public class TexturePolicyConfiguratorWindow : EditorWindow
    {
        private const int OneStrokeHeight = 30;
        private const int TwoStrokeHeight = 40;
        private const int ShortWidth = 100;

        public TexturePolicyConfig texturePolicyConfig;

        private SerializedObject _serializedObject;

        private void OnEnable()
        {
            ScriptableObject target = this;
            _serializedObject = new SerializedObject(target);
        }
        
        [MenuItem("RML/Tools/Textures/Texture Policy Configurator Window")]
        public static void ShowWindow()
        {
            GetWindow(typeof(TexturePolicyConfiguratorWindow), false, "Texture Policy Configurator Window");
        }

        private void OnGUI()
        {
            _serializedObject.Update();

            var config = _serializedObject.FindProperty("texturePolicyConfig");
            EditorGUILayout.PropertyField(config, true);

            GUILayout.Space(20);

            GUILayout.BeginHorizontal();


            if (TwoStrokeButton("Process Textures From Config"))
            {
                if (texturePolicyConfig == null)
                {
                    DisplayDialog("Error", "You need to set texturePolicyConfig first!");
                    return;
                }

                var texturePolicyConfigurator = new TexturePolicyConfigurator();
                texturePolicyConfigurator.HandleTexturePolicy(texturePolicyConfig);
            }


            GUILayout.EndHorizontal();

            _serializedObject.ApplyModifiedProperties();

            bool TwoStrokeButton(string message) => GUILayout.Button(message, GUILayout.Height(TwoStrokeHeight));

            bool DisplayDialog(string dialogTitle = "Message", string message = "Are you sure?") =>
                EditorUtility.DisplayDialog(dialogTitle, message, "Ok", "Cancel");
        }
    }
}