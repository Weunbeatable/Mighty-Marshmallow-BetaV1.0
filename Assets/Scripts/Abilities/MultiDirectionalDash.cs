using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MultiDirectionalDash : MonoBehaviour
{
    Rigidbody2D playerBody;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int directon;
    Vector2 moveInput;
    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (directon == 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.LeftArrow))
            {
                directon = 1;
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.RightArrow))
            {
                directon = 2;
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.UpArrow))
            {
                directon = 3;
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.DownArrow))
            {
                directon = 4;
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                directon = 0;
                dashTime = startDashTime;
            }
            else
            {
                dashTime -= Time.deltaTime;

                if (directon == 1)
                {
                    playerBody.velocity = Vector2.left * dashSpeed;
                }
                else if (directon == 2)
                {
                    playerBody.velocity = Vector2.right * dashSpeed;
                }
                else if (directon == 3)
                {
                    playerBody.velocity = Vector2.up * dashSpeed;
                }
                else if (directon == 4)
                {
                    playerBody.velocity = Vector2.down * dashSpeed;
                }
            }
        }
    }


   
}
