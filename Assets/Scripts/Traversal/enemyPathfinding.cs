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
        if (waypointIndex < waypoints.Count) // Have we reached the last patrol point.
        {
            Vector3 targetposition = waypoints[startPos].position; // Where are we trying to go to. 
            float delta = waveconfig.GetMoveSpeed() * Time.deltaTime; // how fast do you want to move to this position
            transform.position = Vector2.MoveTowards(transform.position, targetposition, delta); // 3 cases to move, where to start from, where to go to, how fast you want to get there. 
            if (transform.position == targetposition) // Did you get to that desired spot 
            {
                waypointIndex++; // If you reach the desired point, get the new position to go to

            }
            else
            {
                reversePath(); // If you've reached the last patrol spot, turn around and go back
            }
        }
    }
        void reversePath() // Literally everything I just said in reverse. 
        {
            if (waypointIndex >= waypoints.Count)
            {
                Vector3 targetposition = waypoints[0].position;
                float delta = waveconfig.GetMoveSpeed() * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, targetposition, delta);
                if (transform.position == targetposition)
                {
                    waypointIndex--;
                    Debug.Log(waypointIndex); // as an added guide, debug the position in the array to get an understanding of where we are. 
                }
            }
        }
    
}
