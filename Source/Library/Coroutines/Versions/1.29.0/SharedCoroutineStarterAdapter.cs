#nullable enable

using Legato;
using System.Collections;
using UnityEngine;

namespace Legato.Coroutines {
    // unity 2019 cannot instantiate a mod MonoBehaviour here, so use the game's runner
    public sealed class SharedCoroutineStarterAdapter : ICoroutineStarter {
        public Coroutine StartCoroutine(IEnumerator routine) => SharedCoroutineStarter.instance.StartCoroutine(routine);
    }
}
