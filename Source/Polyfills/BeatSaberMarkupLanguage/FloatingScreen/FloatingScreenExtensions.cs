#nullable enable

using BeatSaberMarkupLanguage.FloatingScreen;
using UnityEngine;

namespace Legato {
    internal static class FloatingScreenExtensions {
        internal static GameObject GetHandle(this FloatingScreen screen) {
#if BEAT_SABER_1_29_0 || BEAT_SABER_1_37_1
            return screen.handle;
#else
            return screen.Handle;
#endif
        }
    }
}
