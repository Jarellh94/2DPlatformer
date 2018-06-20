using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour {

    public float flyRange;
    public float flySpeed;
    public float pauseTime;
    public float dropDelay;

    bool movingRight = true;
    int dir = 1;

    float pauseCounter = 0;
    float dropCounter = 0;

    EnemyShooter myShooter;
    Rigidbody2D rig;

    Transform player;
    float startX;

	// Use this for initialization
	void Start () {
        myShooter = GetComponent<EnemyShooter>();
        rig = GetComponent<Rigidbody2D>();
        startX = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
        //If stopped Move to the opposite side.
        //If above player, drop the items.

        float newX = transform.position.x;

        if (player != null)
        {
            if (movingRight && pauseCounter <= 0)
            {
                if (transform.position.x < (player.position.x + flyRange))
                    newX = transform.position.x + flySpeed * Time.deltaTime * dir;
                else
                {
                    ChangeDirection();
                }
            }
            else if (!movingRight && pauseCounter <= 0)
            {
                if (transform.position.x > (player.position.x - flyRange))
                    newX = transform.position.x + flySpeed * Time.deltaTime * dir;
                else
                {
                    ChangeDirection();
                }
            }

            if (transform.position.x >= player.transform.position.x - 0.5f && transform.position.x <= player.transform.position.x + 0.5f && dropCounter <= 0)
                Drop();
        }
        else
        {
            if (movingRight && pauseCounter <= 0)
            {
                if (transform.position.x < (startX + flyRange))
                    newX = transform.position.x + flySpeed * Time.deltaTime * dir;
                else
                {
                    ChangeDirection();
                }
            }
            else if (!movingRight && pauseCounter <= 0)
            {
                if (transform.position.x > (startX - flyRange))
                    newX = transform.position.x + flySpeed * Time.deltaTime * dir;
                else
                {
                    ChangeDirection();
                }
            }
        }

        if (pauseCounter > 0)
        {
            pauseCounter -= Time.deltaTime;
        }

        if (dropCounter > 0)
            dropCounter -= Time.deltaTime;
        
        Move(newX);
    }

    void Move(float nx)
    {
        transform.position = new Vector3(nx, transform.position.y, transform.position.z);
    }

    void ChangeDirection()
    {
        movingRight = !movingRight;
        dir *= -1;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;

        pauseCounter = pauseTime;
    }

    public void SetTarget(Transform tar)
    {
        player = tar;
    }

    void Drop()
    {
        dropCounter = dropDelay;
        myShooter.Fire();
    }
    
}
