#nullable enable

namespace Legato {
    internal static class BeatmapKeyExtensions {
        internal static string CharacteristicSerializedName(this BeatmapKey beatmapKey) {
#if BEAT_SABER_1_29_0 || BEAT_SABER_1_37_1 || BEAT_SABER_1_38_0 || BEAT_SABER_1_40_0 || BEAT_SABER_1_42_0
            return beatmapKey.beatmapCharacteristic?.serializedName ?? string.Empty;
#else
            return beatmapKey.characteristic.SerializedName();
#endif
        }

        internal static bool CharacteristicMatches(this BeatmapKey beatmapKey, string name) {
#if BEAT_SABER_1_29_0 || BEAT_SABER_1_37_1 || BEAT_SABER_1_38_0 || BEAT_SABER_1_40_0 || BEAT_SABER_1_42_0
            return name == beatmapKey.beatmapCharacteristic?.serializedName
                || name == beatmapKey.beatmapCharacteristic?.characteristicNameLocalizationKey;
#else
            return name == beatmapKey.characteristic.SerializedName()
                || name == beatmapKey.characteristic.NameLocalizationKey();
#endif
        }

        internal static string GetEnvironmentName(this BeatmapLevel beatmapLevel, in BeatmapKey beatmapKey) =>
#if BEAT_SABER_1_29_0
            beatmapKey.difficultyBeatmap.GetEnvironmentInfo()?.serializedName ?? string.Empty;
#elif BEAT_SABER_1_37_1 || BEAT_SABER_1_38_0 || BEAT_SABER_1_40_0 || BEAT_SABER_1_42_0
            beatmapLevel.GetEnvironmentName(beatmapKey.beatmapCharacteristic, beatmapKey.difficulty);
#else
            beatmapLevel.GetEnvironmentName(beatmapKey.characteristic, beatmapKey.difficulty);
#endif

#if !BEAT_SABER_1_29_0
        internal static BeatmapBasicData? GetDifficultyBeatmapData(this BeatmapLevel beatmapLevel, in BeatmapKey beatmapKey) {
#if BEAT_SABER_1_37_1 || BEAT_SABER_1_38_0 || BEAT_SABER_1_40_0 || BEAT_SABER_1_42_0
            return beatmapLevel.GetDifficultyBeatmapData(beatmapKey.beatmapCharacteristic, beatmapKey.difficulty);
#else
            return beatmapLevel.GetDifficultyBeatmapData(beatmapKey.characteristic, beatmapKey.difficulty);
#endif
        }
#endif

        internal static ColorScheme? GetColorScheme(this BeatmapLevel beatmapLevel, in BeatmapKey beatmapKey) {
#if BEAT_SABER_1_29_0
            return null;
#elif BEAT_SABER_1_37_1 || BEAT_SABER_1_38_0 || BEAT_SABER_1_40_0 || BEAT_SABER_1_42_0
            return beatmapLevel.GetColorScheme(beatmapKey.beatmapCharacteristic, beatmapKey.difficulty);
#else
            return beatmapLevel.GetColorScheme(beatmapKey.characteristic, beatmapKey.difficulty);
#endif
        }
    }
}
