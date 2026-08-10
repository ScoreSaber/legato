#nullable enable

using System;

namespace Legato {
    internal readonly struct BeatmapKey : IEquatable<BeatmapKey> {
        internal readonly IDifficultyBeatmap? difficultyBeatmap;

        internal BeatmapKey(IDifficultyBeatmap? difficultyBeatmap) {
            this.difficultyBeatmap = difficultyBeatmap;
        }

        public string? levelId => difficultyBeatmap?.level?.levelID;
        public BeatmapDifficulty difficulty => difficultyBeatmap?.difficulty ?? default;
        public BeatmapCharacteristicSO? beatmapCharacteristic => difficultyBeatmap?.parentDifficultyBeatmapSet?.beatmapCharacteristic;

        public bool Equals(BeatmapKey other) => levelId == other.levelId && difficulty == other.difficulty && beatmapCharacteristic == other.beatmapCharacteristic;

        public override bool Equals(object? obj) => obj is BeatmapKey other && Equals(other);

        public override int GetHashCode() {
            unchecked {
                int hash = levelId != null ? levelId.GetHashCode() : 0;
                hash = (hash * 397) ^ (int)difficulty;
                hash = (hash * 397) ^ (beatmapCharacteristic != null ? beatmapCharacteristic.GetHashCode() : 0);
                return hash;
            }
        }
    }
}
