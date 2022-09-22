using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    public bool facingRight = false;
    public LayerMask whatIsGround;

    public bool isGrounded = false;
    public bool isFalling = false;
    public bool isJumping = false;

    public float jumpForceX = 2f;
    public float jumpForceY = 4f;

    public float lastYPos = 0;

    public enum Animations
    {
        Idle = 0,
        Jumping = 1,
        Falling = 2,
    };

    public Animations currentAnim;

    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Animator anim;

    public float idleTime = 2f;
    public float currentIdleTime = 0;
    public bool isIdle = true;

    // Start is called before the first frame update
    void Start()
    {
        lastYPos = transform.position.y;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // This if statement code only gets called if we are idle
        if (isIdle)
        {
            // Add real world time to currentIdleTime
            currentIdleTime += Time.deltaTime;
            // If 2 seconds have passed 
            if(currentIdleTime >= idleTime)
            {
                // Tell our frog to jump, and change the directiong it's facing 
                currentIdleTime = 0;
                facingRight = !facingRight;
                spriteRenderer.flipX = facingRight;
                Jump();
            }
        }
        
        // Determine if our is grounded
        isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - 1f, transform.position.y - 1f),
            new Vector2(transform.position.x + 1f, transform.position.y - 1.1f), whatIsGround);

        // We have just fallen onto the ground, set idle to true
        if (isGrounded && isFalling)
        {
            isJumping = false;
            isFalling = false;
            isIdle = true;
            ChangeAnimation(Animations.Idle);
        }
        // We are going up, and are not grounded, set isJumping to true
        else if(transform.position.y > lastYPos && !isGrounded && !isIdle)
        {
            isJumping = true;
            isFalling = false;
            ChangeAnimation(Animations.Jumping);

        }
        // We are going down, and are not grounded, set isFalling to true
        else if (transform.position.y < lastYPos && !isGrounded && !isIdle)
        {
            isJumping = false;
            isFalling = true;
            ChangeAnimation(Animations.Falling);

        }

        lastYPos = transform.position.y;
    }

    void Jump()
    {
        isJumping = true;
        isIdle = false;
        int direction = 0;
        if (facingRight == true)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
        rb.velocity = new Vector2(jumpForceX * direction, jumpForceY);
        Debug.Log("Jump!");
    }

    void ChangeAnimation(Animations newAnim)
    {
        if(currentAnim != newAnim)
        {
            currentAnim = newAnim;
            anim.SetInteger("state", (int)newAnim);
        }
    }
}
