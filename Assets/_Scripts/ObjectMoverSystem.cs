using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoverSystem : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private Vector3 movementArea = Vector3.one * 10;

    private Bounds areaBounds;

    void Start()
    {
        areaBounds = new Bounds(Vector3.zero, movementArea);
    }

    void Update()
    {
        foreach (ObjectMover obj in ObjectMover.instances)
        {
            if (!obj.direction.HasValue)
                obj.direction = GetRandomDirection();

            //Move obj
            obj.transform.position += obj.direction.Value * speed * Time.deltaTime;

            if (!areaBounds.Contains(obj.transform.position))   // If outside area
            {
                obj.transform.position -= 2 * obj.direction.Value * speed * Time.deltaTime;  // return to area
                obj.direction = GetRandomDirection();
            }
        }
    }

    public Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-0.5f, 0.5f) * movementArea.x, Random.Range(-0.5f, 0.5f) * movementArea.y, Random.Range(-0.5f, 0.5f) * movementArea.z);
    }

    public Vector3 GetRandomDirection()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(Vector3.zero, movementArea);
    }
}
