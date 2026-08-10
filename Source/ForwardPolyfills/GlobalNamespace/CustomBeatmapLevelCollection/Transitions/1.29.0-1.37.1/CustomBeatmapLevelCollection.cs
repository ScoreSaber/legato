#nullable enable
using System.Collections.Generic;

public class CustomBeatmapLevelCollection : IBeatmapLevelCollection
{
    protected readonly IReadOnlyList<CustomPreviewBeatmapLevel> _customPreviewBeatmapLevels;
    public IReadOnlyList<IPreviewBeatmapLevel> beatmapLevels => _customPreviewBeatmapLevels;

    public CustomBeatmapLevelCollection(CustomPreviewBeatmapLevel[] customPreviewBeatmapLevels)
    {
        _customPreviewBeatmapLevels = customPreviewBeatmapLevels;
    }
}
