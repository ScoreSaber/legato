#nullable enable

namespace System {
    internal static class StringForwardPolyfills {
        internal static bool EndsWith(this string value, char ending) => value.Length > 0 && value[value.Length - 1] == ending;
    }
}
