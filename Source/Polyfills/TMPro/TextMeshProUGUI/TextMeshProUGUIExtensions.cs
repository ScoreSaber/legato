#nullable enable

using TMPro;

namespace Legato {
    internal static class TextMeshProUGUIExtensions {
        internal static void SetWordWrapping(this TextMeshProUGUI text, bool enabled) {
#if BEAT_SABER_1_29_0 || BEAT_SABER_1_37_1 || BEAT_SABER_1_38_0 || BEAT_SABER_1_40_0
#pragma warning disable CS0618
            text.enableWordWrapping = enabled;
#pragma warning restore CS0618
#else
            text.textWrappingMode = enabled ? TextWrappingModes.Normal : TextWrappingModes.NoWrap;
#endif
        }
    }
}
