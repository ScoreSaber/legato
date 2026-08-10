#nullable enable

namespace Legato.Beatmaps {
    internal readonly struct BeatmapDifficultyDetails {
        internal readonly string[] Mappers;
        internal readonly int? NotesCount;
        internal readonly int? CuttableObjectsCount;
        internal readonly int? ObstaclesCount;
        internal readonly int? BombsCount;
        internal readonly float NoteJumpMovementSpeed;
        internal readonly float NoteJumpStartBeatOffset;

        internal BeatmapDifficultyDetails(
            string[] mappers,
            int? notesCount,
            int? cuttableObjectsCount,
            int? obstaclesCount,
            int? bombsCount,
            float noteJumpMovementSpeed,
            float noteJumpStartBeatOffset) {

            Mappers = mappers;
            NotesCount = notesCount;
            CuttableObjectsCount = cuttableObjectsCount;
            ObstaclesCount = obstaclesCount;
            BombsCount = bombsCount;
            NoteJumpMovementSpeed = noteJumpMovementSpeed;
            NoteJumpStartBeatOffset = noteJumpStartBeatOffset;
        }
    }
}
