using System;
using System.Collections.Generic;
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
                    // TODO: allow folder to be anywhere
                    _font = AssetDatabase.LoadAssetAtPath<Font>("Assets/Editor/Hierarchy Icons/Icons.ttf");
                }

                return _font;
            }
        }

        static GUIStyle _labelStyle;
        internal static GUIStyle labelStyle
        {
            get
            {
                if (_labelStyle == null)
                {
                    _labelStyle = new GUIStyle(EditorStyles.label);
                    _labelStyle.font = font;
                    _labelStyle.alignment = TextAnchor.MiddleRight;
                }

                return _labelStyle;
            }
        }

        static IconDisplay()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HighlightItems;
        }

        /// <summary>
        /// Displays icons beside each game object in the Hierarchy panel.
        /// </summary>
        /// <param name="instanceID">ID of object.</param>
        /// <param name="selectionRect">Boundaries of object's label.</param>
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
        /// Gets a string of characters mapping to icons for the game object.
        /// </summary>
        static string GetIconString(GameObject target)
        {
            var icons = target
                .GetComponents<Component>()
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

            return string.Join(" ", icons.Select(c => c.ToString()).ToArray());
        }
    }
}
