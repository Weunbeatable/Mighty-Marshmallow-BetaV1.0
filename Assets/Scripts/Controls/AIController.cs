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
    {   [Tooltip("Patroll Behavior")] // Variables should be pre-declared to save time. 

        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float engageDistace = 2f;
        [SerializeField] float patrolSpeed = 2f;
        [SerializeField] float suspicionTime = 5f;
        float timeSinceLastSawPlayer = Mathf.Infinity;


        // Class refrences
        Health health;
        Fighting fighter;
        GameObject player;
        Transform target;
        private Rigidbody2D myRigidbody;
        public EnemyCombat combat;
        Vector2 targetPosition;
       
        // Assumption: - all patrol path code is already created, the logic is the only thing that needs to be explained. 

       
        
        [Tooltip("Waypoint Behavior")]
        [SerializeField] float waypointTolerance = 2f;
        [SerializeField] PatrolPath patrolPath;
        int currentWaypointIndex = 0;
        float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        float waypointDwellTime = 2f;


        //TODO: 
        //flip characters rotation depending on if at the end or start of waypoint cycle.

        private void Start()
        {
           
            player = GameObject.FindWithTag("Hero");
            myRigidbody = GetComponent<Rigidbody2D>();
            fighter = GetComponent<Fighting>();
            health = GetComponent<Health>();
               
        }
        private void Update()
        {
            targetPosition = this.transform.position;
            if (health.IsDead()) return;
            if (player == null) { player = GameObject.FindWithTag("Hero"); }
                if (attackRangeofPlayer() && facingPlayer())
                {
                    EngageBehavior();
                    timeSinceLastSawPlayer = 0f;
                }

                else if (!attackRangeofPlayer() && timeSinceLastSawPlayer < suspicionTime)
                {
                    SuspicionBehavior();
                }
            
            else
            {
                // print("I'm going back home");
                PatrolBehavior();
            }

            updateTimers();
        }

        private void DirectionToFace()// c ourtesy of unity forums
        {
            if (targetPosition.x - this.transform.position.x < 0)
            {
               
                // this.transform.localScale =  new Vector2(-1, 1);
                 this.transform.rotation = new Quaternion(0, 0, 0, 0); // changing rotating  so I don't have to modify facing player bool
            }
            else if (targetPosition.x - this.transform.position.x > 0)
                {
                //  Vector2 flipSIdes = this.transform.localScale;
               // this.transform.localScale = new Vector2(1, 1);
                this.transform.rotation = new Quaternion(0, -180, 0, 0);
            }
           
        }

        private void updateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime; // will update by the amount the frame took on every frame. if we don't reset it, resets when we see player.
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void SuspicionBehavior()
        {
          //  print("Hmm kinda sus bro");
            Vector2 holdPosition = transform.position;
            transform.position = holdPosition;
        }

        private void PatrolBehavior()
        {
            //Vector2 nextPosition = guardingLocation;

            if (patrolPath != null)
            {
                if (atWaypoint())
                {                  
                    timeSinceArrivedAtWaypoint = 0; // reset when arrived at next waypoint                
                    CycleWaypoint();
                }
                if (timeSinceArrivedAtWaypoint > waypointDwellTime)
                {
                    MoveToNextPoint();
                    GetComponent<Animator>().SetTrigger("walk");
                    //Debug.Log("The next pos is " + nextPosition);
                    //if (currentWaypointIndex == 0 || currentWaypointIndex)
                }

            }
             
            
          //  this.transform.position = Vector2.Lerp(transform.position, guardingLocation, regularSpeed); // have to reset the transform position to going to the stored guarding location otherwise the guarding location will just be the current location.
           
          
        }

        private void MoveToNextPoint()
        {
            Vector2 nextPosition = GetCurrentWaypoint();
            float regularSpeed = patrolSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, nextPosition, regularSpeed);
            DirectionToFace();
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
            GetComponent<Animator>().SetTrigger("idle"); // not using animator in example... probably ....
            // Debug.Log("waypointdistance is" + distanceToWaypoint);
            return distanceToWaypoint < waypointTolerance;
        }

        private bool attackRangeofPlayer()
        {
            if (player != null)
            {
                float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position); // vector2.square Magnitude is faster but for such a arbitrarily small task it doesn't really matter in this example. 

                return distanceToPlayer < chaseDistance;
            }
            else return false;
        }

        private bool facingPlayer()
        {
            float dot = Vector2.Dot(transform.right, (player.transform.position - transform.position).normalized); // Call vector 3 for dot product 
            float fov = 0.7f; // Field of View of AI, may make it adjustable for enemy types at some point.
            return dot > fov;
        }

        private void EngageBehavior()
        {
            if (CanAttack() == false)
            {
                AttackBehavior();
            }
            else
            {
                float regularSpeed = engageDistace * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, regularSpeed);
                GetComponent<Animator>().SetTrigger("walk");
            }
        }
        public void AttackBehavior()
        {
            // Cast a ray straight down.
           /* RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right);

            // If it hits something...
            if (hitInfo.transform.gameObject.tag == "Hero")
            {
                print("It's time to attack");
                
            }*/
            combat.CombatAttackTypeCheck();
        }
         
       private bool CanAttack()
        {
            bool callAttack;
            float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position); // call V3 here instead, 
            if ( distanceToPlayer < engageDistace)
            {
                callAttack = true;
            }
            else
                callAttack = false;
            return callAttack;
        }
        // Called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(240, 224, 161, 0.9f);
            Gizmos.DrawWireSphere(transform.position, 1);
        }

    }
}
