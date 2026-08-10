#nullable enable

using BeatSaberMarkupLanguage;

namespace Legato.Presentation {
    internal static class BsmlParser {
        internal static BSMLParser Instance {
            get {
#if BEAT_SABER_1_29_0 || BEAT_SABER_1_37_1
                return BSMLParser.instance;
#else
                return BSMLParser.Instance;
#endif
            }
        }
    }
}
