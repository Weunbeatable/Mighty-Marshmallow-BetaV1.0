using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMM.core;
using TMM.Control;
public class Health : MonoBehaviour
{
    [SerializeField] float healthPoints = 100f;
    [SerializeField] Vector2 deathFling = new Vector2(10f, 10f);
    [SerializeField] Vector2 hurtFling = new Vector2(4f, 4f);
    Rigidbody2D thisBody;

    bool isdead = false;
    private void Start()
    {
        thisBody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            //damageDealer.GetDamage();
        }
    }

    public void TakeDamage(int damage)
    {
        
        healthPoints = Mathf.Max(healthPoints - damage, 0);
       // print(healthPoints);
        HurtAnim();
        if (healthPoints <= 0)
        {
            if (this.gameObject.tag == "Hero")
{
                this.gameObject.GetComponent<PlayerMover>();
            }
            else {
                die();
                StartCoroutine(ProcessDeath());
            }
        }

    }
    public IEnumerator ProcessDeath()
    {
      
            yield return new WaitForSecondsRealtime(1f);
            //thisBody.velocity = deathFling;
            Destroy(gameObject);  
    }

    void die()
    {
       if (isdead) { return; }
       isdead = true;
       GetComponent<Animator>().SetTrigger("die");
      // GetComponent<ActionScheduler>().CancelCurrentACtion();
    }
    void HurtAnim()
    {
        AnimatorStateInfo currentInfo = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        if (currentInfo.normalizedTime < 1f)
        {
            GetComponent<Animator>().SetTrigger("Hurt");
            thisBody.velocity = hurtFling;
        }
    }
    public float HealthValue()
    {
        return healthPoints;
    }

    public bool IsDead()
    {
        return isdead;
    }
}

