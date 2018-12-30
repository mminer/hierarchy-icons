using System;
using System.Collections.Generic;
using UnityEngine;

namespace HierarchyIcons
{
    /// <summary>
    /// Components mapped to their corresponding icon (a character in icon font).
    /// </summary>
    static class IconMapping
    {
        internal static readonly Dictionary<Type, char> componentIcons = new Dictionary<Type, char>()
        {
            { typeof(Animation),      '>' },
            { typeof(AudioListener),  'A' },
            { typeof(AudioSource),    'a' },
            { typeof(Camera),         'c' },
            { typeof(Cloth),          'C' },
            { typeof(ConstantForce),  'f' },
            { typeof(GUIText),        'g' },
            { typeof(Light),          'l' },
            { typeof(NetworkView),    'n' },
            { typeof(ParticleSystem), 'p' },
            { typeof(Projector),      'P' },
            { typeof(Rigidbody),      'r' },
            { typeof(Terrain),        't' },
            { typeof(Tree),           'T' },
        };

        internal static readonly Dictionary<string, char> tagIcons = new Dictionary<string, char>()
        {
            { "Player", 'q' }
        };
    }
}
