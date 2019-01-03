using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace HierarchyIcons
{
    /// <summary>
    /// Adds applicable icons next to game objects in the hierarchy.
    /// </summary>
    [InitializeOnLoad]
    static class IconDisplay
    {
        static Font _font;
        static Font font
        {
            get
            {
                if (_font == null)
                {
                    _font = LoadFont("HierarchyIcons");
                }

                return _font;
            }
        }

        static GUIStyle _labelStyle;
        public static GUIStyle labelStyle
        {
            get
            {
                if (_labelStyle == null)
                {
                    _labelStyle = new GUIStyle(EditorStyles.label);
                    _labelStyle.alignment = TextAnchor.MiddleRight;
                    _labelStyle.font = font;
                }

                return _labelStyle;
            }
        }

        static IconDisplay()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HighlightItems;
        }

        /// <summary>
        /// Gets a string of characters mapping to icons for the game object.
        /// </summary>
        static string GetIconString(GameObject target)
        {
            var icons = target
                .GetComponents<Component>()
                .Where(component => component != null)
                .Select(component => component.GetType())
                .Where(type => IconMapping.componentIcons.ContainsKey(type) &&
                               Preferences.visible[type.Name])
                .Select(type => IconMapping.componentIcons[type])
                .Distinct()
                .ToList();

            // Add icon for tag.
            if (target.tag != null &&
                IconMapping.tagIcons.ContainsKey(target.tag) &&
                Preferences.visible[target.tag])
            {
                var icon = IconMapping.tagIcons[target.tag];
                icons.Add(icon);
            }

            var iconStrings = icons.Select(icon => icon.ToString()).ToArray();
            return string.Join(" ", iconStrings);
        }

        /// <summary>
        /// Displays icons beside each game object in the Hierarchy panel.
        /// </summary>
        static void HighlightItems(int instanceID, Rect selectionRect)
        {
            var target = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

            if (target == null)
            {
                return;
            }

            var iconString = GetIconString(target);
            GUI.Label(selectionRect, iconString, labelStyle);
        }

        /// <summary>
        /// Loads a font from the asset database.
        /// </summary>
        static Font LoadFont(string name)
        {
            var guid = AssetDatabase.FindAssets(name).First();
            var path = AssetDatabase.GUIDToAssetPath(guid);
            return AssetDatabase.LoadAssetAtPath<Font>(path);
        }
    }
}
