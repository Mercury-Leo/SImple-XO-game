using System;
using System.Collections;
using System.Threading;
using UnityEngine;

namespace Core.Extensions.Coroutine {
    /// <summary>
    /// Based on <see cref="https://forum.unity.com/threads/startcoroutine-add-overload-with-cancellationtoken.1006379/"/>
    /// </summary>
    public static class MonoBehaviourExtensions {
        private static IEnumerator CoroutineCancel(IEnumerator routine, CancellationToken token) {
            if (routine is null)
                throw new ArgumentException(nameof(routine));

            if (token == CancellationToken.None)
                throw new ArgumentException(nameof(token));

            while (!token.IsCancellationRequested && routine.MoveNext())
                yield return routine.Current;
        }

        public static UnityEngine.Coroutine StartCoroutine(this MonoBehaviour monoBehaviour, IEnumerator routine,
            CancellationToken token) {
            return monoBehaviour.StartCoroutine(CoroutineCancel(routine, token));
        }
    }
}