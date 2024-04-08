using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderComponent : MonoBehaviour
{

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "PlayerControllable")
        {
            Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                rb.useGravity = false;
            }
        }
    }

  



    public void OnCollisionStay(Collision collision)
    {

    }

    public void OnCollisionExit(Collision collision)
    {

    }




}
