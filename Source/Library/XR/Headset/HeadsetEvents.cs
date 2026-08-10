#nullable enable

using System;
using System.Diagnostics;

namespace Legato.XR.Headset {
    public static class HeadsetEvents {
        public static event EventHandler<HeadsetUnmountedEventArgs>? HeadsetUnmounted;

        internal static bool RaiseHeadsetUnmounted() {
            var eventArgs = new HeadsetUnmountedEventArgs();
            Delegate[]? subscribers = HeadsetUnmounted?.GetInvocationList();
            if (subscribers == null) {
                return true;
            }

            foreach (EventHandler<HeadsetUnmountedEventArgs> subscriber in subscribers) {
                try {
                    subscriber(null, eventArgs);
                } catch (Exception exception) {
                    Debug.WriteLine(exception);
                }
            }
            return !eventArgs.SuppressGameHandling;
        }
    }
}
