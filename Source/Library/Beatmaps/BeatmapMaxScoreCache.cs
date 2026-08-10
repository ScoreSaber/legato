#nullable enable

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Legato.Beatmaps {
    internal class BeatmapMaxScoreCache {

        private readonly Dictionary<BeatmapKey, int> _cache = new Dictionary<BeatmapKey, int>();

        public async Task<int> GetMaxScore(BeatmapLevel beatmapLevel, BeatmapKey beatmapKey) {
            if (_cache.TryGetValue(beatmapKey, out int cachedScore)) {
                return cachedScore;
            }

            int maxScore = await ComputeMaxScore(beatmapLevel, beatmapKey);
            _cache[beatmapKey] = maxScore;
            return maxScore;
        }

#if BEAT_SABER_1_29_0
        private readonly PlayerDataModel _playerDataModel;

        public BeatmapMaxScoreCache(PlayerDataModel playerDataModel) {
            _playerDataModel = playerDataModel;
        }

        private async Task<int> ComputeMaxScore(BeatmapLevel beatmapLevel, BeatmapKey beatmapKey) {
            IDifficultyBeatmap difficultyBeatmap = beatmapKey.difficultyBeatmap
                ?? throw new InvalidOperationException($"Beatmap is unavailable for {beatmapKey.levelId}");
            var beatmapData = await difficultyBeatmap.GetBeatmapDataAsync(difficultyBeatmap.GetEnvironmentInfo(), _playerDataModel.playerData.playerSpecificSettings);
            return ScoreModel.ComputeMaxMultipliedScoreForBeatmap(beatmapData);
        }
#else
        private readonly BeatmapLevelLoader _beatmapLevelLoader;
        private readonly BeatmapDataLoader _beatmapDataLoader;
        private readonly BeatmapLevelsEntitlementModel _beatmapLevelsEntitlementModel;

        public BeatmapMaxScoreCache(BeatmapLevelLoader beatmapLevelLoader, BeatmapDataLoader beatmapDataLoader, BeatmapLevelsEntitlementModel beatmapLevelsEntitlementModel) {
            _beatmapLevelLoader = beatmapLevelLoader;
            _beatmapDataLoader = beatmapDataLoader;
            _beatmapLevelsEntitlementModel = beatmapLevelsEntitlementModel;
        }

        private async Task<int> ComputeMaxScore(BeatmapLevel beatmapLevel, BeatmapKey beatmapKey) {
            var beatmapLevelDataVersion = await _beatmapLevelsEntitlementModel.GetLevelDataVersionAsync(beatmapKey.levelId, CancellationToken.None);
            var beatmapLevelData = (await _beatmapLevelLoader.LoadBeatmapLevelDataAsync(beatmapLevel, beatmapLevelDataVersion, CancellationToken.None)).beatmapLevelData;
            if (beatmapLevelData == null) {
                throw new InvalidOperationException($"Beatmap data is unavailable for {beatmapKey.levelId}");
            }
            var beatmapData = await _beatmapDataLoader.LoadBeatmapDataAsync(beatmapLevelData: beatmapLevelData,
                                                                            beatmapKey: beatmapKey,
                                                                            startBpm: beatmapLevel.beatsPerMinute,
                                                                            loadingForDesignatedEnvironment: false,
#if BEAT_SABER_1_37_1
                                                                            environmentInfo: null,
#else
                                                                            originalEnvironmentInfo: null,
                                                                            targetEnvironmentInfo: null,
#endif
                                                                            beatmapLevelDataVersion: beatmapLevelDataVersion,
                                                                            gameplayModifiers: null,
                                                                            playerSpecificSettings: null,
                                                                            enableBeatmapDataCaching: false);
            if (beatmapData == null) {
                throw new InvalidOperationException($"Beatmap could not be loaded for {beatmapKey.levelId}");
            }
            return ScoreModel.ComputeMaxMultipliedScoreForBeatmap(beatmapData);
        }
#endif
    }
}
