#nullable enable

using HarmonyLib;

namespace Legato.Leaderboards {
    internal static class SelectedLevelExtensions {
        internal static string? GetSelectedLevelId(this object navigationController) {
#if BEAT_SABER_1_29_0
            return Traverse.Create(navigationController).Field("selectedLevel").GetValue<IPreviewBeatmapLevel>()?.levelID;
#else
            BeatmapKey? beatmapKey = Traverse.Create(navigationController).Field("selectedLevelKey").GetValue<BeatmapKey?>();
            return beatmapKey?.levelId;
#endif
        }
    }
}
