#nullable enable

using System;
using UnityEngine.UI;

namespace Legato {
    internal static class StandardLevelDetailViewFavoriteToggleExtensions {
        internal static IDisposable SubscribeToFavoriteToggle(this StandardLevelDetailView view, Action<Toggle> callback) =>
            new FavoriteToggleSubscription(view, callback);
    }

    internal sealed class FavoriteToggleSubscription : IDisposable {
        private readonly StandardLevelDetailView _view;
        private readonly Action<Toggle> _callback;

#if BEAT_SABER_1_29_0
        private readonly Action<StandardLevelDetailView, Toggle> _handler;
#else
        private readonly Action<Toggle> _handler;
#endif

        internal FavoriteToggleSubscription(StandardLevelDetailView view, Action<Toggle> callback) {
            _view = view;
            _callback = callback;
#if BEAT_SABER_1_29_0
            _handler = (_, toggle) => _callback(toggle);
#else
            _handler = _callback;
#endif
            _view.didFavoriteToggleChangeEvent += _handler;
        }

        public void Dispose() {
            _view.didFavoriteToggleChangeEvent -= _handler;
        }
    }
}
