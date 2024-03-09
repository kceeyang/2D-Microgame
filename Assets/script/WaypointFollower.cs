using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{

    [SerializeField] private GameObject[] waypoints;
    private int currtWayptIndex = 0;

    [SerializeField] private float speed = 2f;

    private void Update()
    {
        if (Vector2.Distance(waypoints[currtWayptIndex].transform.position, transform.position) < .1f)
        {
            currtWayptIndex ++;
            if (currtWayptIndex >= waypoints.Length)
            {
                currtWayptIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currtWayptIndex].transform.position, Time.deltaTime * speed);
    }
}
