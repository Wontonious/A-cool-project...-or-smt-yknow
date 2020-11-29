using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public Rigidbody2D rb;
    private float distanceFromCamera;
    private bool isBeingHeld = false;

    void Update()
    {
        if (Input.GetMouseButton(0) && isBeingHeld)
        {
            Vector3 pos = Input.mousePosition;
            pos.z = distanceFromCamera;
            pos = Camera.main.ScreenToWorldPoint(pos);
            rb.velocity = (pos - this.gameObject.transform.localPosition) * 10;
        }
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Input.mousePosition;
            pos.z = distanceFromCamera;
            pos = Camera.main.ScreenToWorldPoint(pos);

            isBeingHeld = true;
        }
    }

    void OnMouseUp()
    {
        isBeingHeld = false;
    }
}