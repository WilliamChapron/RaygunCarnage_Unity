using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderComponent : MonoBehaviour
{
    private Rigidbody rb;
    private bool hasCollided = false;

    

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("EntityEmpty"))
        {
            Debug.Log(name + " a une Collision avec : " + other.gameObject.name);
            //HealthComponent healthComponent = collision.collider.gameObject.GetComponent<HealthComponent>();
            //if (healthComponent != null)
            //{
            //    healthComponent.TakeDamage(20);
            //}
        }
    }





    public void OnCollisionStay(Collision collision)
    {

    }





}
