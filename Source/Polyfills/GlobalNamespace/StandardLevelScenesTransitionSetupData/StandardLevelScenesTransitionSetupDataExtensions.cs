#nullable enable

namespace Legato {
    internal static class StandardLevelScenesTransitionSetupDataExtensions {
#if BEAT_SABER_1_29_0
        internal static BeatmapLevel? GetBeatmapLevel(this StandardLevelScenesTransitionSetupData transition) =>
            transition.difficultyBeatmap?.level == null ? null : new BeatmapLevel(transition.difficultyBeatmap.level);

        internal static BeatmapKey GetBeatmapKey(this StandardLevelScenesTransitionSetupData transition) =>
            new BeatmapKey(transition.difficultyBeatmap);
#else
        internal static BeatmapLevel? GetBeatmapLevel(this StandardLevelScenesTransitionSetupData transition) => transition.beatmapLevel;

        internal static BeatmapKey GetBeatmapKey(this StandardLevelScenesTransitionSetupData transition) => transition.beatmapKey;
#endif
    }
}
