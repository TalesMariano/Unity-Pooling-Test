using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public static List<ObjectMover> instances = new List<ObjectMover>();

    public Vector3? direction;

    private void OnEnable()
    {
        if (!instances.Contains(this))
        {
            instances.Add(this);
        }
    }

    private void OnDisable()
    {
        if (instances.Contains(this))
        {
            instances.Remove(this);
        }
    }
}