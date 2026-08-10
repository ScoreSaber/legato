#nullable enable

namespace Legato {
    internal static class LevelFilteringBeatmapLevelPackExtensions {
        internal static BeatmapLevelPackAdapter? GetSelectedLevelPack(this LevelFilteringNavigationController controller) {
#if BEAT_SABER_1_29_0
            IBeatmapLevelPack pack = controller.selectedBeatmapLevelPack;
            return pack == null ? null : new BeatmapLevelPackAdapter(pack);
#else
            BeatmapLevelPack pack = controller.selectedBeatmapLevelPack;
            return pack == null ? null : new BeatmapLevelPackAdapter(pack);
#endif
        }

        internal static void SelectLevelPack(this LevelFilteringNavigationController controller, BeatmapLevelPackAdapter? pack) {
            if (pack == null) {
                return;
            }

#if BEAT_SABER_1_29_0
            controller.SelectAnnotatedBeatmapLevelCollection(pack.source as IBeatmapLevelPack);
            controller.HandleAnnotatedBeatmapLevelCollectionsViewControllerDidSelectAnnotatedBeatmapLevelCollection(pack.source);
#else
            controller.SelectAnnotatedBeatmapLevelCollection(pack.source);
            controller.HandleAnnotatedBeatmapLevelCollectionsViewControllerDidSelectAnnotatedBeatmapLevelCollection(pack.source);
#endif
        }
    }
}
