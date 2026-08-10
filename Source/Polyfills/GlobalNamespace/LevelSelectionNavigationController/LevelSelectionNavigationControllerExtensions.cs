#nullable enable

namespace Legato {
    internal static class LevelSelectionNavigationControllerExtensions {
        internal static BeatmapKey GetBeatmapKey(this LevelSelectionNavigationController controller) {
#if BEAT_SABER_1_29_0
            return new BeatmapKey(controller.selectedDifficultyBeatmap);
#else
            return controller.beatmapKey;
#endif
        }
    }
}
