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
    public float gravity = -9.81f;
    public bool isGrounded = false;
    private bool occurOnce = false;

    //Player health
    float playerHealth = 50f;

    //Misc 
    public Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer sprite;
    GroundCheck grounded;
    SwitchPlayer player;

    void Start()
    {
        movingSpeed = moveSpeed;
        player = GetComponent<SwitchPlayer>();
        grounded = GetComponent<GroundCheck>();
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
        moveDirection.y += gravity * Time.deltaTime;

    }

    private void FixedUpdate()
    {
        if (player.SelectPlayer())
        {
            anim.SetFloat("MoveSpeed", Mathf.Abs(moveDirection.x));

            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
            CounterMoves();
            Move();
        }
        if (!player.SelectPlayer())
        {
            anim.SetFloat("MoveSpeed", 0f);
            Vector2 movement = Vector2.zero;
            movement.y = rb.velocity.y;
            rb.velocity = movement;
        }
    }

    void ProcessInputs()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal") * moveSpeed;

        if (Input.GetButtonDown("Jump") && (isGrounded || grounded.Grounded()))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);

            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.A))
        {
            sprite.flipX = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            sprite.flipX = false;
        }

        if (isGrounded)
        {
            moveDirection.y = -2f;
            moveSpeed = movingSpeed;
            occurOnce = true;
        }

        if (!isGrounded && occurOnce)
        {
            rb.AddForce(Vector2.down * gravity * Time.deltaTime);
            occurOnce = false;
            moveSpeed = moveSpeed / jumpDivide;
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
        rb.AddForce(moveDirection * moveSpeed + counterMove);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isGrounded = false;
        }
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isGrounded = true;
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
