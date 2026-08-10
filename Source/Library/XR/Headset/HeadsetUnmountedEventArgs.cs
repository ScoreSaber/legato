#nullable enable

using System;

namespace Legato.XR.Headset {
    public sealed class HeadsetUnmountedEventArgs : EventArgs {
        public bool SuppressGameHandling { get; set; }
    }
}
