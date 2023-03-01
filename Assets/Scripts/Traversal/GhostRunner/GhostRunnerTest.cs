using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMM.Control;
public class GhostRunnerTest : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerMover playerShadow;
    public Rigidbody2D thisbody;
    Queue<Vector2> retraceSteps = new Queue<Vector2>();
    Queue<Vector2> testSteps = new Queue<Vector2>();
    float MoveTime = 2f;
    Vector2 nextStep;
    void Start()
    {
        playerShadow.GetComponent<PlayerMover>();
    }

    // Update is called once per frame
    void Update()
    {

        while (testSteps.Count < 300)
        {
            testSteps.Enqueue(playerShadow.transform.position);
            Debug.Log(testSteps.Count);
        }
        if (testSteps.Count >= 0)
        {
            nextStep = testSteps.Dequeue();
            this.transform.position = Vector2.MoveTowards(this.transform.position, nextStep, .2f);
        }
        // runItBack();

    }
    public void runItBack()
    {
        Vector2 initialPost, NextPos;
        initialPost = thisbody.transform.position;
        retraceSteps.Enqueue(playerShadow.LogJourney());
        Debug.Log(retraceSteps.Count);
        while(retraceSteps.Count > 0) { 
            NextPos = retraceSteps.Dequeue();
            Vector2.MoveTowards(this.transform.position, NextPos, MoveTime);
            //initialPost = NextPos;
           

        }
    }
}
