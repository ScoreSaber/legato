#nullable enable

public class BeatmapLevelPackCollectionSO : PersistentScriptableObject, IBeatmapLevelPackCollection
{
    protected IBeatmapLevelPack[] _allBeatmapLevelPacks = System.Array.Empty<IBeatmapLevelPack>();
    public IBeatmapLevelPack[] beatmapLevelPacks { get => _allBeatmapLevelPacks; set => _allBeatmapLevelPacks = value; }
}
