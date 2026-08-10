#nullable enable

using IPA.Utilities;

namespace Legato {
    internal static class ResultsViewControllerExtensions {
#if BEAT_SABER_1_29_0
        private static readonly FieldAccessor<ResultsViewController, IDifficultyBeatmap>.Accessor DifficultyBeatmap =
            FieldAccessor<ResultsViewController, IDifficultyBeatmap>.GetAccessor("_difficultyBeatmap");

        internal static BeatmapLevel? GetBeatmapLevel(this ResultsViewController controller) {
            IDifficultyBeatmap difficultyBeatmap = DifficultyBeatmap(ref controller);
            return difficultyBeatmap?.level == null ? null : new BeatmapLevel(difficultyBeatmap.level);
        }

        internal static BeatmapKey GetBeatmapKey(this ResultsViewController controller) => new BeatmapKey(DifficultyBeatmap(ref controller));
#else
        private static readonly FieldAccessor<ResultsViewController, BeatmapLevel>.Accessor SelectedLevel =
            FieldAccessor<ResultsViewController, BeatmapLevel>.GetAccessor("_beatmapLevel");
        private static readonly FieldAccessor<ResultsViewController, BeatmapKey>.Accessor SelectedKey =
            FieldAccessor<ResultsViewController, BeatmapKey>.GetAccessor("_beatmapKey");

        internal static BeatmapLevel? GetBeatmapLevel(this ResultsViewController controller) => SelectedLevel(ref controller);

        internal static BeatmapKey GetBeatmapKey(this ResultsViewController controller) => SelectedKey(ref controller);
#endif
    }
}
