#nullable enable

using SiraUtil.Web;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Legato {
    // old SiraUtil can't time out individual requests so we're faking it with a linked token
    internal static class IHttpServiceExtensions {
        internal static Task<IHttpResponse> SendAsync(this IHttpService httpService, HTTPMethod method, string url, int timeoutSeconds, string? body = null, IDictionary<string, string>? headers = null, IProgress<float>? downloadProgress = null, CancellationToken? cancellationToken = null) {
#if BEAT_SABER_1_29_0 || BEAT_SABER_1_37_1 || BEAT_SABER_1_38_0
            return SendWithLinkedTimeoutAsync(httpService, method, url, timeoutSeconds, body, headers, downloadProgress, cancellationToken);
#else
            return httpService.SendAsync(method, url, timeoutSeconds, body, headers, downloadProgress, cancellationToken);
#endif
        }

#if BEAT_SABER_1_29_0 || BEAT_SABER_1_37_1 || BEAT_SABER_1_38_0
        private static async Task<IHttpResponse> SendWithLinkedTimeoutAsync(IHttpService httpService, HTTPMethod method, string url, int timeoutSeconds, string? body, IDictionary<string, string>? headers, IProgress<float>? downloadProgress, CancellationToken? cancellationToken) {
            // old SiraUtil reports timeout cancels as code <= 0
            using (var timeoutSource = cancellationToken.HasValue
                ? CancellationTokenSource.CreateLinkedTokenSource(cancellationToken.Value)
                : new CancellationTokenSource()) {
                timeoutSource.CancelAfter(TimeSpan.FromSeconds(timeoutSeconds));
                return await httpService.SendAsync(method, url, body, headers, downloadProgress, timeoutSource.Token);
            }
        }
#endif
    }
}
