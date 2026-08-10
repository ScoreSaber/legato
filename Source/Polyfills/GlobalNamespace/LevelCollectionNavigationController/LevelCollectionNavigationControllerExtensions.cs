#nullable enable

namespace Legato {
    internal static class LevelCollectionNavigationControllerExtensions {
        internal static void SelectLevel(this LevelCollectionNavigationController controller, BeatmapLevel level) {
#if BEAT_SABER_1_29_0
            controller.SelectLevel(level.previewBeatmapLevel);
#else
            controller.SelectLevel(level);
#endif
        }
    }
}
