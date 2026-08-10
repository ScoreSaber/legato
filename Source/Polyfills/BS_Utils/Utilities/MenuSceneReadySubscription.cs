#nullable enable

using System;
using System.Collections;
using UnityEngine;

namespace Legato {
    internal static class BSUtilsEvents {
        internal static IDisposable SubscribeToMenuSceneReady(Action callback) => new MenuSceneReadySubscription(callback);
    }

    internal sealed class MenuSceneReadySubscription : IDisposable {
        private readonly Action _callback;
        private MenuSceneReadyScheduler _scheduler;
        private bool _disposed;

        internal MenuSceneReadySubscription(Action callback) {
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
            BS_Utils.Utilities.BSEvents.menuSceneLoadedFresh += HandleMenuSceneLoaded;
        }

        private void HandleMenuSceneLoaded() {
            if (_disposed || _scheduler != null) {
                return;
            }

            var gameObject = new GameObject("Legato menu readiness");
            _scheduler = gameObject.AddComponent<MenuSceneReadyScheduler>();
            _scheduler.Begin(InvokeReady);
        }

        private void InvokeReady() {
            _scheduler = null;
            if (!_disposed) {
                _callback();
            }
        }

        public void Dispose() {
            if (_disposed) {
                return;
            }

            _disposed = true;
            BS_Utils.Utilities.BSEvents.menuSceneLoadedFresh -= HandleMenuSceneLoaded;
            if (_scheduler != null) {
                UnityEngine.Object.Destroy(_scheduler.gameObject);
                _scheduler = null;
            }
        }
    }

    internal sealed class MenuSceneReadyScheduler : MonoBehaviour {
        private Action _callback;

        internal void Begin(Action callback) {
            _callback = callback;
            StartCoroutine(WaitForReady());
        }

        private IEnumerator WaitForReady() {
            yield return new WaitForEndOfFrame();
            Action callback = _callback;
            UnityEngine.Object.Destroy(gameObject);
            callback();
        }
    }
}
