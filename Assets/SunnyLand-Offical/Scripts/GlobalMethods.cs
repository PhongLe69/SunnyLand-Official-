using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMethods
{
    public static IEnumerator DelayedInvoke(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);

        action();
    }
}
