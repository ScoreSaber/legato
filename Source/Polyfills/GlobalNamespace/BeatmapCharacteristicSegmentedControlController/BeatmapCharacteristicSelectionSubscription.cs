#nullable enable

using System;

namespace Legato {
    internal static class BeatmapCharacteristicSelectionExtensions {
        internal static IDisposable SubscribeToCharacteristicSelection(this BeatmapCharacteristicSegmentedControlController controller, Action callback) =>
            new CharacteristicSelectionSubscription(controller, callback);
    }

    internal sealed class CharacteristicSelectionSubscription : IDisposable {
        private readonly BeatmapCharacteristicSegmentedControlController _controller;
        private readonly Action _callback;

        private readonly Action<BeatmapCharacteristicSegmentedControlController, BeatmapCharacteristicSO> _handler;

        internal CharacteristicSelectionSubscription(BeatmapCharacteristicSegmentedControlController controller, Action callback) {
            _controller = controller;
            _callback = callback;
            _handler = (_, _) => _callback();
            _controller.didSelectBeatmapCharacteristicEvent += _handler;
        }

        public void Dispose() {
            _controller.didSelectBeatmapCharacteristicEvent -= _handler;
        }
    }
}
