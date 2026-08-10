#nullable enable
using System.Threading.Tasks;

namespace Legato {
    internal static class BeatmapDataLoaderForwardPolyfills {
        internal static Task<IReadonlyBeatmapData?> LoadBeatmapDataAsync(
            this BeatmapDataLoader instance,
            IBeatmapLevelData beatmapLevelData,
            BeatmapKey beatmapKey,
            float startBpm,
            bool loadingForDesignatedEnvironment,
            IEnvironmentInfo? environmentInfo,
            BeatmapLevelDataVersion beatmapLevelDataVersion,
            GameplayModifiers? gameplayModifiers,
            PlayerSpecificSettings? playerSpecificSettings,
            bool enableBeatmapDataCaching) => instance.LoadBeatmapDataAsync(beatmapLevelData, beatmapKey, startBpm, loadingForDesignatedEnvironment, environmentInfo, null, beatmapLevelDataVersion, gameplayModifiers, playerSpecificSettings, enableBeatmapDataCaching);
    }
}
