using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [SerializeField] Vector2 movementVector = new Vector2(10f, 10f);

    //todo remove from inspector later
    [Range(0, 1)] //  the modifying movement factor
    [SerializeField]
    float movementFactor; //0 for not moved, 1 for fully moved. 
    [SerializeField] float period = 2f;

    Vector2 StartingPos;
   
    void Start()
    {
        StartingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon)
        {
            return;
        }
        float cycles = Time.time / period; // time.time is framerate Independent 

        const float tau = Mathf.PI * 2;
        float rawSinwave = Mathf.Sin(cycles * tau);
        movementFactor = rawSinwave / 2f + 0.5f;
        Vector2 offset = movementVector * movementFactor;
        transform.position = StartingPos + offset;
    }

   /* private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == PlayerBody)
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == PlayerBody)
        {
            collision.transform.SetParent(null);
        }
    }*/
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hero")
        {
            collision.gameObject.transform.SetParent(transform);
            Debug.Log("success");
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hero")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
