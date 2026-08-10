#nullable enable

namespace Legato {
    internal static class LevelCollectionViewControllerExtensions {
        internal static void SelectLevel(this LevelCollectionViewController controller, BeatmapLevel level) {
#if BEAT_SABER_1_29_0
            controller.SelectLevel(level.previewBeatmapLevel);
#else
            controller.SelectLevel(level);
#endif
        }
    }
}
