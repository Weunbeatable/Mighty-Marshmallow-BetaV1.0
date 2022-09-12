using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPathfinding : MonoBehaviour
{
    [SerializeField] WaveConfigSO waveconfig;
    List<Transform> waypoints;
    int waypointIndex = 0;
    Vector3 startingPosition;
    int startPos = 0;

    void Start()
    {
        waypoints = waveconfig.GetWaypoints();
        waypoints[waypointIndex].position = transform.position;
        startingPosition = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        followPath();
       // reversePath();
    }
    void followPath()
    {
        if (waypointIndex < waypoints.Count)
        {
            Vector3 targetposition = waypoints[startPos].position;
            float delta = waveconfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetposition, delta);
            if (transform.position == targetposition)
            {
                waypointIndex++;

            }
            else
            {
                reversePath();
            }
        }
    }
        void reversePath()
        {
            if (waypointIndex >= waypoints.Count)
            {
                Vector3 targetposition = waypoints[0].position;
                float delta = waveconfig.GetMoveSpeed() * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, targetposition, delta);
                if (transform.position == targetposition)
                {
                    waypointIndex--;
                    Debug.Log(waypointIndex);
                }
            }
        }
    
}
