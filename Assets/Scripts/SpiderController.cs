using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
    public float speed = 0;
    public List<Transform> waypoints;

    private int waypointsIndex;
    private float range;

    // Start is called before the first frame update
    void Start()
    {
        waypointsIndex = 0;
        range = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    void Move()
    {
        transform.LookAt(waypoints[waypointsIndex]);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, waypoints[waypointsIndex].position) < range)
        {
            waypointsIndex++;

            if (waypointsIndex >= waypoints.Count)
            {
                waypointsIndex = 0;
            }

        }

    }
}
