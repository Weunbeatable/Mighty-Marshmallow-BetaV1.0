using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHadouken : MonoBehaviour
{
    [SerializeField] float HadoukenSpeed = 1f;
    Rigidbody2D myRigidbody;
    PlayerMover myPlayer;
    float xSpeed;
    float xFacing;
    float acceleration = 0.5f;
    float accelerationOpposite = -0.5f;
   
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myPlayer = FindObjectOfType<PlayerMover>();
        xSpeed = myPlayer.transform.localScale.x * HadoukenSpeed;
        transform.localScale = new Vector2((Mathf.Sign(xSpeed)) * transform.localScale.x, transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        
        myRigidbody.velocity = new Vector2(xSpeed, 0f);
        if (xSpeed > 0)
        {
            xSpeed += acceleration;
            if (xSpeed > 20f)
            {
                xSpeed = 20f * Time.deltaTime;
            }
        }
        if(xSpeed < 0)
        {
            xSpeed += accelerationOpposite;
            if (xSpeed < -20f)
            {
                xSpeed = -20f * Time.deltaTime;
            }
        }
       
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
