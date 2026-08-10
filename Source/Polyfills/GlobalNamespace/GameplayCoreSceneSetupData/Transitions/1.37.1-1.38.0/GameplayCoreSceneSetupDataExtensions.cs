#nullable enable

namespace Legato {
    internal static partial class GameplayCoreSceneSetupDataExtensions {
        internal static string GetEnvironmentSerializedName(this GameplayCoreSceneSetupData instance) =>
#if BEAT_SABER_1_29_0 || BEAT_SABER_1_37_1
            instance.environmentInfo.serializedName;
#else
            instance.targetEnvironmentInfo.serializedName;
#endif

        internal static EnvironmentInfoSO GetTargetEnvironmentInfo(this GameplayCoreSceneSetupData instance) =>
#if BEAT_SABER_1_29_0 || BEAT_SABER_1_37_1
            instance.environmentInfo;
#else
            instance.targetEnvironmentInfo;
#endif
    }
}
