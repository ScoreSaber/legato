#nullable enable

using SiraUtil.Zenject;

namespace Legato {
    internal static class ZenjectorExtensions {
#pragma warning disable CS0618
        internal static void ExposeFromContract<T>(this Zenjector zenjector, string contractName) {
            zenjector.Expose<T>(contractName);
        }
#pragma warning restore CS0618
    }
}
