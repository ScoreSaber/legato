#nullable enable

using IPA.Utilities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Legato.Beatmaps {
    public abstract class MutableBeatmapLevelPack : BeatmapLevelPack {
#if BEAT_SABER_1_37_1
        private static readonly FieldAccessor<BeatmapLevelPack, BeatmapLevel[]>.Accessor BeatmapLevels =
            FieldAccessor<BeatmapLevelPack, BeatmapLevel[]>.GetAccessor("beatmapLevels");

        protected MutableBeatmapLevelPack(string packId, string packName, Sprite coverImage, BeatmapLevel[] beatmapLevels, string shortPackName = "")
            : base(packId, packName, shortPackName.Length == 0 ? packName : shortPackName, coverImage, coverImage, beatmapLevels, PlayerSensitivityFlag.Safe) { }

        protected void SetBeatmapLevels(BeatmapLevel[] beatmapLevels) {
            BeatmapLevelPack pack = this;
            BeatmapLevels(ref pack) = beatmapLevels;
        }
#else
        private static readonly FieldAccessor<BeatmapLevelPack, List<BeatmapLevel>>.Accessor AllLevels =
            FieldAccessor<BeatmapLevelPack, List<BeatmapLevel>>.GetAccessor("_allBeatmapLevels");
        private static readonly FieldAccessor<BeatmapLevelPack, List<BeatmapLevel>>.Accessor AdditionalBeatmapLevels =
            FieldAccessor<BeatmapLevelPack, List<BeatmapLevel>>.GetAccessor("_additionalBeatmapLevels");

        protected MutableBeatmapLevelPack(string packId, string packName, Sprite coverImage, BeatmapLevel[] beatmapLevels, string shortPackName = "")
            : base(packId, packName, shortPackName.Length == 0 ? packName : shortPackName, coverImage, coverImage, PackBuyOption.Default, beatmapLevels, PlayerSensitivityFlag.Safe) { }

        protected void SetBeatmapLevels(BeatmapLevel[] beatmapLevels) {
            BeatmapLevelPack pack = this;
            AllLevels(ref pack) = beatmapLevels.Concat(AdditionalBeatmapLevels(ref pack)).ToList();
        }
#endif
    }
}
