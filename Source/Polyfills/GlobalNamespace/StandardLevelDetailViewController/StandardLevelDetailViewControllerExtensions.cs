#nullable enable

using System;

namespace Legato {
    internal static class StandardLevelDetailViewControllerExtensions {
        internal static SelectedBeatmapAdapter? GetSelectedBeatmap(this StandardLevelDetailViewController controller) {
#if BEAT_SABER_1_29_0
            var beatmap = controller.selectedDifficultyBeatmap;
            return beatmap == null
                ? null
                : new SelectedBeatmapAdapter(
                    new BeatmapLevelAdapter(beatmap.level),
                    beatmap.noteJumpMovementSpeed,
                    beatmap.noteJumpStartBeatOffset);
#else
            var level = controller.beatmapLevel;
            if (level == null) {
                return null;
            }

            var beatmap = level.GetDifficultyBeatmapData(controller.beatmapKey);
            if (beatmap == null) {
                return null;
            }
            return new SelectedBeatmapAdapter(
                new BeatmapLevelAdapter(level),
                beatmap.noteJumpMovementSpeed,
                beatmap.noteJumpStartBeatOffset);
#endif
        }

        internal static IDisposable SubscribeToDifficultyChange(
            this StandardLevelDetailViewController controller,
            Action<SelectedBeatmapAdapter?> callback) =>
            new DifficultyChangeSubscription(controller, callback);
    }

    internal sealed class DifficultyChangeSubscription : IDisposable {
        private readonly StandardLevelDetailViewController _controller;
        private readonly Action<SelectedBeatmapAdapter?> _callback;

#if BEAT_SABER_1_29_0
        private readonly Action<StandardLevelDetailViewController, IDifficultyBeatmap> _handler;

        internal DifficultyChangeSubscription(StandardLevelDetailViewController controller, Action<SelectedBeatmapAdapter?> callback) {
            _controller = controller;
            _callback = callback;
            _handler = (_, beatmap) => _callback(beatmap == null
                ? null
                : new SelectedBeatmapAdapter(
                    new BeatmapLevelAdapter(beatmap.level),
                    beatmap.noteJumpMovementSpeed,
                    beatmap.noteJumpStartBeatOffset));
            _controller.didChangeDifficultyBeatmapEvent += _handler;
        }

        public void Dispose() {
            _controller.didChangeDifficultyBeatmapEvent -= _handler;
        }
#else
        private readonly Action<StandardLevelDetailViewController> _handler;

        internal DifficultyChangeSubscription(StandardLevelDetailViewController controller, Action<SelectedBeatmapAdapter?> callback) {
            _controller = controller;
            _callback = callback;
            _handler = view => _callback(view.GetSelectedBeatmap());
            _controller.didChangeDifficultyBeatmapEvent += _handler;
        }

        public void Dispose() {
            _controller.didChangeDifficultyBeatmapEvent -= _handler;
        }
#endif
    }
}
