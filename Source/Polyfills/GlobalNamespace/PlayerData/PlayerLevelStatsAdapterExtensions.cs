#nullable enable

using System.Collections.Generic;
using System.Linq;

namespace Legato {
    internal readonly struct PlayerLevelStatsAdapter {
        internal PlayerLevelStatsAdapter(string levelID, int playCount) {
            this.levelID = levelID;
            this.playCount = playCount;
        }

        internal string levelID { get; }
        internal int playCount { get; }
    }

    internal static class PlayerLevelStatsAdapterExtensions {
        internal static IEnumerable<PlayerLevelStatsAdapter> GetLevelStats(this PlayerData playerData) {
#if BEAT_SABER_1_29_0
            return playerData.levelsStatsData.Select(stats => new PlayerLevelStatsAdapter(stats.levelID, stats.playCount));
#else
            return playerData.levelsStatsData.Select(stats => new PlayerLevelStatsAdapter(stats.Key.levelId, stats.Value.playCount));
#endif
        }
    }
}
