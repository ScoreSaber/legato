#nullable enable

using BeatSaberMarkupLanguage;
using System.Collections.Generic;
using UnityEngine;

namespace Legato {
    internal static class ComponentTypeWithDataExtensions {
        internal static Component GetComponent(this BSMLParser.ComponentTypeWithData componentType) {
#if BEAT_SABER_1_29_0 || BEAT_SABER_1_37_1
            return componentType.component;
#else
            return componentType.Component;
#endif
        }

        internal static Dictionary<string, string> GetData(this BSMLParser.ComponentTypeWithData componentType) {
#if BEAT_SABER_1_29_0 || BEAT_SABER_1_37_1
            return componentType.data;
#else
            return componentType.Data;
#endif
        }
    }
}
