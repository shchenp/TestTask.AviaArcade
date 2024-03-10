using System;
using System.Collections;
using UnityEngine;

public static class DelayExtensions
{
    public static void DoAfter(this MonoBehaviour monoBehaviour, Action action, float delay)
    {
        monoBehaviour.StartCoroutine(DoAfterCoroutine(action, delay));
    }

    private static IEnumerator DoAfterCoroutine(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        action?.Invoke();
    }
}