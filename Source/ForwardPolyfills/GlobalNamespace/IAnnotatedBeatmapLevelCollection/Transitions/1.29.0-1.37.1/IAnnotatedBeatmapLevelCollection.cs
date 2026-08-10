#nullable enable
using UnityEngine;

public interface IAnnotatedBeatmapLevelCollection
{
    string collectionName { get; }
    Sprite coverImage { get; }
    Sprite smallCoverImage { get; }
    IBeatmapLevelCollection beatmapLevelCollection { get; }
}
