#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

namespace Legato {
    internal class BeatmapLevel {
        internal readonly IPreviewBeatmapLevel previewBeatmapLevel;
        private readonly IBeatmapLevel? _beatmapLevel;

        internal BeatmapLevel(IPreviewBeatmapLevel previewBeatmapLevel) {
            this.previewBeatmapLevel = previewBeatmapLevel;
            _beatmapLevel = previewBeatmapLevel as IBeatmapLevel;
            previewMediaData = new PreviewMediaData(previewBeatmapLevel);
        }

        internal BeatmapLevel(IBeatmapLevel beatmapLevel) {
            previewBeatmapLevel = beatmapLevel;
            _beatmapLevel = beatmapLevel;
            previewMediaData = new PreviewMediaData(beatmapLevel);
        }

        public string levelID => previewBeatmapLevel.levelID;
        public string songName => previewBeatmapLevel.songName;
        public string songSubName => previewBeatmapLevel.songSubName;
        public string songAuthorName => previewBeatmapLevel.songAuthorName;
        public float beatsPerMinute => previewBeatmapLevel.beatsPerMinute;
        public float songDuration => previewBeatmapLevel.songDuration;
        public string[] allMappers => string.IsNullOrEmpty(previewBeatmapLevel.levelAuthorName) ? Array.Empty<string>() : new[] { previewBeatmapLevel.levelAuthorName };
        public string[] allLighters => Array.Empty<string>();
        public PreviewMediaData previewMediaData { get; }

        public IEnumerable<BeatmapKey> GetBeatmapKeys() {
            return _beatmapLevel?.beatmapLevelData?.difficultyBeatmapSets == null
                ? Enumerable.Empty<BeatmapKey>()
                : _beatmapLevel.beatmapLevelData.difficultyBeatmapSets
                    .SelectMany(set => set.difficultyBeatmaps)
                    .Select(difficultyBeatmap => new BeatmapKey(difficultyBeatmap));
        }
    }
}
