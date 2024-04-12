using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class ColliderComponent : MonoBehaviour
{

    private string playerName;

    public void SetPlayerName(string name)
    {
        playerName = name;
    }

    public void PrintPlayerName()
    {
        Debug.Log(playerName);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerControllable") && other.gameObject.name != playerName)
        {
            if (other.gameObject.CompareTag("PlayerControllable"))
            {
                if (other.gameObject.GetComponent<PlayerController>()._currentState != PlayerState.Shield)
                {
                    if (GetComponent<Collider>().gameObject.GetComponent<PlayerController>()._currentState == PlayerState.MiddleDead)
                    {
                        GetComponent<Collider>().gameObject.GetComponent<PlayerController>().SetPlayerState(PlayerState.Dead);
                    }
                    else
                    {
                        GetComponent<Collider>().gameObject.GetComponent<PlayerController>().SetPlayerState(PlayerState.MiddleDead);
                    }
                }

            }
        }
    }





    //public void OnCollisionStay(Collision collision)
    //{

    //}





}
