#nullable enable

namespace Legato {
    internal static partial class GameplayCoreSceneSetupDataExtensions {
        internal static BeatmapKey GetBeatmapKey(this GameplayCoreSceneSetupData instance) =>
#if BEAT_SABER_1_29_0
            new BeatmapKey(instance.difficultyBeatmap);
#else
            instance.beatmapKey;
#endif

        internal static BeatmapLevel? GetBeatmapLevel(this GameplayCoreSceneSetupData instance) =>
#if BEAT_SABER_1_29_0
            instance.previewBeatmapLevel == null ? null : new BeatmapLevel(instance.previewBeatmapLevel);
#else
            instance.beatmapLevel;
#endif
    }
}
