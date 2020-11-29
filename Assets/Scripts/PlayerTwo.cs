using UnityEngine;

public class PlayerTwo : MonoBehaviour
{
    //Sugar and spice and everything that has to do with movement
    public float moveSpeed = 20f;
    public float counterSpeed = 4f;
    public float maxSpeed = 50f;
    Vector2 moveDirection;
    Vector2 counterMove;

    //Jumping
    public float jumpForce = 10f;
    float jumpMultiplier = 10000f;
    public float gravity = -9.81f;
    public bool isGrounded = false;

    //Player health
    public float playerHealth = 50f;

    //Misc 
    public Rigidbody2D rb;
    public GameObject groundCheck;
    public GameObject ceilingCheck;

    SwitchPlayer player;

    void Start()
    {
        player = GetComponent<SwitchPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.SelectPlayer())
        {
            ProcessInputs();
        }
    }

    private void FixedUpdate()
    {
        if (!player.SelectPlayer())
        {
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
            CounterMoves();
            Move();
        }

        if (player.SelectPlayer())
        {
            rb.velocity = Vector2.zero;
        }
    }

    void ProcessInputs()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal") * moveSpeed;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce * jumpMultiplier * Time.deltaTime);
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
    }

    void Move()
    {
        rb.AddForce(moveDirection + counterMove);
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
