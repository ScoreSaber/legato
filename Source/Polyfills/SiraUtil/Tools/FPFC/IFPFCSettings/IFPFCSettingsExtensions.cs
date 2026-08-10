#nullable enable

using SiraUtil.Tools.FPFC;
using System;

namespace Legato {
    internal static class IFPFCSettingsExtensions {
#pragma warning disable CS0618
        internal static void AddChangedListener(this IFPFCSettings settings, Action<IFPFCSettings> handler) {
            settings.Changed += handler;
        }

        internal static void RemoveChangedListener(this IFPFCSettings settings, Action<IFPFCSettings> handler) {
            settings.Changed -= handler;
        }
#pragma warning restore CS0618
    }
}
