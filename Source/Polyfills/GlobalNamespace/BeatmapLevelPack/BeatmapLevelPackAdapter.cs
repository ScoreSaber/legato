#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Legato {
    // The 1.37 model replaced the interface graph with concrete level and pack objects.
    // Keep that seam here so a port only has one representation to work with.
    internal sealed class BeatmapLevelAdapter : IPreviewBeatmapLevel {
#if BEAT_SABER_1_29_0
        internal readonly IPreviewBeatmapLevel source;

        internal BeatmapLevelAdapter(IPreviewBeatmapLevel source) {
            this.source = source;
        }

        internal string[] allMappers => string.IsNullOrEmpty(source.levelAuthorName) ? Array.Empty<string>() : new[] { source.levelAuthorName };
        internal string[] allLighters => Array.Empty<string>();
#else
        internal readonly BeatmapLevel source;

        internal BeatmapLevelAdapter(BeatmapLevel source) {
            this.source = source;
        }

        internal string[] allMappers => source.allMappers;
        internal string[] allLighters => source.allLighters;
#endif

        public string levelID => source.levelID;
        public string songName => source.songName;
        public string songSubName => source.songSubName;
        public string songAuthorName => source.songAuthorName;
        public string levelAuthorName => allMappers.FirstOrDefault() ?? string.Empty;
        public float beatsPerMinute => source.beatsPerMinute;
        public float songTimeOffset => source.songTimeOffset;
        public float previewStartTime => source.previewStartTime;
        public float previewDuration => source.previewDuration;
        public float songDuration => source.songDuration;

#if BEAT_SABER_1_29_0
        public float shuffle => source.shuffle;
        public float shufflePeriod => source.shufflePeriod;
        public EnvironmentInfoSO environmentInfo => source.environmentInfo;
        public EnvironmentInfoSO allDirectionsEnvironmentInfo => source.allDirectionsEnvironmentInfo;
        public IReadOnlyList<PreviewDifficultyBeatmapSet> previewDifficultyBeatmapSets => source.previewDifficultyBeatmapSets;
        public Task<Sprite> GetCoverImageAsync(CancellationToken cancellationToken) => source.GetCoverImageAsync(cancellationToken);
#endif

#if BEAT_SABER_1_29_0
        internal IReadOnlyList<BeatmapDifficultyAdapter> difficulties => Array.Empty<BeatmapDifficultyAdapter>();
#else
        internal IReadOnlyList<BeatmapDifficultyAdapter> difficulties => source.beatmapBasicData.Keys
            .Select(key => new BeatmapDifficultyAdapter(key.characteristic.serializedName, key.difficulty.ToString()))
            .ToArray();
#endif
    }

    internal sealed class BeatmapDifficultyAdapter {
        internal BeatmapDifficultyAdapter(string characteristic, string name) {
            this.characteristic = characteristic;
            this.name = name;
        }

        internal string characteristic { get; }
        internal string name { get; }
    }

    internal sealed class BeatmapLevelCollectionAdapter : IBeatmapLevelCollection {
        private readonly IReadOnlyList<IPreviewBeatmapLevel> _levels;

        internal BeatmapLevelCollectionAdapter(IEnumerable<BeatmapLevelAdapter> levels) {
            _levels = levels.Cast<IPreviewBeatmapLevel>().ToArray();
        }

        public IReadOnlyList<IPreviewBeatmapLevel> beatmapLevels => _levels;
    }

    internal sealed class BeatmapLevelPackAdapter : IBeatmapLevelPack {
#if BEAT_SABER_1_29_0
        internal readonly IAnnotatedBeatmapLevelCollection source;

        internal BeatmapLevelPackAdapter(IAnnotatedBeatmapLevelCollection source) {
            this.source = source;
        }

        internal IReadOnlyList<BeatmapLevelAdapter> beatmapLevels => source.beatmapLevelCollection.beatmapLevels
            .Select(level => new BeatmapLevelAdapter(level))
            .ToArray();
#else
        internal readonly BeatmapLevelPack source;

        internal BeatmapLevelPackAdapter(BeatmapLevelPack source) {
            this.source = source;
        }

#if BEAT_SABER_1_37_1
        internal IReadOnlyList<BeatmapLevelAdapter> beatmapLevels => source.beatmapLevels
#else
        internal IReadOnlyList<BeatmapLevelAdapter> beatmapLevels => source.AllBeatmapLevels()
#endif
            .Select(level => new BeatmapLevelAdapter(level))
            .ToArray();
#endif

#if BEAT_SABER_1_29_0
        public string packID => (source as IBeatmapLevelPack)?.packID ?? string.Empty;
        public string packName => (source as IBeatmapLevelPack)?.packName ?? source.collectionName;
        public string shortPackName => (source as IBeatmapLevelPack)?.shortPackName ?? source.collectionName;
        public string collectionName => source.collectionName;
#else
        public string packID => source.packID;
        public string packName => source.packName;
        public string shortPackName => source.shortPackName;
        public string collectionName => source.shortPackName;
#endif
        public Sprite coverImage => source.coverImage;
        public Sprite smallCoverImage => source.smallCoverImage;
        public IBeatmapLevelCollection beatmapLevelCollection => new BeatmapLevelCollectionAdapter(beatmapLevels);
        public bool isPackAlwaysOwned => true;

        internal static BeatmapLevelPackAdapter From(IAnnotatedBeatmapLevelCollection source) {
#if BEAT_SABER_1_29_0
            return source as BeatmapLevelPackAdapter ?? new BeatmapLevelPackAdapter(source);
#else
            return source as BeatmapLevelPackAdapter
                ?? throw new ArgumentException("The pack did not originate from a Legato beatmap view", nameof(source));
#endif
        }

        internal static BeatmapLevelPackAdapter CreateFiltered(
            string packID,
            string packName,
            string shortPackName,
            Sprite coverImage,
            Sprite smallCoverImage,
            IReadOnlyList<BeatmapLevelAdapter> levels) {
#if BEAT_SABER_1_29_0
            return new BeatmapLevelPackAdapter(new BeatmapLevelPack(
                packID,
                packName,
                shortPackName,
                coverImage,
                smallCoverImage,
                new BeatmapLevelCollection(levels.Select(level => level.source).ToArray())));
#elif BEAT_SABER_1_37_1
            return new BeatmapLevelPackAdapter(new BeatmapLevelPack(
                packID,
                packName,
                shortPackName,
                coverImage,
                smallCoverImage,
                levels.Select(level => level.source).ToArray(),
                PlayerSensitivityFlag.Safe));
#else
            return new BeatmapLevelPackAdapter(new BeatmapLevelPack(
                packID,
                packName,
                shortPackName,
                coverImage,
                smallCoverImage,
                PackBuyOption.Default,
                levels.Select(level => level.source).ToArray(),
                PlayerSensitivityFlag.Safe));
#endif
        }

        internal static BeatmapLevelPackAdapter CreateFiltered(
            string packID,
            string packName,
            string shortPackName,
            Sprite coverImage,
            Sprite smallCoverImage,
            IReadOnlyList<IPreviewBeatmapLevel> levels) => CreateFiltered(
                packID,
                packName,
                shortPackName,
                coverImage,
                smallCoverImage,
                levels.Select(level => level as BeatmapLevelAdapter
                    ?? throw new ArgumentException("The level did not originate from a Legato beatmap view", nameof(levels)))
                    .ToArray());
    }
}
