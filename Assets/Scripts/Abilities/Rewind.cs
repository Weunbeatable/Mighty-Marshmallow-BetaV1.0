using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewind : MonoBehaviour
{

    List<Vector2> listOfPosition;
    Vector2 guardingLocation;
    // Start is called before the first frame update
    void Start()
    {
        guardingLocation = this.transform.position;
        listOfPosition = new List<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            RecordedMovement();
            print("Recording");
        }
        if (Input.GetKeyDown(KeyCode.J)) 
        {
            print("Rewinding");
            replay();
        }
    }

    void RecordedMovement()
    {
        
            foreach (Vector2 x in listOfPosition)
            {
                listOfPosition.Add(guardingLocation);
            }
            print(transform.position);
        
    }

    void replay()
    {
        
            for (int i = listOfPosition.Count - 1; i >= 0; i--)
            {
                this.transform.position = Vector2.Lerp( transform.position, listOfPosition[i], 1f);
            }
        
    }

}
