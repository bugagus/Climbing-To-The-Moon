using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using System;
using Cinemachine;

public enum EasingFunction
{
    Linear,
    InOutQuad,
    InfiniteAcceleration
}

public class MoveCamera : MonoBehaviour
{
    private GameObject objectToMove;
    private float moveDuration = 2f;
    [SerializeField] private EasingFunction easingType = EasingFunction.Linear;

    private bool moving = false;

    void Start()
    {
        objectToMove = this.gameObject;
    }

    public void MoveObjectToPoint(Vector3 targetPosition, float duration)
    {
        moveDuration = duration;
        if (!moving && objectToMove != null)
        {
            moving = true;
            Debug.Log("ENTRO AQUI");
            Tween moveTween = objectToMove.transform.DOMove(targetPosition, moveDuration);

            switch (easingType)
            {
                case EasingFunction.Linear:
                    moveTween.SetEase(Ease.Linear);
                    break;
                case EasingFunction.InOutQuad:
                    moveTween.SetEase(Ease.InOutQuad);
                    break;
                case EasingFunction.InfiniteAcceleration:
                    moveTween.SetEase(EaseCustom);
                    break;
                default:
                    moveTween.SetEase(Ease.Linear);
                    break;
            }

            moveTween.OnComplete(() => OnMoveComplete());
        }
    }

    void OnMoveComplete()
    {
        moving = false;
    }

    private float EaseCustom(float time, float duration, float unusedOvershootOrAmplitude, float unusedPeriod)
    {
        return time * time * time / (duration * duration * duration);
    }
}