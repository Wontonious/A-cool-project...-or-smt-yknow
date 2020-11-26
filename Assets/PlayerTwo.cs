using UnityEngine;

public class PlayerTwo : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 1f;
    public Rigidbody2D rb;

    private Vector2 moveDirection;
    private Vector2 mousePos;

    public Camera cam;
    SwitchPlayer player;

    void Start()
    {
        player = GetComponent<SwitchPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!player.SelectPlayer())
        {
        ProcessInputs();

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        }
    }
    //Good for physics calculations
    void FixedUpdate()
    {
        if (!player.SelectPlayer())
        {
            Move();
            Aim();
        }
        if (player.SelectPlayer())
        {
            rb.velocity = Vector2.zero;
        }
    }

    void ProcessInputs()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");
    }

    void Move()
    {
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    void Aim()
    {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }
}
