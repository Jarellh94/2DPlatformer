﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce;

    private float moveInput;
    private bool facingRight = true;

    private Rigidbody2D rb;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    public int maxJumps;
    private int extrajumps;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxis("Horizontal");

        if(Input.GetKey(KeyCode.LeftShift))
            rb.velocity = new Vector2(moveInput * runSpeed, rb.velocity.y);
        else
            rb.velocity = new Vector2(moveInput * walkSpeed, rb.velocity.y);

        //When input is greater than 0, the player is walking right. When less than zero they're walking left.
        if (!facingRight && moveInput > 0)
            Flip();
        else if (facingRight && moveInput < 0)
            Flip();
    }

    void Update()
    {
        if(isGrounded)
        {
            extrajumps = maxJumps;
        }

        if(Input.GetKeyDown(KeyCode.W) && extrajumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extrajumps--;
        }
        else if(Input.GetKeyDown(KeyCode.W) && extrajumps == 0 && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
