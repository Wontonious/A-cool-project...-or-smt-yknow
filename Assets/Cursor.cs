
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public Camera cam;
    private Vector3 camPos;
    // Update is called once per frame
    void Update()
    {
        camPos.Set(0f, 0f, 15f);
        transform.position = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position += camPos;
    }
}
