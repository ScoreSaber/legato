#nullable enable

using BeatSaberMarkupLanguage.Components;
using UnityEngine.UI;

namespace Legato {
    internal static class BackgroundableExtensions {
        internal static Image GetBackground(this Backgroundable backgroundable) {
#if BEAT_SABER_1_29_0 || BEAT_SABER_1_37_1
            return backgroundable.background;
#else
            return backgroundable.Background;
#endif
        }
    }
}
