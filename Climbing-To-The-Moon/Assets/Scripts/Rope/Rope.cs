using System.Collections.Generic;
using UnityEngine;

public class LineRendererUpdater : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToFollow = new List<GameObject>();
    [SerializeField, Range(0f, 1f)] private float ropeWidth;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = ropeWidth;
        lineRenderer.endWidth = ropeWidth;
    }

    void Update()
    {
        lineRenderer.positionCount = objectsToFollow.Count;
        for (int i = 0; i < objectsToFollow.Count; i++)
        {
            if (objectsToFollow[i] != null)
            {
                lineRenderer.SetPosition(i, objectsToFollow[i].transform.position);
            }
        }
    }
}
