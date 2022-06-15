using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] float RunSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float ClimbSpeed = 5f;
    [SerializeField] Vector2 deathFling = new Vector2(10f, 10f);
    [SerializeField] GameObject generatedPlayerProjectile;
    [SerializeField] Transform swordSlashTip;

    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;



    private void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }
    Vector2 moveInput;
    Rigidbody2D playerBody;
    Animator playerAnimations;
    CapsuleCollider2D playerCollider;
    BoxCollider2D myFeetCollider;


    float startingGravity;
    bool isAlive = true;
  

    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerAnimations = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        startingGravity = playerBody.gravityScale;
        isAlive = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }
        Run();
        flipSprite();
        onClimbladder();
        playerIsFalling();
        PlayerIdleAnim();
        Die();

    }





    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();


    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        bool PlayerhasVerticalSpeed = Mathf.Abs(playerBody.velocity.y) > Mathf.Epsilon;
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        {
            if (value.isPressed)
            {
                playerBody.velocity += new Vector2(0f, jumpSpeed);
                playerAnimations.SetBool("isJumping", PlayerhasVerticalSpeed);
               
            }

            /*playerAnimations.SetBool("isIdle", false);
            playerAnimations.SetBool("isFalling", false);*/

        }


    }

    void OnFire(InputValue value)
    {
        if (!isAlive) { return; }
        playerAnimations.SetTrigger("Attack");
        Instantiate(generatedPlayerProjectile, swordSlashTip.position, Quaternion.identity);
    }
    void playerIsFalling()
    {
        if (!isAlive) { return; }
        bool playerIsFalling = playerBody.velocity.y < 0f;
        if ( playerIsFalling)
        {
            playerAnimations.SetBool("isFalling", true);


        }
       


    }


    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * RunSpeed, playerBody.velocity.y);
        playerBody.velocity = playerVelocity;

        bool PlayerhasHorizontalSpeed = Mathf.Abs(playerBody.velocity.x) > Mathf.Epsilon;

        playerAnimations.SetBool("isRunning", PlayerhasHorizontalSpeed);


    }

    private void flipSprite()
    {

        bool PlayerhasHorizontalSpeed = Mathf.Abs(playerBody.velocity.x) > Mathf.Epsilon;

        if (PlayerhasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerBody.velocity.x), 1f);
        }
    }

    void PlayerIdleAnim()
    {
        Vector2 playerVelocity = new Vector2(playerBody.velocity.x, playerBody.velocity.y);
        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
            myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climibing")) 
            && playerVelocity.x == 0f && playerVelocity.y < Mathf.Epsilon)
        {
            playerAnimations.SetBool("isIdle", true);
            playerAnimations.SetBool("isJumping", false);
            playerAnimations.SetBool("isFalling", false);
        }
    }
    void onClimbladder()
    {
        

        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climibing"))) 
        {
            playerBody.gravityScale = startingGravity;
            playerAnimations.SetBool("isIdle", false);
            return;
        }

            Vector2 climbVelocity = new Vector2(playerBody.velocity.x, moveInput.y * ClimbSpeed);
            playerBody.velocity = climbVelocity;
            playerBody.gravityScale = 0f;

            bool PlayerhasVerticalSpeed = Mathf.Abs(playerBody.velocity.y) > Mathf.Epsilon;
            playerAnimations.SetBool("isClimbing", PlayerhasVerticalSpeed);
       /* if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climibing")) && PlayerhasVerticalSpeed)
        {
            playerAnimations.SetBool("isClimbing", !PlayerhasVerticalSpeed);
        }*/
        }

 

    private void Die()
    {
        if (playerBody.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            isAlive = false;          
            playerAnimations.SetTrigger("isDead");
            playerBody.velocity = deathFling;
           
            ShakeCamera();
            applyCameraShake = true;
           StartCoroutine(FindObjectOfType<GameSession>().ProcessPlayerDeath());

        }
    }

    private void ShakeCamera()
    {
        if(cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
           
        }
    }
}
