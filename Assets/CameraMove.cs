using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player;
    public Transform playerTwo;
    public float cameraZ = -15f;
    Vector3 position;
    SwitchPlayer selectedPlayer;

    void Start()
    {
        selectedPlayer = GetComponent<SwitchPlayer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (player != null && selectedPlayer.SelectPlayer())
        {
            position = player.transform.position;
        }
        else if(player != null && !selectedPlayer.SelectPlayer())
        {
            position = playerTwo.transform.position;
        }
        position.z = cameraZ;
        transform.position = position;
        transform.rotation = Quaternion.identity;
    }
}
