using UnityEngine;

public class SwitchPlayer : MonoBehaviour
{
    bool selectPlayerOne = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            PlayerSelector();
        }
    }
    public void PlayerSelector()
    {
        if(selectPlayerOne == true)
        {
            selectPlayerOne = false;
        }
        else if(selectPlayerOne == false)
        {
            selectPlayerOne = true;
        }
    }

    public bool SelectPlayer()
    {
        return selectPlayerOne;
    }
}
