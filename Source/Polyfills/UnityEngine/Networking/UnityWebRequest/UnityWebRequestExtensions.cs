#nullable enable

using UnityEngine.Networking;

namespace Legato {
    internal static class UnityWebRequestExtensions {
        internal static bool IsConnectionError(this UnityWebRequest request) =>
#if BEAT_SABER_1_29_0
            request.isNetworkError;
#else
            request.result == UnityWebRequest.Result.ConnectionError;
#endif

        internal static bool IsProtocolError(this UnityWebRequest request) =>
#if BEAT_SABER_1_29_0
            request.isHttpError;
#else
            request.result == UnityWebRequest.Result.ProtocolError;
#endif
    }
}
