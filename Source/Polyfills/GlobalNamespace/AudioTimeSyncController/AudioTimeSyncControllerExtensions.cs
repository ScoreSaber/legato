#nullable enable

namespace Legato {
    internal static class AudioTimeSyncControllerExtensions {
        internal static bool IsPlaying(this AudioTimeSyncController controller) {
#if BEAT_SABER_1_29_0 || BEAT_SABER_1_37_1 || BEAT_SABER_1_38_0 || BEAT_SABER_1_40_0
            return controller.state == AudioTimeSyncController.State.Playing;
#else
            return controller.state == IAudioTimeSource.State.Playing;
#endif
        }

        internal static bool IsPaused(this AudioTimeSyncController controller) {
#if BEAT_SABER_1_29_0 || BEAT_SABER_1_37_1 || BEAT_SABER_1_38_0 || BEAT_SABER_1_40_0
            return controller.state == AudioTimeSyncController.State.Paused;
#else
            return controller.state == IAudioTimeSource.State.Paused;
#endif
        }
    }
}
