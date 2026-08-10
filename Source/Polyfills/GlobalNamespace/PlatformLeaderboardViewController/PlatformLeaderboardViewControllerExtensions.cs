#nullable enable

namespace Legato {
    internal static class PlatformLeaderboardViewControllerExtensions {
        internal static void SetData(this PlatformLeaderboardViewController controller, in BeatmapKey beatmapKey) {
#if BEAT_SABER_1_29_0
            controller.SetData(beatmapKey.difficultyBeatmap);
#else
            controller.SetData(beatmapKey);
#endif
        }
    }
}
