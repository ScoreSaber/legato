#nullable enable

using System.Collections.Concurrent;

namespace System.Collections.Concurrent {
    internal static class ConcurrentDictionaryForwardPolyfills {
        internal static TValue GetValueOrDefault<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key) =>
            dictionary.TryGetValue(key, out TValue value) ? value : default!;
    }
}
