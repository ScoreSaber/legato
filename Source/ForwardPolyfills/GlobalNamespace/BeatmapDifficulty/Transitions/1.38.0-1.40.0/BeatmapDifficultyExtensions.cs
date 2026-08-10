#nullable enable

namespace Legato {
    internal static class BeatmapDifficultyForwardPolyfills {
        internal static float NoteJumpMovementSpeed(this BeatmapDifficulty difficulty) => global::BeatmapDifficultyMethods.DefaultNoteJumpMovementSpeed(difficulty);
    }
}
