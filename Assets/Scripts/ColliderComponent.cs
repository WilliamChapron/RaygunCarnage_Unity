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
        Debug.Log("test");
        if (other.CompareTag("PlayerControllable") && other.gameObject.name != playerName)
        {
            if (other.gameObject.CompareTag("PlayerControllable"))
            {
                Player1Controller Pco = other.gameObject.GetComponent<Player1Controller>();
                if (Pco == null)
                {
                    Player2Controller Pct = other.gameObject.GetComponent<Player2Controller>();
                    if(Pct._currentState != PlayerState.Shield) 
                    { 
                        if(Pct.middledead == true)
                        {
                            Pct.SetPlayerState(PlayerState.Dead);
                        }
                        else
                        {
                            Pct.middledead = true;
                        }
                    }
                    
                }
                else
                {
                    if (Pco._currentState != PlayerState.Shield)
                    {
                        if (Pco.middledead == true)
                        {
                            Pco.SetPlayerState(PlayerState.Dead);
                        }
                        else
                        {
                            Pco.middledead = true;
                        }
                    }
                }
            }
        }
    }





    //public void OnCollisionStay(Collision collision)
    //{

    //}





}
