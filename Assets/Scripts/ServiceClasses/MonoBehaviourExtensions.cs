﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonoBehaviourExtensions
{
    public static void DelayedAction(this MonoBehaviour monoBehaviour, float time, EmptyAction action)
    {
        monoBehaviour.StartCoroutine(DelayedActionRoutine(time, action));
    }

    private static IEnumerator DelayedActionRoutine(float time, EmptyAction action)
    {
        yield return new WaitForSeconds(time);
        action?.Invoke();
    }

    public static void SmoothMoveTo(this MonoBehaviour monoBehaviour, Vector3 targetPos, float dampTime = .15f)
    {
        //it needs to stop previous move routine if it works
        monoBehaviour.StartCoroutine(MoveRoutine(monoBehaviour.transform, targetPos, null, dampTime));
    }

    public static void MoveWithAction(this MonoBehaviour monoBehaviour,
        Vector3 targetPos, EmptyAction postAction, float dampTime = .15f)
    {
        monoBehaviour.StartCoroutine(MoveRoutine(monoBehaviour.transform, targetPos, postAction, dampTime));
    }

    public static IEnumerator MoveRoutine(Transform movingObj,
        Vector3 targetPos, EmptyAction postAction = null, float dampTime = .15f)
    {
        Vector3 speed = Vector3.zero;
        while (Vector3.SqrMagnitude(movingObj.position - targetPos) > .001f)
        {
            movingObj.position = Vector3.SmoothDamp(movingObj.position, targetPos, ref speed, dampTime);
            yield return new WaitForEndOfFrame();
        }
        postAction?.Invoke();
    }
}
