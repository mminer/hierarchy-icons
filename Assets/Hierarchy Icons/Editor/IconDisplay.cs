//
// Author: Matthew Miner
//         matthew@matthewminer.com
//         http://matthewminer.com
//
// Copyright (c) 2014
//

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
		const string fontPath = "Assets/Hierarchy Icons/Icons.ttf";
		static Font font;

		static GUIStyle _labelStyle;
		public static GUIStyle labelStyle
		{
			get {
				if (_labelStyle == null) {
					_labelStyle = new GUIStyle(EditorStyles.label);
					_labelStyle.font = font;
					_labelStyle.alignment = TextAnchor.MiddleRight;
				}

				return _labelStyle;
			}
		}

		static IconDisplay ()
		{
			font = AssetDatabase.LoadAssetAtPath(fontPath, typeof(Font)) as Font;

			if (font == null) {
				Debug.LogError("[Icon Hierarchy] The font file appears to be missing. Perhaps it was moved?");
			} else {
				EditorApplication.hierarchyWindowItemOnGUI += HighlightItems;
			}
		}

		/// <summary>
		/// Displays icons beside each game object in the Hierarchy panel.
		/// </summary>
		/// <param name="instanceID">ID of object.</param>
		/// <param name="selectionRect">Boundaries of object's label.</param>
		static void HighlightItems (int instanceID, Rect selectionRect)
		{
			var target = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
			var iconString = GetIconString(target);
			GUI.Label(selectionRect, iconString, labelStyle);
		}

		/// <summary>
		/// Gets a string of characters mapping to icons for the game object.
		/// </summary>
		/// <param name="target">Target game object.</param>
		/// <returns>Icon string.</returns>
		static string GetIconString (GameObject target)
		{
			var icons = target.GetComponents<Component>()
				.Select(component => component.GetType())
				.Where(type => IconMapping.componentIcons.ContainsKey(type) &&
				               Preferences.visible[type.Name])
				.Select(type => IconMapping.componentIcons[type])
				.Distinct()
				.ToList();

			// Add icon for tag.
			if (target.tag != null &&
					IconMapping.tagIcons.ContainsKey(target.tag) &&
					Preferences.visible[target.tag]) {
				icons.Add(IconMapping.tagIcons[target.tag]);
			}

			var iconString = string.Join(" ", icons.Select(c => c.ToString()).ToArray());
			return iconString;
		}
	}
}
