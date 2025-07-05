using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testKeyPress : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidbody2;
    [SerializeField] Vector2 uppies = new Vector2(0, 10f);
    [SerializeField] float charge = 10f;

    float cooldownTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        uppies.y = charge;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space) && cooldownTimer <= 0f)
        {
            charge += 120* Time.deltaTime; 
            if(charge > 450f)
            {
                charge = 450f;
            }
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            if (cooldownTimer > 0f)
            {

                return;
            }
            else
            {
                rigidbody2.AddForce(uppies, ForceMode2D.Impulse);
                Debug.Log("charge value" + charge);
                cooldownTimer = 1.5f;
            }
  
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0f, 0f, (45f * Time.deltaTime) * -1f);
        }

        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0f, 0f, 45f * Time.deltaTime);
        }

        if (cooldownTimer > 0f)
        {

            cooldownTimer -= Time.deltaTime;
        }
    }
}
