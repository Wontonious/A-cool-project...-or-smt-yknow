using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneInteract : MonoBehaviour
{
    private Vector3 scaleChange;

    void Awake()
    {
        scaleChange = new Vector3(1f, 1f, 0f)
;    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerOne"))
        {
            transform.localScale += scaleChange;
        }
    }
}
