#nullable enable

public interface IBeatmapLevelPack : IAnnotatedBeatmapLevelCollection
{
    string packID { get; }
    string packName { get; }
    string shortPackName { get; }
}
