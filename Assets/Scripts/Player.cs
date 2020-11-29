using UnityEngine;

public class Player : MonoBehaviour
{
    //Sugar and spice and everything that has to do with movement
    public float moveSpeed = 20f;
    private float movingSpeed;
    public float counterSpeed = 4f;
    public float maxSpeed = 50f;
    Vector2 moveDirection;
    Vector2 counterMove;

    //Jumping
    public float jumpDivide = 1.5f;
    public float jumpForce = 25f;
    float jumpMultiplier = 10000f;
    public float gravity = -9.81f;
    public bool isGrounded = false;
    private bool occurOnce = false;
    public bool canJump = true;

    //Player health
    public float playerHealth = 50f;

    //Misc 
    public Rigidbody2D rb;
    public Animator anim;
    SwitchPlayer player;
    public SpriteRenderer sprite;


    void Start()
    {
        movingSpeed = moveSpeed;
        player = GetComponent<SwitchPlayer>();
        //playerIsGrounded = GetComponent<FootOnGround>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.SelectPlayer())
        {
            ProcessInputs();
        }
        if (rb.velocity.y > 0)
        {
            rb.AddForce(Vector2.up * gravity * 0.75f * Time.deltaTime);
        }

    }

        private void FixedUpdate()
    {
        if (player.SelectPlayer())
        {
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
            CounterMoves();
            Move();
        }

        if (!player.SelectPlayer())
        {
            rb.velocity = Vector2.zero;
        }
    }

    void ProcessInputs()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        anim.SetFloat("MoveSpeed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));

        if (Input.GetButtonDown("Jump") && isGrounded && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up.normalized * jumpForce * jumpMultiplier * Time.deltaTime);
            moveSpeed /= 4f;
            canJump = false;
        }


        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(moveDirection * moveSpeed * Time.deltaTime);
            sprite.flipX = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(moveDirection * moveSpeed * Time.deltaTime);
            sprite.flipX = false;
        }

        if (isGrounded)
        {
            moveSpeed = movingSpeed;
            occurOnce = true;
            canJump = true;
        }

        if (!isGrounded && occurOnce)
        {
            occurOnce = false;
            canJump = false;
            moveSpeed = moveSpeed / jumpDivide;
        }

        if(Input.GetAxisRaw("Horizontal") == 0 && (rb.velocity.x > 0.1f || rb.velocity.x <0.1f))
        {
            rb.AddForce(Vector2.right * -rb.velocity.normalized);
        }
    }

    void CounterMoves()
    {
        if (rb.velocity.x > 0)
        {
            counterMove.x = -counterSpeed;
        }
        if (rb.velocity.x < 0)
        {
            counterMove.x = counterSpeed;
        }
        rb.AddForce(counterMove * Time.deltaTime);
    }

    void Move()
    {
        //rb.AddForce(moveDirection + counterMove);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isGrounded = false;
        }
    }


    void TakeDamage(float damage)
    {
        playerHealth -= damage;
        Debug.Log(playerHealth);
        if (playerHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("You died");
        Destroy(gameObject);
    }

}
