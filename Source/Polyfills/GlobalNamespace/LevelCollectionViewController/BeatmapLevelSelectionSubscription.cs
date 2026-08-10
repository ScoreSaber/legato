#nullable enable

using System;

namespace Legato {
    internal static class LevelCollectionBeatmapLevelSelectionExtensions {
        internal static IDisposable SubscribeToLevelSelection(this LevelCollectionViewController controller, Action<BeatmapLevelAdapter?> callback) =>
            new LevelSelectionSubscription(controller, callback);

        internal static IDisposable SubscribeToPreviewLevelSelection(this LevelCollectionViewController controller, Action<IPreviewBeatmapLevel?> callback) =>
            new PreviewLevelSelectionSubscription(controller, callback);
    }

    internal sealed class LevelSelectionSubscription : IDisposable {
        private readonly LevelCollectionViewController _controller;
        private readonly Action<BeatmapLevelAdapter?> _callback;

#if BEAT_SABER_1_29_0
        private readonly Action<LevelCollectionViewController, IPreviewBeatmapLevel> _handler;
#else
        private readonly Action<LevelCollectionViewController, BeatmapLevel> _handler;
#endif

        internal LevelSelectionSubscription(LevelCollectionViewController controller, Action<BeatmapLevelAdapter?> callback) {
            _controller = controller;
            _callback = callback;
            _handler = (_, level) => _callback(level == null ? null : new BeatmapLevelAdapter(level));
            _controller.didSelectLevelEvent += _handler;
        }

        public void Dispose() {
            _controller.didSelectLevelEvent -= _handler;
        }
    }

    internal sealed class PreviewLevelSelectionSubscription : IDisposable {
        private readonly IDisposable _subscription;

        internal PreviewLevelSelectionSubscription(LevelCollectionViewController controller, Action<IPreviewBeatmapLevel?> callback) {
            _subscription = new LevelSelectionSubscription(controller, level => callback(level));
        }

        public void Dispose() {
            _subscription.Dispose();
        }
    }
}
