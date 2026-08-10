#nullable enable

#if BEAT_SABER_1_29_0 || BEAT_SABER_1_37_1 || BEAT_SABER_1_38_0 || BEAT_SABER_1_40_0 || BEAT_SABER_1_42_0
using Legato.XR.InputFocus;
using HarmonyLib;

namespace Legato {
    [HarmonyPatch(typeof(OculusVRHelper), nameof(OculusVRHelper.hasInputFocus), MethodType.Getter)]
    internal static class OculusVRHelperInputFocusPatch {
        private static void Postfix(ref bool __result) => __result = InputFocusEvents.RaiseInputFocusEvaluated(__result);
    }
}
#endif
