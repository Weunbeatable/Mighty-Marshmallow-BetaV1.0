using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMM.Combat;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidbody;
    bool isAlive;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        isAlive = true;
       
    }

    
    void Update()
    {
       // Health myHealth = GetComponent<Health>();
       // if (myHealth.HealthValue() < 0) { isAlive = false; }
        if (!isAlive) { return; } // base state for player living (this can be abstracted and really doesn't matter for the sake of the example.)
        myRigidbody.velocity = new Vector2 (moveSpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }


    private void FlipEnemyFacing() // needed for pathfinding but this will be re-evaluated for 3D space. 
    {
        if (!isAlive) { return; }
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
    }


// TODO:
// Add block to handle direction facing for 3D example. 
}
