#nullable enable

using SiraUtil.Web;
using System;
using System.Diagnostics;
using System.Net.Http;

namespace Legato {
    internal static class IHttpResponseExtensions {
        internal static void CopyHeadersTo(this IHttpResponse source, HttpResponseMessage target) {
#if !BEAT_SABER_1_29_0
            if (source.Headers == null) {
                return;
            }

            foreach (var header in source.Headers) {
                if (string.IsNullOrEmpty(header.Key) || header.Value == null) {
                    continue;
                }

                try {
                    if (!target.Headers.TryAddWithoutValidation(header.Key, header.Value)) {
                        target.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
                    }
                } catch (Exception ex) {
                    Debug.WriteLine($"Skipped invalid HTTP response header {header.Key}: {ex.Message}");
                }
            }
#endif
        }
    }
}
