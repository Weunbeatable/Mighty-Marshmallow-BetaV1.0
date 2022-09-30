using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMM.Control
{
    public class PatrolPath : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            const float waypointGizmoRadius = 0.2f;
            for (int i = 0; i < transform.childCount; i++)
            {
   
                int j = GetNextIndex(i);
                Gizmos.color = new Color(245, 240, 100, 1f);
                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
               
            }
        }

        public  int GetNextIndex(int i)
        {
            if( i + 1 == transform.childCount) // In order for us to grab the corner case of returning 
            {
                return 0;
            }
            return i + 1;
        }

        public Vector2 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }

        public int GetCurrentIndex(int i)
        {
            if (i + 1 == transform.childCount) // In order for us to grab the corner case of returning 
            {
                return 0;
            }
            return i;
        }
        public int GetIndexSize()
        {
            return transform.childCount;
        }
    }
}
