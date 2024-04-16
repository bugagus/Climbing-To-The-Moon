using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollower : MonoBehaviour
{
    [SerializeField] private Transform objectToFollow;
    [SerializeField] private float YOffset, XOffset, ZOffset;

    void Update()
    {
        Vector3 followedPosition = objectToFollow.position;
        transform.position = new Vector3(followedPosition.x + XOffset, followedPosition.y + YOffset, followedPosition.z + ZOffset);
    }
}
