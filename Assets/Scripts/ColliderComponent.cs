using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Debug.Log(name + " a une Collision avec : " + other.gameObject.name);
            HealthComponent healthComponent = other.gameObject.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(200);
                Debug.Log("Prend Dommage a cause particule");
            }
        }
    }





    //public void OnCollisionStay(Collision collision)
    //{

    //}





}
