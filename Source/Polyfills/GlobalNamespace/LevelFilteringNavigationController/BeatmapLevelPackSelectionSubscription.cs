#nullable enable

using System;

namespace Legato {
    // Events changed shape with the concrete beatmap model. Normalise them once,
    // rather than making every mod branch on the target profile.
    internal sealed class BeatmapLevelPackSelectionSubscription : IDisposable {
        private readonly LevelFilteringNavigationController _controller;

#if BEAT_SABER_1_29_0
        private readonly Action<LevelFilteringNavigationController, IAnnotatedBeatmapLevelCollection, UnityEngine.GameObject, BeatmapCharacteristicSO> _handler;

        internal BeatmapLevelPackSelectionSubscription(LevelFilteringNavigationController controller, Action<BeatmapLevelPackAdapter?> callback) {
            _controller = controller;
            _handler = (_, pack, _, _) => callback(pack == null ? null : new BeatmapLevelPackAdapter(pack));
            _controller.didSelectAnnotatedBeatmapLevelCollectionEvent += _handler;
        }

        public void Dispose() {
            _controller.didSelectAnnotatedBeatmapLevelCollectionEvent -= _handler;
        }
#else
        private readonly Action<LevelFilteringNavigationController, BeatmapLevelPack, UnityEngine.GameObject, LevelSelectionOptions> _handler;

        internal BeatmapLevelPackSelectionSubscription(LevelFilteringNavigationController controller, Action<BeatmapLevelPackAdapter?> callback) {
            _controller = controller;
            _handler = (_, pack, _, _) => callback(pack == null ? null : new BeatmapLevelPackAdapter(pack));
            _controller.didSelectBeatmapLevelPackEvent += _handler;
        }

        public void Dispose() {
            _controller.didSelectBeatmapLevelPackEvent -= _handler;
        }
#endif
    }

    internal static partial class LevelFilteringNavigationControllerExtensions {
        internal static IDisposable SubscribeToLevelPackSelection(
            this LevelFilteringNavigationController controller,
            Action<BeatmapLevelPackAdapter?> callback) => new BeatmapLevelPackSelectionSubscription(controller, callback);

        internal static IDisposable SubscribeToAnnotatedLevelPackSelection(
            this LevelFilteringNavigationController controller,
            Action<IAnnotatedBeatmapLevelCollection?> callback) =>
            new BeatmapLevelPackSelectionSubscription(controller, pack => callback(pack));
    }
}
