#nullable enable

using System.Collections;
using UnityEngine;

namespace Legato {
    // BGLib added this service after 1.29
    public interface ICoroutineStarter {
        Coroutine StartCoroutine(IEnumerator routine);
    }
}
