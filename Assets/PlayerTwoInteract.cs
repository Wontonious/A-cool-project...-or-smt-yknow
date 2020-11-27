using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoInteract : MonoBehaviour
{
    private Vector3 scaleChange;

    void Awake()
    {
        scaleChange = new Vector3(-1f, -1f, 0f)
;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerTwo"))
        {
            if (transform.localScale.x >= 3f || transform.localScale.y >= 3f)
            {
                transform.localScale += scaleChange;
            }
        }
    }
}
