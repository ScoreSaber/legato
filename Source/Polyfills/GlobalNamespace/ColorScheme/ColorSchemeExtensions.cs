#nullable enable

namespace Legato {
    internal static class ColorSchemeExtensions {
        internal static bool ShouldOverrideLightshowColors(this ColorScheme colorScheme) {
#if BEAT_SABER_1_29_0 || BEAT_SABER_1_37_1 || BEAT_SABER_1_38_0
            return false;
#else
            return colorScheme.overrideLights;
#endif
        }
    }
}
