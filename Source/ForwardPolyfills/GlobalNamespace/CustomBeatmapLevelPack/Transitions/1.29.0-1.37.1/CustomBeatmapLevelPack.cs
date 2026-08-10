#nullable enable
using UnityEngine;

public class CustomBeatmapLevelPack : IBeatmapLevelPack
{
    public string packID { get; }
    public string packName { get; }
    public string shortPackName { get; }
    public string collectionName => shortPackName;
    public Sprite coverImage { get; }
    public Sprite smallCoverImage { get; }
    public IBeatmapLevelCollection beatmapLevelCollection { get; }
    public bool isPackAlwaysOwned => true;

    public CustomBeatmapLevelPack(string packID, string packName, string shortPackName, Sprite coverImage, Sprite smallCoverImage, CustomBeatmapLevelCollection beatmapLevelCollection)
    {
        this.packID = packID;
        this.packName = packName;
        this.shortPackName = shortPackName;
        this.coverImage = coverImage;
        this.smallCoverImage = smallCoverImage;
        this.beatmapLevelCollection = beatmapLevelCollection;
    }
}
