#nullable enable

public class CustomPreviewBeatmapLevel : IPreviewBeatmapLevel
{
    public string levelID { get; } = string.Empty;
    public string songName { get; } = string.Empty;
    public string songSubName { get; } = string.Empty;
    public string songAuthorName { get; } = string.Empty;
    public string levelAuthorName { get; } = string.Empty;
    public float beatsPerMinute { get; }
    public float songTimeOffset { get; }
    public float previewStartTime { get; }
    public float previewDuration { get; }
    public float songDuration { get; }
}
