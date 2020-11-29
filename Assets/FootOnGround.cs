using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootOnGround : MonoBehaviour
{
    bool isGrounded = false;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isGrounded = false;
        }
    }

    public bool PlayerGrounded()
    {
        return isGrounded;
    }
}
