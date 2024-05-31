using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FindNearestBrain : MonoBehaviour
{
    KdTree<FindNearestNeighbour> squares;

    private void Start()
    {
        squares = new KdTree<FindNearestNeighbour>();
    }

    void Update()
    {
        squares.Clear();

        squares.AddAll(FindNearestNeighbour.instances);

        foreach (var item in squares)
        {
            var nearestOnj = squares.FindClosest(item.transform.position);
            
            if (nearestOnj == null)
                continue;

            item.NearestNeighbour = nearestOnj;
        }
    }
}
