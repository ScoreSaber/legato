#nullable enable

public interface IPreviewBeatmapLevel
{
    string levelID { get; }
    string songName { get; }
    string songSubName { get; }
    string songAuthorName { get; }
    string levelAuthorName { get; }
    float beatsPerMinute { get; }
    float songTimeOffset { get; }
    float previewStartTime { get; }
    float previewDuration { get; }
    float songDuration { get; }
}
