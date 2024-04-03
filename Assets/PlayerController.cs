using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    private bool isRunning = false;

    void Update()
    {
        //if (Input.GetKey(KeyCode.O))
        //{
        //    Debug.Log("Avance");
        //    isRunning = true;
        //}
        //else
        //{
        //    isRunning = false;
        //}
        animator.SetBool("isRunning", true);
    }
}
