#nullable enable

#if BEAT_SABER_1_40_0 || BEAT_SABER_1_42_0
using Legato.Gameplay.Movement;
using HarmonyLib;

namespace Legato {
    [HarmonyPatch(typeof(VariableMovementDataProvider), nameof(VariableMovementDataProvider.Init))]
    internal static class VariableMovementDataProviderEventsPatch {
        private static void Prefix(
            ref float noteJumpMovementSpeed,
            ref BeatmapObjectSpawnMovementData.NoteJumpValueType noteJumpValueType,
            ref float noteJumpValue) =>
            MovementDataEvents.RaiseInitializing(ref noteJumpMovementSpeed, ref noteJumpValueType, ref noteJumpValue);
    }
}
#endif
