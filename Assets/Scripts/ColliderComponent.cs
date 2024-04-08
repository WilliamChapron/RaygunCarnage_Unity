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

  



    public void OnCollisionStay(Collision collision)
    {

    }

    public void OnCollisionExit(Collision collision)
    {

    }




}
