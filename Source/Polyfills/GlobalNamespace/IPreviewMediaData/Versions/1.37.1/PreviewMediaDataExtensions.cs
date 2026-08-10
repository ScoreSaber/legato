#nullable enable

using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Legato {
    internal static class PreviewMediaDataExtensions {
        internal static Task<Sprite> GetCoverSpriteAsync(this IPreviewMediaData previewMediaData) =>
            previewMediaData.GetCoverSpriteAsync(CancellationToken.None);
    }
}
