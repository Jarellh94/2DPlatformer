using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //Actual Movement Stuff
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce;
    public float footStoolForce;

    private float moveInput;
    private bool facingRight = true;

    private Rigidbody2D rb;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public float footStoolCheckRadius;
    public LayerMask whatIsGround;
    public LayerMask enemyHead;

    private bool onEnemyHead;

    public int maxJumps;
    private int extrajumps;

    public Transform spawnPoint;

    //Knockback Stuff
    public float knockbackTime;
    float knockbackCounter = 0;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        onEnemyHead = Physics2D.OverlapCircle(groundCheck.position, footStoolCheckRadius, enemyHead);

        moveInput = Input.GetAxis("Horizontal");

        if (knockbackCounter == 0)
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetButton("Fire2"))
                rb.velocity = new Vector2(moveInput * runSpeed, rb.velocity.y);
            else
                rb.velocity = new Vector2(moveInput * walkSpeed, rb.velocity.y);
        }

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
        if (knockbackCounter == 0)
        {

            if (onEnemyHead && (Input.GetKey(KeyCode.W) || Input.GetButton("Jump")))
            {
                rb.velocity = Vector2.up * footStoolForce;
            }
            else if ((Input.GetKeyDown(KeyCode.W) || Input.GetButtonDown("Jump")) && extrajumps > 0 && !isGrounded)
            {
                rb.velocity = Vector2.up * jumpForce;
                extrajumps--;
            }
            else if ((Input.GetKeyDown(KeyCode.W) || Input.GetButtonDown("Jump")) && isGrounded)
            {
                rb.velocity = Vector2.up * jumpForce;
            }
        }
        else
        {
            knockbackCounter -= Time.deltaTime;

            if (knockbackCounter < 0)
            {
                knockbackCounter = 0;
                rb.velocity = Vector2.zero;
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
        transform.position = spawnPoint.position;
    }

    public void Knockback(Vector2 direction, float knockbackForce)
    {
        knockbackCounter = knockbackTime;

        rb.velocity = Vector2.zero;

        int dir = 1;

        if (direction.x < 0)
            dir = -1;

        //Apply knockback force to entity by changing velocity and moving the transform. This seems to feel the best for me.
        rb.velocity = new Vector2(dir * knockbackForce, knockbackForce / 2);

        //transform.Translate(new Vector2(dir * knockbackForce, 0));
    }
}
