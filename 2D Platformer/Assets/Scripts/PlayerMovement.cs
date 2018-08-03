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
    public float footStoolCheckHeight;
    public LayerMask whatIsGround;
    public LayerMask enemyHead;
    public LayerMask playerLayer;

    private bool onEnemyHead;
    private bool onPlayerHead;

    public int maxJumps;
    private int extrajumps;

    public Transform spawnPoint;

    //Knockback Stuff
    public float knockbackTime;
    float knockbackCounter = 0;

    float freezeCounter = 0;

    //Controller Stuff
    public int playerNum;
    public int controllerNum;

    PlayerActions actions;
    Gun myGun;
    GameManager gameMam;

    string horizontal;
    string vertical;
    string A;
    string B;
    string X;
    string RB;
    string startButton;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        actions = GetComponent<PlayerActions>();
        myGun = GetComponentInChildren<Gun>();
        gameMam = FindObjectOfType<GameManager>();

        horizontal = "J" + controllerNum + "Horizontal";
        //vertical = "J" + controllerNum + "Vertical";
        A = "J" + controllerNum + "A";
        B = "J" + controllerNum + "B";
        X = "J" + controllerNum + "X";
        RB = "J" + controllerNum + "RB";
        startButton = "J" + controllerNum + "Start";
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        onEnemyHead = Physics2D.OverlapBox(groundCheck.position, new Vector2(1.3f, footStoolCheckHeight), 0f,  enemyHead);
       


        moveInput = Input.GetAxis(horizontal);

        if (knockbackCounter == 0)
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetButton(X))
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
        if (isGrounded)
        {
            extrajumps = maxJumps;
        }

        if (knockbackCounter == 0 && freezeCounter == 0)
        {

            if (onEnemyHead && (Input.GetKey(KeyCode.W) || Input.GetButton(A)))
            {
                rb.velocity = Vector2.up * footStoolForce;
                Debug.Log("Footstool");
            }
            else if ((Input.GetKeyDown(KeyCode.W) || Input.GetButtonDown(A)) && extrajumps > 0 && !isGrounded)
            {
                rb.velocity = Vector2.up * jumpForce;
                extrajumps--;
            }
            else if ((Input.GetKeyDown(KeyCode.W) || Input.GetButtonDown(A)) && isGrounded)
            {
                rb.velocity = Vector2.up * jumpForce;
            }

            if ((Input.GetKeyDown(KeyCode.J) || Input.GetButtonDown(B)))
            {
                actions.Attack();
            }

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown(RB))
            {
                myGun.Fire();
            }
        }
        else
        {
            if (knockbackCounter > 0)
            {
                knockbackCounter -= Time.deltaTime;

                if (knockbackCounter < 0)
                {
                    knockbackCounter = 0;
                    rb.velocity = Vector2.zero;
                }
            }

            if (freezeCounter > 0)
            {
                freezeCounter -= Time.deltaTime;

                if (freezeCounter < 0)
                    freezeCounter = 0;
            }
        }

        if (Input.GetButtonDown(startButton))
        {
            gameMam.PausePressed();
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
        Gun myGun = GetComponentInChildren<Gun>();

        myGun.Respawn();
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

    public void Freeze(float freezeTime)
    {
        freezeCounter = freezeTime;
    }

    public void SetControllerNum(int num)
    {
        gameObject.SetActive(true);
        controllerNum = num;
        GetComponent<PlayerUIManager>().ActivatePanel();

        horizontal = "J" + controllerNum + "Horizontal";
        vertical = "J" + controllerNum + "Vertical";
        A = "J" + controllerNum + "A";
        B = "J" + controllerNum + "B";
        X = "J" + controllerNum + "X";
        RB = "J" + controllerNum + "RB";
        startButton = "J" + controllerNum + "Start";

        Respawn();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawCube(groundCheck.position, new Vector3(1.3f, footStoolCheckHeight));

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheck.position, checkRadius);
    }

}
