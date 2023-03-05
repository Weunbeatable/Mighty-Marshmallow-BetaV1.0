using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    // TODO: Add flexibility to allow for different projectile types under this script 

    [Tooltip("values associated with projectile")]
    [SerializeField] float HadoukenSpeed = 4f;
    [SerializeField] int HadoukenDamage = 15;
    public float zFacing = 200;
    

    [Tooltip("Player component refrence")]

    Rigidbody2D myRigidbody;
    

    [Tooltip("Combat Parameters")]
    GameObject player;




    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Hero");
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    void FixedUpdate()
    {
        Vector2 direction = player.transform.position - myRigidbody.transform.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        myRigidbody.angularVelocity = rotateAmount * zFacing;
        myRigidbody.velocity = transform.up * HadoukenSpeed;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hero")
        {
            Health healthAspect = collision.gameObject.GetComponent<Health>();
           // healthAspect.TakeDamage(HadoukenDamage);
            //print("I Hit you");
            DamageDealer damagedealt = GetComponent<DamageDealer>();
            damagedealt.GetDamage();
          
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void projectileDirection()
    {
     
    }
}
