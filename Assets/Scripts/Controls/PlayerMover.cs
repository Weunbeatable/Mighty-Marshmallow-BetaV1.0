using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMM.Combat;
using TMM.Core;

namespace TMM.Control
{
    public class PlayerMover : MonoBehaviour
    {
        [Header("MovementSpeed")]
        [SerializeField] float RunSpeed = 5f;
        [SerializeField] float ClimbSpeed = 5f;
        [SerializeField] float dashBoostSpeed = 5f;
        [SerializeField] Vector2 deathFling = new Vector2(10f, 10f);
        [SerializeField] Vector2 hurtFling = new Vector2(4f, 4f);
        

        [Header("JumpTools")]
        public bool PlayerHasAirTime;
        [SerializeField] float fallMultiplier = 2.5f;
        [SerializeField] float lowJumpMultiplier = 2f;
        [SerializeField] float jumpSpeed = 5f;


        [Header("DashMechanic")]
        public ParticleSystem dust;
        public ParticleSystem FlashStep;
        [SerializeField] public GameObject generatedPlayerProjectile;
        [SerializeField] public Transform swordSlashTip;
        [SerializeField] bool applyCameraShake;

        [Header("AttackMechanics")]
        public float attackRate = 1f;
        float nextATtackTime = 0f;
        int attackCounter = 0;


        [Header("GeneralPlayerComponents")]
        CameraShake cameraShake;
        Vector2 DashForce;
        Vector2 moveInput;
        Rigidbody2D playerBody;
        Animator playerAnimations;
        CapsuleCollider2D playerCollider;
        BoxCollider2D myFeetCollider;


        // Player Dashing
        private bool canDash = true;
        private bool isDashing;
        private float dashingStrength = 10f;
        private float dashingTime = 0.2f;
        private float dashingCooldown = 1f;
        [SerializeField] TrailRenderer DashTrail;

        // placeholder for combat system
        DamageDealer damagedealt;
        Fighting currentProjectile;
        private void Awake()
        {
            cameraShake = Camera.main.GetComponent<CameraShake>();
        }



        float startingGravity;
        bool isAlive = true;
        bool jumpRequest;

        void Start()
        {
            playerBody = GetComponent<Rigidbody2D>();
            playerAnimations = GetComponent<Animator>();
            playerCollider = GetComponent<CapsuleCollider2D>();
            myFeetCollider = GetComponent<BoxCollider2D>();
            damagedealt = GetComponent<DamageDealer>();
            currentProjectile = GetComponent<Fighting>();
            startingGravity = playerBody.gravityScale;
            isAlive = true;
            InputValue value;
        
            }


        // Update is called once per frame
        void Update()
        {
            if (!isAlive) { return; }
            if (isDashing) { return; }
            Run();
            flipSprite();
            onClimbladder();
            playerIsFalling();
            playerIsJumping();
            PlayerIdleAnim();
            startDash();
            //lightDash();
            Die();

        }


        private void FixedUpdate()
        {
            bool PlayerhasVerticalSpeed = Mathf.Abs(playerBody.velocity.y) > 0f;
            if (jumpRequest)
            {
                {
                    //playerBody.velocity += new Vector2(0f, jumpSpeed);
                    playerBody.AddForce(new Vector2(0f, jumpSpeed), ForceMode2D.Impulse);
                    CreateDust();
                    jumpRequest = false;
                   
                }

            }

         /*   else if (!jumpRequest && PlayerhasVerticalSpeed)
            {
                playerBody.gravityScale = lowJumpMultiplier;
                // playerBody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
            else
            {
                playerBody.gravityScale = 1;
            }*/
        }


        void OnMove(InputValue value)
        {
            if (!isAlive) { return; }
            moveInput = value.Get<Vector2>();


        }

        void OnJump(InputValue value)
        {
            if (!isAlive) { return; }
            bool PlayerhasVerticalSpeed = Mathf.Abs(playerBody.velocity.y) > 0f;
            if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }


            if (value.isPressed)
            {
                jumpRequest = true;
         
            }

            /* if (Mathf.Abs(playerBody.velocity.y) > 0f && !value.isPressed )
            {
                playerBody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }*/

            // playerAnimations.SetBool("isFalling", false);*
        }

        //TO DO decouple fire from player mover into combat namespace/ combat class. 
        void OnFire(InputValue value)
        {
            if (!isAlive) { return; }
            if (Time.time > nextATtackTime)
            {
                if (value.isPressed)
                {

                    playerAnimations.SetTrigger("Attack");
                    Instantiate(generatedPlayerProjectile, swordSlashTip.position, Quaternion.identity);
                   // damagedealt.GetDamage();
                    nextATtackTime = Time.time + .7f / attackRate;
                }
            }
        }

        
        void playerIsFalling()
        {
            if (!isAlive) { return; }
            bool playerIsFalling = playerBody.velocity.y < 0f;
            if (playerIsFalling)
            {
                playerAnimations.SetBool("isFalling", true);
            }
            if (playerIsFalling && !myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climibing")))
            {
                playerBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
        }
        void playerIsJumping()
        {
            if (!isAlive) { return; }
            bool playerIsJumping = playerBody.velocity.y > 0f;
            if (playerIsJumping)
            {
                playerAnimations.SetBool("isJumping", true);
            }
        }


        void Run()
        {
            Vector2 playerVelocity = new Vector2(moveInput.x * RunSpeed, playerBody.velocity.y);
            playerBody.velocity = playerVelocity;

            bool PlayerhasHorizontalSpeed = Mathf.Abs(playerBody.velocity.x) > Mathf.Epsilon;

            playerAnimations.SetBool("isRunning", PlayerhasHorizontalSpeed);



        }
      /*  void lightDash()
        {

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                DashForce = new Vector2(150f, 0f);
                playerBody.AddForce(DashForce, ForceMode2D.Impulse);
                CreateFlashStep();
            }

        }*/
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
                playerAnimations.SetBool("isClimbing", false);
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

        //TODO***** work on an injury system 
        /* private void gotHurt()
         {
             if(playerBody.IsTouchingLayers(LayerMask.GetMask("Enemies")))
             {
                 playerAnimations.SetBool("myShoulder", true)
                 playerBody.velocity = hurtFling;
             }
         }*/
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

        //TO DO Decouple shake Camera into Core namespace. 
        private void ShakeCamera()
        {
            if (cameraShake != null && applyCameraShake)
            {
                cameraShake.Play();

            }
        }

        void CreateDust()
        {
            dust.Play();
        }
        void CreateFlashStep()
        {
            FlashStep.Play();
        }

        void startDash()
        {
            if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                StartCoroutine(Dash());
            }
        }
        private IEnumerator Dash()
        {
            canDash = false;
            isDashing = true;
            float originalGravity = playerBody.gravityScale;
            playerBody.gravityScale = 0f;
            playerBody.velocity = new Vector2(transform.localScale.x * dashingStrength, moveInput.y * dashingStrength);
            DashTrail.emitting = true;
            yield return new WaitForSeconds(dashingTime); // only allow dashing for period of dash time
            DashTrail.emitting = false;
            playerBody.gravityScale = originalGravity; //reset gravity when dash is finished
            isDashing = false;
            yield return new WaitForSeconds(dashingCooldown); // cooldown for dashing.
            canDash = true;
        }
        public Vector3 getSwordTip()
        {
            return swordSlashTip.position;
        }
        
        public GameObject getProjectile()
        {
            return generatedPlayerProjectile;
        }
    }
}
