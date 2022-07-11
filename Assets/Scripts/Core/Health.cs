using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] Vector2 deathFling = new Vector2(10f, 10f);
    [SerializeField] Vector2 hurtFling = new Vector2(4f, 4f);
    Rigidbody2D enemyBody;

    private void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if(damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            damageDealer.Hit();
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        enemyBody.velocity = hurtFling;
        if(health <= 0)
        {
            enemyBody.velocity = deathFling;
            Destroy(gameObject);
        }
      
    }
}
