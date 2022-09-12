using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMM.Control;

namespace TMM.Combat
{
    public class Fighting : MonoBehaviour
    {
     
        // TODO: Add flexibility to allow for different projectile types under this script 

        [Tooltip("values associated with projectile")]
        [SerializeField] float HadoukenSpeed = 1f;
        [SerializeField] int HadoukenDamage = 15;
        float engageDistace = 2f;
        float xSpeed;
        float xFacing;
        float acceleration = 0.5f;
        float accelerationOpposite = -0.5f;

        [Tooltip("Player component refrence")]
      
        Rigidbody2D myRigidbody;
        PlayerMover myPlayer;

        [Tooltip("Combat Parameters")]
        GameObject player;
        Transform target;



        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindWithTag("Hero");
            myRigidbody = FindObjectOfType<Rigidbody2D>();
            myPlayer = FindObjectOfType<PlayerMover>();
            xSpeed = myPlayer.transform.localScale.x * HadoukenSpeed;
            transform.localScale = new Vector2((Mathf.Sign(xSpeed)) * transform.localScale.x, transform.localScale.y);
        }

        // Update is called once per frame
        void Update()
        {

            myRigidbody.velocity = new Vector2(xSpeed, 0f);
            projectileDirection();

        }


        void FixedUpdate()
        {
            
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Health healthAspect = collision.gameObject.GetComponent<Health>();
                healthAspect.TakeDamage(HadoukenDamage);
                print("I Hit you");
                /* DamageDealer damagedealt = GetComponent<DamageDealer>();
                 damagedealt.GetDamage();
                 Destroy(collision.gameObject);*/
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void projectileDirection()
        {
            if (xSpeed > 0)
            {
                xSpeed += acceleration;
                if (xSpeed > 20f)
                {
                    xSpeed = 20f * Time.deltaTime;
                }
            }
            if (xSpeed < 0)
            {
                xSpeed += accelerationOpposite;
                if (xSpeed < -20f)
                {
                    xSpeed = -20f * Time.deltaTime;
                }
            }
        }

        void EngagementLogic()
        {

        }

        

      
    }
}


