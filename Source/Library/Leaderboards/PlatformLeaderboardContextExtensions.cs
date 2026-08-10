#nullable enable

using IPA.Utilities;

namespace Legato.Leaderboards {
    internal static class PlatformLeaderboardContextExtensions {
#if BEAT_SABER_1_29_0
        private static readonly FieldAccessor<PlatformLeaderboardViewController, IDifficultyBeatmap>.Accessor PlatformDifficultyBeatmap =
            FieldAccessor<PlatformLeaderboardViewController, IDifficultyBeatmap>.GetAccessor("_difficultyBeatmap");
#else
        private static readonly FieldAccessor<PlatformLeaderboardViewController, BeatmapKey>.Accessor PlatformBeatmapKey =
            FieldAccessor<PlatformLeaderboardViewController, BeatmapKey>.GetAccessor("_beatmapKey");
#endif

        internal static bool TryGetBeatmapKey(this PlatformLeaderboardViewController controller, out BeatmapKey beatmapKey) {
#if BEAT_SABER_1_29_0
            IDifficultyBeatmap difficultyBeatmap = PlatformDifficultyBeatmap(ref controller);
            if (difficultyBeatmap == null) {
                beatmapKey = default;
                return false;
            }

            beatmapKey = new BeatmapKey(difficultyBeatmap);
            return true;
#else
            beatmapKey = PlatformBeatmapKey(ref controller);
            return !string.IsNullOrEmpty(beatmapKey.levelId);
#endif
        }
    }
}
