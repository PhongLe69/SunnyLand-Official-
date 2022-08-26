﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    // Private Field
    Rigidbody2D rb;
    Animator animator;
    [SerializeField] Collider2D standingCollider, crouchingCollider;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] Transform overheadCheckCollider;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform wallCheckCollider;
    [SerializeField] LayerMask wallLayer;

    const float groundCheckRadius = 0.2f;
    const float overheadCheckRadius = 0.2f;
    const float wallCheckRadius = 0.2f;
    [SerializeField] float speed = 2;
    [SerializeField] float jumpPower = 500;
    [SerializeField] float slideFactor = 0.2f;
    [SerializeField] int totalJumps;
    int availableJumps;
    float horizontalValue;
    float runSpeedModifier = 2f;
    float crouchSpeedModifier = 0.5f;
    float dirX;

    [SerializeField] bool isGrounded;
    bool isRunning;
    bool facingRight = true;
    bool crouchPressed;
    bool multipleJump;
    bool coyoteJump;
    bool isSliding;
    bool isDead = false;
    bool isHurting;

    Vector3 localScale;

    void Awake()
    {
        availableJumps = totalJumps;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        ResetPlayer();
    }

    private void OnEnable()
    {
        PlayerHealth.INSTANCE.OnHealthZero += Die;
        PlayerHealth.INSTANCE.OnTakeDamage += OnHurt;
    }

    private void OnDisable()
    {
        PlayerHealth.INSTANCE.OnHealthZero -= Die;
        PlayerHealth.INSTANCE.OnTakeDamage -= OnHurt;
    }

    void Update()
    {
        /*if (CanMoveOrInteract() == false)
        {
            Debug.Log("Cannot Move");
            return;
        }*/

        // Store the Horizontal value
        horizontalValue = Input.GetAxisRaw("Horizontal");

        // If LShift is clicked enable isRunning
        if (Input.GetKeyDown(KeyCode.LeftShift))
            isRunning = true;
        // If LShift is released disable isRunning
        if (Input.GetKeyUp(KeyCode.LeftShift))
            isRunning = false;

        // If we press Jump button enable jump
        if (Input.GetButtonDown("Jump"))
            Jump();
        /*{
            animator.SetBool("Jump", true);
            jump = true;
        }*/

        // If we press Crouch button enable crouch
        if (Input.GetButtonDown("Crouch"))
            crouchPressed = true;
        // Otherwise disable it
        else if (Input.GetButtonUp("Crouch"))
            crouchPressed = false;

        // Set the yVelocity in the animator
        animator.SetFloat("yVelocity", rb.velocity.y);
        
        // Check if we are touching a wall to slide on it
        WallCheck();

    }

    void FixedUpdate()
    {
        GroundCheck();
        Move(horizontalValue, crouchPressed);


        // Hurting
        /*if (!isHurting)
            rb.velocity = new Vector2(dirX, rb.velocity.y);*/
    }

    /*void LateUpdate()
    {
        CheckWhereToFace();
    }

    void CheckWhereToFace()
    {
        if (dirX > 0)
            facingRight = true;
        else if (dirX < 0)
            facingRight = false;

        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
            localScale.x *= -1;

        transform.localScale = localScale;

    }*/


    public void OnHurt()
    {
        StartCoroutine(OnHurt_Corou());
    }

    IEnumerator OnHurt_Corou()
    {
        isHurting = true;
        rb.velocity = Vector2.zero;

        //Cause: OnTriggerEnter2D của EnemyAI
        //Effect (Cause): Player Take Damage qua method Lose Life
        //Lose Life: tọa độ của enemy vào Player Health. Ở đây mình ghi tọa độ cái thằng vừa đánh mình.
        //PlayerHealth enemyPos = tọa độ của enemy
        //Có tọa độ enemy, xác định là enemy bên trái hay phải so với mình, và nhảy theo hướng ngược lại.
        //playerPos.x, y và enemyPos.x,y so sánh x

        if (facingRight)
            rb.AddForce(new Vector2(-3500f, 400f));
        else
            rb.AddForce(new Vector2(3500f, 400f));

        yield return new WaitForSeconds(0.5f);

        isHurting = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheckCollider.position, groundCheckRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(overheadCheckCollider.position, overheadCheckRadius);
    }

    bool CanMoveOrInteract()
    {
        bool can = true;

        if (FindObjectOfType<InteractionSystem>().isExamining)
            can = false;
        if (FindObjectOfType<InventorySystem>().isOpen)
            can = false;
        if (isDead)
            can = false;

        return can;
    }

    void GroundCheck()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;
        //Check if the GroundCheckObject is colliding with other
        // 2D Colliders that are in the "Ground" Layer
        // If yes (isGrounded true) else (isGrounded false)
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
        {
            isGrounded = true;
            if (!wasGrounded)
            {
                availableJumps = totalJumps;
                multipleJump = false;

                AudioManager.instance.playSFX("landing");
            }

            // Check if any of the colliders is moving platform
            // Parent it to this platform
            foreach(var c in colliders)
            {
                if (c.tag == "MovingPlatform")
                    transform.parent = c.transform;
            }
        }
        else
        {
            // Un-parent the tranform
            transform.parent = null;

            if (wasGrounded)
                StartCoroutine(CoyoteJumpDelay());
        }

        //As long as we are grounded the "Jump" bool
        //in the animator is disabled
        animator.SetBool("Jump", !isGrounded);
    }

    void WallCheck()
    {
        // If we are touching a wall
        // and we are moving toward the wall
        // and we are falling 
        // and we are not grounded
        // ==>> Slide on the wall
        if (Physics2D.OverlapCircle(wallCheckCollider.position, wallCheckRadius, wallLayer)
            && Mathf.Abs(horizontalValue) > 0
            && rb.velocity.y < 0
            && !isGrounded )
        {
            if (!isSliding)
            {
                availableJumps = totalJumps;
                multipleJump = false;
            }
            Vector2 v = rb.velocity;
            v.y = -slideFactor;
            rb.velocity = v;
            isSliding = true;

            if (Input.GetButtonDown("Jump"))
            {
                availableJumps--;

                rb.velocity = Vector2.up * jumpPower;
                animator.SetBool("Jump", true);
            }
        }
        else
        {
            isSliding = false;
        }
    }

    #region Jump
    IEnumerator CoyoteJumpDelay()
    {
        coyoteJump = true;
        yield return new WaitForSeconds(0.2f);
        coyoteJump = false;
    }

    void Jump()
    {
        // If we press Crouch we disable standinCollider + animate Crouching
        // reduce the speed 
        // if release resume the original speed + enable the standing collider 
        // + disable couch animation
        if (isGrounded)
        {
            /*if (crouchFlag)
            {
                standingCollider.enabled = false;
            }
            else
            {
                standingCollider.enabled = true;
            }*/


            // If the player is grounded and pressed W jump

            //Add jump force
            //rb.AddForce(new Vector2(0f, jumpPower));   OLD JUMP

            multipleJump = true;
            availableJumps--;

            // and NEW JUMP
            rb.velocity = Vector2.up * jumpPower;
            animator.SetBool("Jump", true);
        }
        else
        {
            if (coyoteJump)
            {
                multipleJump = true;
                availableJumps--;

                rb.velocity = Vector2.up * jumpPower;
                animator.SetBool("Jump", true);
            }

            if (multipleJump && availableJumps > 0)
            {
                {
                    availableJumps--;

                    rb.velocity = Vector2.up * jumpPower;
                    animator.SetBool("Jump", true);
                }
            }
        }
    }

    #endregion

    void Move(float dir, bool crouchFlag)
    {
        #region Crouch

        // If we are crouching and disable crouching
        //Check overhead for collision with Ground items
        //If there are any, remain crouched, otherwise un-crouch
        if (!crouchFlag)
        {
            if (Physics2D.OverlapCircle(overheadCheckCollider.position, overheadCheckRadius, groundLayer))
                crouchFlag = true;  
        }

        animator.SetBool("Crouch", crouchFlag);
        standingCollider.enabled = !crouchFlag;
        crouchingCollider.enabled = crouchFlag;



        // animator.SetBool("Crouch", crouchFlag);

        #endregion

        #region Move & Run
        // set the value of x using dir and speed
        float xVal = dir * speed * 100 * Time.fixedDeltaTime;
        //If we are running multiply with the running modifier (higher)
        if (isRunning)
            xVal *= runSpeedModifier;
        //If we are running multiply with the running modifier (higher)
        if (crouchFlag)
            xVal *= crouchSpeedModifier;
        // Create Vec2 for the velocity
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        // set the player's veclocity
        rb.velocity = targetVelocity;


        // If loking right and clicked left (flip to the left)
        if (facingRight && dir < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }

        // If loking left and clicked right (flip to the right)
        else if (!facingRight && dir > 0)   // !facingRight : facingRight == false
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }

        // (0 idle, 4 walking, 8 running)
        // Set the float xVelocity arcoding to the x value
        // of the Rigidbody2D velocity 
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        #endregion
    }

    public void Die()
    {
        isDead = true;
        FindObjectOfType<LevelManager>().Restart();
    }

    public void ResetPlayer()
    {
        isDead = false;
    }
}