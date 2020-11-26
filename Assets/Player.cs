using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 1f;
    public Rigidbody2D rb;

    private Vector2 moveDirection;
    private Vector2 mousePos;

    public Camera cam;
    SwitchPlayer playerSwitcher;

    void Start()
    {
        playerSwitcher = GetComponent<SwitchPlayer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerSwitcher.SelectPlayer())
        {
            ProcessInputs();

            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        }
    }
    //Good for physics calculations
    void FixedUpdate()
    {
        if (playerSwitcher.SelectPlayer())
        {
            Move();
            Aim();
        }
        if (!playerSwitcher.SelectPlayer())
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
