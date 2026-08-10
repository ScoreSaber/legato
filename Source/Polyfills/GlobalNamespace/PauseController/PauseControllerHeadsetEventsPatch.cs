#nullable enable

using Legato.XR.Headset;
using HarmonyLib;

namespace Legato {
#if BEAT_SABER_1_29_0 || BEAT_SABER_1_37_1 || BEAT_SABER_1_38_0 || BEAT_SABER_1_40_0 || BEAT_SABER_1_42_0
    [HarmonyPatch(typeof(PauseController), nameof(PauseController.HandleHMDUnmounted))]
    internal static class PauseControllerHeadsetEventsPatch {
        private static bool Prefix() => HeadsetEvents.RaiseHeadsetUnmounted();
    }
#else
    [HarmonyPatch(typeof(PauseController), nameof(PauseController.HandleSystemStateChange))]
    internal static class PauseControllerHeadsetEventsPatch {
        private static bool Prefix(XRSystemEventType eventType) =>
            eventType != XRSystemEventType.HmdUnmounted || HeadsetEvents.RaiseHeadsetUnmounted();
    }
#endif
}
