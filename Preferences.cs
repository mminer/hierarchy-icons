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

        [SettingsProvider]
        static SettingsProvider CreateSettingsProvider()
        {
            var provider = new SettingsProvider("Project/HierarchyIcons", SettingsScope.User, new[] { "Hierarchy", "Icon" })
            {
                activateHandler = (searchContext, rootElement) => Load(),
                guiHandler = searchContext => DisplayGUI(),
                label = "Hierarchy Icons"
            };

            return provider;
        }

        static void DisplayGUI()
        {
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

            if (GUI.changed)
            {
                Save();
            }
        }

        static bool IconToggle(char icon, string name, bool val)
        {
            GUILayout.BeginHorizontal();
            var label = icon.ToString();
            GUILayout.Label(label, IconDisplay.labelStyle, GUILayout.ExpandWidth(false));
            val = EditorGUILayout.Toggle(name, val);
            GUILayout.EndHorizontal();
            return val;
        }

        static void Load()
        {
            visible = IconMapping.componentIcons.Keys
                .Select(type => type.Name)
                .Concat(IconMapping.tagIcons.Keys)
                .ToDictionary(
                    name => name,
                    name => EditorPrefs.GetBool(prefsPrefix + name, true)
                );
        }

        static void Save()
        {
            foreach (var kvp in visible)
            {
                EditorPrefs.SetBool(prefsPrefix + kvp.Key, kvp.Value);
            }
        }
    }
}
