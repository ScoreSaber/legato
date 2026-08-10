#nullable enable
using System.Collections.Generic;

public interface IBeatmapLevelCollection
{
    IReadOnlyList<IPreviewBeatmapLevel> beatmapLevels { get; }
}
