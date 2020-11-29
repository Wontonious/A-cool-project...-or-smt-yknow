using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    bool isGrounded = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isGrounded = false;
        }
    }


    public bool Grounded()
    {
        return isGrounded;
    }
}
