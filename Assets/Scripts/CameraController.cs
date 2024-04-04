using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //public float _moveSpeed = 5f;
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    // Controls WASD
    //    float horizontalInput = Input.GetAxisRaw("Horizontal");
    //    float verticalInput = Input.GetAxisRaw("Vertical");

    //    Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * _moveSpeed * Time.deltaTime;
    //    transform.Translate(movement);
    //}

    public Transform target;

    void Update()
    {
        // Rotate the camera every frame so it keeps looking at the target
        //transform.LookAt(target);

        // Same as above, but setting the worldUp parameter to Vector3.left in this example turns the camera on its side
        transform.LookAt(target, Vector3.up);
    }


    //public float rotationSpeed = 6.0f; // Vitesse de rotation de la caméra

    //void Update()
    //{
    //    // Rotation autour du point cible
    //    transform.RotateAround(new Vector3(0.0f, 0.0f, 0.0f), Vector3.up, rotationSpeed * Time.deltaTime);
    //}
}
