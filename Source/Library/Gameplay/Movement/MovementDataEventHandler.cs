#nullable enable

namespace Legato.Gameplay.Movement {
    public delegate void MovementDataEventHandler(
        ref float noteJumpMovementSpeed,
        ref BeatmapObjectSpawnMovementData.NoteJumpValueType noteJumpValueType,
        ref float noteJumpValue);
}
