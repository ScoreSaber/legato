#nullable enable

using System;
using System.Diagnostics;

namespace Legato.Gameplay.Movement {
    public static class MovementDataEvents {
        public static event MovementDataEventHandler? Initializing;

        internal static void RaiseInitializing(
            ref float noteJumpMovementSpeed,
            ref BeatmapObjectSpawnMovementData.NoteJumpValueType noteJumpValueType,
            ref float noteJumpValue) {
            Delegate[]? subscribers = Initializing?.GetInvocationList();
            if (subscribers == null) {
                return;
            }

            foreach (MovementDataEventHandler subscriber in subscribers) {
                try {
                    subscriber(ref noteJumpMovementSpeed, ref noteJumpValueType, ref noteJumpValue);
                } catch (System.Exception exception) {
                    Debug.WriteLine(exception);
                }
            }
        }
    }
}
