#nullable enable

namespace Legato {
    internal static class BeatmapDifficultyExtensions {
        internal static float NoteJumpMovementSpeed(this BeatmapDifficulty difficulty, float noteJumpMovementSpeed, bool useFastNotes) =>
            noteJumpMovementSpeed > 0f ? noteJumpMovementSpeed : difficulty.NoteJumpMovementSpeed();
    }
}
