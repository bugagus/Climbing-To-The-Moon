using UnityEngine;
using DG.Tweening;
using System.Linq;
using System;

public enum EasingFunctionType
{
    Linear,
    InOutQuad,
    InfiniteAcceleration
}

public class MovingObject : MonoBehaviour
{
    [SerializeField] private Transform[] followPoints;
    [SerializeField] private float moveDuration = 2f;
    [SerializeField] private EasingFunctionType easingType = EasingFunctionType.Linear;
    [SerializeField] private LineRenderer lineRenderer;

    private bool moving = false;

    void Start()
    {
        if (lineRenderer != null)
        {
            Vector3[] points = new Vector3[followPoints.Length];
            for (int i = 0; i < followPoints.Length; i++)
            {
                points[i] = followPoints[i].position;
            }
            lineRenderer.positionCount = points.Length;
            lineRenderer.SetPositions(points);
        }
        transform.position = followPoints[0].position;
        MoveObject();
    }

    public void MoveToPoint(int pointIndex)
    {
        if (!moving)
        {
            moving = true;
            Vector3 targetPosition = followPoints[pointIndex].position;

            Tween moveTween = transform.DOMove(targetPosition, moveDuration);

            switch (easingType)
            {
                case EasingFunctionType.Linear:
                    moveTween.SetEase(Ease.Linear);
                    break;
                case EasingFunctionType.InOutQuad:
                    moveTween.SetEase(Ease.InOutQuad);
                    break;
                case EasingFunctionType.InfiniteAcceleration:
                    moveTween.SetEase(EaseCustom);
                    break;
                default:
                    moveTween.SetEase(Ease.Linear);
                    break;
            }

            if (pointIndex < (followPoints.Length - 1))
            {
                moveTween.OnComplete(() => MoveToPoint(pointIndex + 1));
            }
            else
            {
                moveTween.OnComplete(OnMoveComplete);
            }
        }
    }

    public void MoveObject()
    {
        MoveToPoint(1);
    }

    void OnMoveComplete()
    {
        moving = false;
        Array.Reverse(followPoints);
        MoveObject();
    }

    private float EaseCustom(float time, float duration, float unusedOvershootOrAmplitude, float unusedPeriod)
    {
        return time * time * time / (duration * duration);
    }
}