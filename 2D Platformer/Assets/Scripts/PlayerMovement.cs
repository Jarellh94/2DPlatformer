using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

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
    public LayerMask whatIsGround;
    public LayerMask enemyHead;

    private bool onEnemyHead;

    public int maxJumps;
    private int extrajumps;

    public Transform spawnPoint;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        onEnemyHead = Physics2D.OverlapCircle(groundCheck.position, checkRadius, enemyHead);

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

        if(onEnemyHead && Input.GetKey(KeyCode.W))
        {
            rb.velocity = Vector2.up * footStoolForce;
        }
        else if(Input.GetKeyDown(KeyCode.W) && extrajumps > 0)
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

    public void Respawn()
    {
        transform.position = spawnPoint.position;
    }
}
