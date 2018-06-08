using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState { PATROL, CHASE, STOPPED}

public class Enemy : MonoBehaviour {

    public float WalkRange;
    public float walkSpeed;

    public float knockbackTime;
    float knockbackCounter = 0;
    float freezeCounter = 0;

    private bool isGrounded;
    private bool groundAhead;
    public Transform groundCheck;
    public Transform aheadCheck;
    public float checkRadius;

    Vector3 spawnPoint;

    EnemyState myState = EnemyState.PATROL;
    bool stopped;
    int walkDir = 1;

    public LayerMask whatIsGround;

    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        spawnPoint = transform.position;

        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        groundAhead = Physics2D.OverlapCircle(aheadCheck.position, checkRadius, whatIsGround);

        if (isGrounded)
        {
            if (myState == EnemyState.PATROL)
            {
                if (knockbackCounter == 0)
                {
                    transform.Translate(new Vector3(walkDir * walkSpeed * Time.deltaTime, 0));

                    if (isGrounded && !groundAhead)
                        Turn();

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
            if (myState == EnemyState.STOPPED)
            {
                if (freezeCounter <= 0)
                {
                    myState = EnemyState.PATROL;
                }
                else
                    freezeCounter -= Time.deltaTime;
            }
        }
	}

    public void Turn()
    {
        walkDir *= -1;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    public void Respawn()
    {
        transform.position = spawnPoint;
        walkDir = 1;
    }

    public void Knockback(Vector2 direction, float knockbackForce)
    {
        knockbackCounter = knockbackTime;

        rb.velocity = Vector2.zero;

        int dir = 1;

        if (direction.x < 0)
            dir = -1;

        //Apply knockback force to entity by changing velocity and moving the transform. This seems to feel the best for me.
        rb.velocity = new Vector2(dir * knockbackForce, knockbackForce/2);

        //transform.Translate(new Vector2(dir * knockbackForce, 0));
    }

    public void Freeze(float freezeTime)
    {
        myState = EnemyState.STOPPED;

        freezeCounter = freezeTime;
    }
}
