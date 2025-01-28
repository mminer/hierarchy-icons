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
            { typeof(Camera),         'c' },
            { typeof(Light),          'l' },
            { typeof(Projector),      'P' },
#if USE_ANIMATION
            { typeof(Animation),      '>' },
#endif

#if USE_AUDIO
            { typeof(AudioListener),  'A' },
            { typeof(AudioSource),    'a' },
#endif

#if USE_CLOTH
            { typeof(Cloth),          'C' },
#endif

#if USE_PARTICLES
            { typeof(ParticleSystem), 'p' },
#endif

#if USE_PHYSICS
            { typeof(ConstantForce),  'f' },
            { typeof(Rigidbody),      'r' },
#endif

#if USE_TERRAIN
            { typeof(Terrain),        't' },
            { typeof(Tree),           'T' },
#endif
        };

        public static readonly Dictionary<string, char> tagIcons = new Dictionary<string, char>()
        {
            { "Player", 'q' }
        };
    }
}
