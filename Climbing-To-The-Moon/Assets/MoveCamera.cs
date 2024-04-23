using UnityEngine;

public class SmoothCameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.5f;
    private Vector3 velocity = Vector3.zero;
    public bool isMoving = false;

    void Update()
    {
        if (isMoving && target != null)
        {
            Vector3 targetPosition = target.position;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }

    public void StartCameraMovement(Transform newTarget)
    {
        target = newTarget;
        isMoving = true;
    }
}