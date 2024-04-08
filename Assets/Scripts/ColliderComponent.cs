using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderComponent : MonoBehaviour
{

    public void OnCollisionEnter(Collision collision)
    {
        //GameObject otherObject = collision.gameObject;
        //Debug.Log(gameObject.tag + ", " + otherObject.tag);
        ////if (otherObject != null && otherObject.CompareTag("Player"))
        ////{
        ////    if (gameObject != null)
        ////    {
        ////        Destroy(gameObject);
        ////    }
        ////}

    }

    public void OnLaserCollision(Collider collider)
    {
        //collider.gameObject.SetActive(false);
        HealthComponent healthComponent = collider.gameObject.GetComponent<HealthComponent>();
        if (healthComponent != null)
        {
            healthComponent.TakeDamage(20);
            Debug.Log("Le laser a touché cet objet !");
        }

    }



    public void OnCollisionStay(Collision collision)
    {

    }

    public void OnCollisionExit(Collision collision)
    {

    }




}
