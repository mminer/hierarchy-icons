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
        public static readonly Dictionary<Type, char> componentIcons = new Dictionary<Type, char>()
        {
            { typeof(Animation),      '>' },
            { typeof(AudioListener),  'A' },
            { typeof(AudioSource),    'a' },
            { typeof(Camera),         'c' },
            { typeof(Cloth),          'C' },
            { typeof(ConstantForce),  'f' },
            { typeof(Light),          'l' },
            { typeof(ParticleSystem), 'p' },
            { typeof(Projector),      'P' },
            { typeof(Rigidbody),      'r' },
            { typeof(Terrain),        't' },
            { typeof(Tree),           'T' },
        };

        public static readonly Dictionary<string, char> tagIcons = new Dictionary<string, char>()
        {
            { "Player", 'q' }
        };
    }
}
