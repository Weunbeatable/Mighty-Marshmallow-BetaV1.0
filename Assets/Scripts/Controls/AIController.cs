using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMM.Combat;
using TMM.core;
using TMM.Control;
using System;

namespace RPG.Control
{
    public class AIController:MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float engageDistace = 2f;
        [SerializeField] float patrolSpeed = 2f;
        [SerializeField] float suspicionTime = 5f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 2f;
        Health health;
        Fighting fighter;
        GameObject player;

        Transform target;
        private Rigidbody2D myRigidbody;

        Vector2 guardingLocation;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        int currentWaypointIndex = 0;

        //TODO: 
        //flip characters rotation depending on if at the end or start of waypoint cycle.

        private void Start()
        {
           
            player = GameObject.FindWithTag("Hero");
            myRigidbody = GetComponent<Rigidbody2D>();
            fighter = GetComponent<Fighting>();
            health = GetComponent<Health>();
            guardingLocation = this.transform.position;
            
        }
        private void Update()
        {
            if (health.IsDead())  return;

            if (attackRangeofPlayer() && facingPlayer())
            {
                EngageBehavior();
                timeSinceLastSawPlayer = 0f;
                AttackBehavior();
            }
            else if (!attackRangeofPlayer() && timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehavior();
            }
            else
            {
                print("I'm going back home");
                PatrolBehavior();               
            }

            timeSinceLastSawPlayer += Time.deltaTime; // will update by the amount the frame took on every frame. if we don't reset it, resets when we see player.

        }

        private void SuspicionBehavior()
        {
            print("Hmm kinda sus bro");
            Vector2 holdPosition = transform.position;
            transform.position = holdPosition;
        }

        private void PatrolBehavior()
        {
            //Vector2 nextPosition = guardingLocation;

            if(patrolPath != null)
            {
                if (atWaypoint())
                {
                    CycleWaypoint();
                }
               Vector2  nextPosition = GetCurrentWaypoint();
                float regularSpeed = patrolSpeed * Time.deltaTime;
                GetComponent<Animator>().SetTrigger("idle");
                transform.position = Vector2.Lerp(transform.position, nextPosition, regularSpeed);
                Debug.Log( "The next pos is " + nextPosition);
            }
             
            
          //  this.transform.position = Vector2.Lerp(transform.position, guardingLocation, regularSpeed); // have to reset the transform position to going to the stored guarding location otherwise the guarding location will just be the current location.
           
          
        }

        private Vector2 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private bool atWaypoint()
        {
            float distanceToWaypoint = Vector2.Distance(transform.position, GetCurrentWaypoint());
            Debug.Log("waypointdistance is" + distanceToWaypoint);
            return distanceToWaypoint < waypointTolerance;
        }

        private bool attackRangeofPlayer()
        {
            float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }

        private bool facingPlayer()
        {
            float dot = Vector2.Dot(transform.right, (player.transform.position - transform.position).normalized);
            float fov = 0.7f; // Field of View of AI, may make it adjustable for enemy types at some point.
            return dot > fov;
        }

        private void EngageBehavior()
        {
            float regularSpeed = engageDistace * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, regularSpeed);
            GetComponent<Animator>().SetTrigger("walk");
        }
        public void AttackBehavior()
        {
            // Cast a ray straight down.
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right);

            // If it hits something...
            if (hitInfo)
            {
                print("It's time to attack");
            }
        }
         
       
        // Called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(240, 224, 161, 0.9f);
            Gizmos.DrawWireSphere(transform.position, 1);
        }

    }
}