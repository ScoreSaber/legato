#nullable enable

using System.Linq;
using UnityEngine;

namespace Legato {
    internal static class LevelCollectionViewBeatmapLevelPackExtensions {
        internal static void SetData(
            this LevelCollectionViewController controller,
            BeatmapLevelPackAdapter pack,
            string headerText,
            Sprite headerSprite,
            GameObject noDataInfoPrefab) {
#if BEAT_SABER_1_29_0
            controller.SetData(pack.source.beatmapLevelCollection, headerText, headerSprite, false, noDataInfoPrefab);
#else
            controller.SetData(
                pack.beatmapLevels.Select(level => level.source).ToArray(),
                headerText,
                headerSprite,
                false,
                false,
                noDataInfoPrefab);
#endif
        }
    }
}
