#nullable enable

namespace Legato {
    internal static class BeatmapLevelsModelExtensions {
        internal static BeatmapLevel? GetBeatmapLevel(this BeatmapLevelsModel beatmapLevelsModel, string levelId) {
            IPreviewBeatmapLevel? previewBeatmapLevel = beatmapLevelsModel.GetLevelPreviewForLevelId(levelId);
            return previewBeatmapLevel == null ? null : new BeatmapLevel(previewBeatmapLevel);
        }
    }
}
