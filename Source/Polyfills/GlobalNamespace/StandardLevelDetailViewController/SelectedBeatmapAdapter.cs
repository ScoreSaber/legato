#nullable enable

namespace Legato {
    internal sealed class SelectedBeatmapAdapter {
        internal readonly BeatmapLevelAdapter level;
        internal readonly float noteJumpMovementSpeed;
        internal readonly float noteJumpStartBeatOffset;

        internal SelectedBeatmapAdapter(
            BeatmapLevelAdapter level,
            float noteJumpMovementSpeed,
            float noteJumpStartBeatOffset) {
            this.level = level;
            this.noteJumpMovementSpeed = noteJumpMovementSpeed;
            this.noteJumpStartBeatOffset = noteJumpStartBeatOffset;
        }
    }
}
