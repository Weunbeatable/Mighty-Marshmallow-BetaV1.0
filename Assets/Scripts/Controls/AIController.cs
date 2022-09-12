using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMM.Combat;

namespace RPG.Control
{
    public class AIController:MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float engageDistace = 2f;
        Fighting fighter;
        GameObject player;

        Transform target;
        private void Start()
        {
            player = GameObject.FindWithTag("Hero");
            fighter = GetComponent<Fighting>();
            
        }
        private void Update()
        {
            //if(target == null) { return; }
            if (attackRangeofPlayer() && facingPlayer())
            {
                engage();
                activateFightState();
            }
         

        }

        private bool attackRangeofPlayer()
        {
            float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }

        private bool facingPlayer()
        {
            float dot = Vector2.Dot(transform.right, (player.transform.position - transform.position).normalized);
            float fov = 0.7f;
            return dot > fov;
        }

        private void engage()
        {
            float regularSpeed = engageDistace * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, regularSpeed);
        }
        public void activateFightState()
        {
            // Cast a ray straight down.
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right);

            // If it hits something...
            if (hitInfo)
            {
                print("It's time to attack");
            }
        }
    }
}
