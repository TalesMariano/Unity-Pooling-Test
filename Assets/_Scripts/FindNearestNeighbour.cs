using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class FindNearestNeighbour : MonoBehaviour
{
    public static List<FindNearestNeighbour> instances = new List<FindNearestNeighbour>();

    private FindNearestNeighbour _nearestNeighbour; 
    public FindNearestNeighbour NearestNeighbour { set { _nearestNeighbour = value; UpdateLine(); } }
    private LineRenderer _lineRenderer;

    private void OnEnable()
    {
        if (!instances.Contains(this))
            instances.Add(this);
    }

    private void OnDisable()
    {
        if (instances.Contains(this))
            instances.Remove(this);
    }

    private void Awake()
    {
        SetupLineRenderer();
    }

    private void SetupLineRenderer()
    {
        _lineRenderer = GetComponent<LineRenderer>();//gameObject.AddComponent<LineRenderer>();
        _lineRenderer.startWidth = 0.05f;
        _lineRenderer.endWidth = 0.05f;
        _lineRenderer.positionCount = 0;
    }

    void UpdateLine()
    {
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, _nearestNeighbour.transform.position);
    }
}
