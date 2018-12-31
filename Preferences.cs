using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace HierarchyIcons
{
    /// <summary>
    /// Options for controlling which icons are visible.
    /// </summary>
    static class Preferences
    {
        public static Dictionary<string, bool> visible { get; private set; }

        const string prefsPrefix = "hierarchyicons_";
        static Vector2 scrollPosition;

        static Preferences()
        {
            visible = IconMapping.componentIcons.Keys
                .Select(type => type.Name)
                .Concat(IconMapping.tagIcons.Keys)
                .ToDictionary(
                    name => name,
                    name => EditorPrefs.GetBool(prefsPrefix + name, true)
                );
        }

        [PreferenceItem("Hierarchy Icons")]
        static void OnGUI()
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            GUILayout.Label("Components", EditorStyles.boldLabel);

            foreach (var kvp in IconMapping.componentIcons)
            {
                var name = kvp.Key.Name;
                visible[name] = IconToggle(kvp.Value, name, visible[name]);
            }

            EditorGUILayout.Space();
            GUILayout.Label("Tags", EditorStyles.boldLabel);

            foreach (var kvp in IconMapping.tagIcons)
            {
                var name = kvp.Key;
                visible[name] = IconToggle(kvp.Value, name, visible[name]);
            }

            EditorGUILayout.EndScrollView();

            if (GUI.changed)
            {
                SavePreferences();
            }
        }

        static void SavePreferences()
        {
            foreach (var kvp in visible)
            {
                EditorPrefs.SetBool(prefsPrefix + kvp.Key, kvp.Value);
            }
        }

        /// <summary>
        /// Toggle box for icons.
        /// </summary>
        /// <param name="icon">Character of icon to display.<param>
        /// <param name="name">Name of component or tag.</param>
        /// <param name="val">Shown state of toggle box.</param>
        /// <returns>New state of toggle box.</returns>
        static bool IconToggle(char icon, string name, bool val)
        {
            GUILayout.BeginHorizontal();

            var text = icon.ToString();
            GUILayout.Label(text, IconDisplay.labelStyle, GUILayout.ExpandWidth(false));
            val = EditorGUILayout.Toggle(name, val);

            GUILayout.EndHorizontal();
            return val;
        }
    }
}
