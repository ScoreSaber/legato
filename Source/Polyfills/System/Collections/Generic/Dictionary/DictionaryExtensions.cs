#nullable enable

using System.Collections.Generic;

namespace System.Collections.Generic {
    internal static class DictionaryForwardPolyfills {
        internal static bool TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value) {
            if (dictionary.ContainsKey(key)) {
                return false;
            }

            dictionary.Add(key, value);
            return true;
        }
    }
}
