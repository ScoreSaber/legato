#nullable enable

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Legato
{
    internal static class SongCoreLoaderEvents
    {
        internal static IDisposable SubscribeToSongsLoaded(Action callback)
        {
            return new SongsLoadedSubscription(callback);
        }

        internal static async Task<bool> RefreshSongsAsync(this SongCore.Loader loader, bool fullRefresh, TimeSpan timeout, CancellationToken cancellationToken)
        {
            var loaded = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            using (SubscribeToSongsLoaded(() => loaded.TrySetResult(true)))
            {
                loader.RefreshSongs(fullRefresh);
                Task completed = await Task.WhenAny(loaded.Task, Task.Delay(timeout, cancellationToken));
                if (completed == loaded.Task)
                    return true;

                cancellationToken.ThrowIfCancellationRequested();
                return false;
            }
        }
    }

    internal sealed class SongsLoadedSubscription : IDisposable
    {
        private readonly Action _callback;

#if BEAT_SABER_1_29_0
        private readonly Action<SongCore.Loader, System.Collections.Concurrent.ConcurrentDictionary<string, CustomPreviewBeatmapLevel>> _handler;
#else
        private readonly Action<SongCore.Loader, System.Collections.Concurrent.ConcurrentDictionary<string, BeatmapLevel>> _handler;
#endif

        internal SongsLoadedSubscription(Action callback)
        {
            _callback = callback;
            _handler = (_, _) => _callback();
            SongCore.Loader.SongsLoadedEvent += _handler;
        }

        public void Dispose()
        {
            SongCore.Loader.SongsLoadedEvent -= _handler;
        }
    }
}
