using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCube : MonoBehaviour
{
    public int index;
    private void OnTriggerEnter(Collider player)
    {
        
       Shooting myShoot = player.GetComponent<Shooting>();
       myShoot.addPower(index);
       Destroy(gameObject);
    }
}
