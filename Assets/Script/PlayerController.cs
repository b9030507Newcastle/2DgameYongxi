using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    public float doubleJumpSpeed;
    
    private Rigidbody2D rb;
    private Animator am;
    private BoxCollider2D feet;
    private bool isGround;
    private bool canDoubleJump;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        am = GetComponent<Animator>();
        feet = GetComponent<BoxCollider2D>();
    }
    

    private void Update()
    {
        CheckGround();
        Run();
        Jump();
        Flip();
        SwitchAnimation();
        //Attack();
    }

    void Run()
    { 
        float moveDir = Input.GetAxis("Horizontal");
        Vector2 playerVel = new Vector2(moveDir * runSpeed, rb.velocity.y);
        rb.velocity = playerVel;
        bool playerHasXAxisSpeed = Math.Abs(rb.velocity.x) > Mathf.Epsilon;
        am.SetBool("Run", playerHasXAxisSpeed);
    }

    void CheckGround()
    {
        isGround = feet.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
                   feet.IsTouchingLayers(LayerMask.GetMask("MovingPlatform")); ;
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if(isGround)
            {
                am.SetBool("Jump", true);
                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
                rb.velocity = Vector2.up * jumpVel;
                canDoubleJump = true;
            }
            else
            {
                if (canDoubleJump)
                {
                    am.SetBool("DoubleJump", true);
                    Vector2 doubleJumpVel = new Vector2(0.0f, doubleJumpSpeed);
                    rb.velocity = Vector2.up * doubleJumpVel;
                    canDoubleJump = false;
                }
            }
        }
    }
    void SwitchAnimation()
        {
            am.SetBool("Idle", false);
            if (am.GetBool("Jump"))
            {
                if (rb.velocity.y < 0.0f)
                {
                    am.SetBool("Jump", false);
                    am.SetBool("Fall", true);
                }
            }
            else if(isGround)
            {
                am.SetBool("Fall", false);
                am.SetBool("Idle", true);
            }

            if (am.GetBool("DoubleJump"))
            {
                if (rb.velocity.y < 0.0f)
                {
                    am.SetBool("DoubleJump", false);
                    am.SetBool("DoubleFall", true);
                }
            }
            else if(isGround)
            {
                am.SetBool("DoubleFall", false);
                am.SetBool("Idle", true);
            }

        }
    void Flip()
    {
        bool playerHasXAxisSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (playerHasXAxisSpeed)
        {
            if (rb.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0,0,0);
            }
            
            if (rb.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0,180,0);
            }
        }
    }

    // void Attack()
    // {
    //     if (Input.GetButtonDown("Attack"))
    //     {
    //         am.SetTrigger("Attack");
    //     }
    // }

}
