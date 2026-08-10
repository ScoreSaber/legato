#nullable enable

using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Legato {
    internal class PreviewMediaData {
        private readonly IPreviewBeatmapLevel _level;

        internal PreviewMediaData(IPreviewBeatmapLevel level) {
            _level = level;
        }

        public Task<Sprite> GetCoverSpriteAsync() => _level.GetCoverImageAsync(CancellationToken.None);
    }
}
