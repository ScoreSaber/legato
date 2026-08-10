#nullable enable

using LeaderboardCore.Models;

namespace Legato.Leaderboards {
    internal abstract class CustomLeaderboardAdapter : CustomLeaderboard {
#if BEAT_SABER_1_29_0
        public sealed override bool ShowForLevel(IPreviewBeatmapLevel selectedLevel) => selectedLevel != null && ShowForLevelId(selectedLevel.levelID);
#else
        public sealed override bool ShowForLevel(BeatmapKey? beatmapKey) => beatmapKey.HasValue && ShowForLevelId(beatmapKey.Value.levelId);
#endif

        protected abstract bool ShowForLevelId(string levelId);
    }
}
