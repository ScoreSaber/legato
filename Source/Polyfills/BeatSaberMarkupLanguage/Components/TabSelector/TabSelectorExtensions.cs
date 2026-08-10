#nullable enable

using BeatSaberMarkupLanguage.Components;
using HMUI;

namespace Legato {
    internal static class TabSelectorExtensions {
        internal static TextSegmentedControl GetTextSegmentedControl(this TabSelector tabSelector) {
#if BEAT_SABER_1_29_0 || BEAT_SABER_1_37_1
            return tabSelector.textSegmentedControl;
#else
            return tabSelector.TextSegmentedControl;
#endif
        }
    }
}
