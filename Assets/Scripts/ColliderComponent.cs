using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderComponent : MonoBehaviour
{
    // Start is called before the first frame update


    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    //public void OnCollisionEnter(Collision collision)
    //{
    //    GameObject otherObject = collision.gameObject;
    //    Debug.Log(gameObject.tag + ", " + otherObject.tag);
    //    //if (otherObject != null && otherObject.CompareTag("Player"))
    //    //{
    //    //    if (gameObject != null)
    //    //    {
    //    //        Destroy(gameObject);
    //    //    }
    //    //}

    //}

    public void OnLaserCollision()
    {
        Debug.Log("Le laser a touché cet objet !");
    }



    //public void OnCollisionStay(Collision collision)
    //{

    //}

    //public void OnCollisionExit(Collision collision)
    //{

    //}




}
