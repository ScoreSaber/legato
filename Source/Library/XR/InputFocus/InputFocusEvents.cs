#nullable enable

using System;
using System.Diagnostics;

namespace Legato.XR.InputFocus {
    public static class InputFocusEvents {
        public static event InputFocusEventHandler? InputFocusEvaluated;

        internal static bool RaiseInputFocusEvaluated(bool hasInputFocus) {
            Delegate[]? subscribers = InputFocusEvaluated?.GetInvocationList();
            if (subscribers == null) {
                return hasInputFocus;
            }

            foreach (InputFocusEventHandler subscriber in subscribers) {
                try {
                    subscriber(ref hasInputFocus);
                } catch (System.Exception exception) {
                    Debug.WriteLine(exception);
                }
            }
            return hasInputFocus;
        }
    }
}
