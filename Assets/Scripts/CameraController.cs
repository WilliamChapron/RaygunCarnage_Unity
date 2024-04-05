using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class CameraController : MonoBehaviour
{
    public float _moveSpeed = 5f;
    private float _zoomSpeed = 5.0f;
    void Start()
    {
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    public struct PositionInfo
    {
        public float x;
        public bool isPositiveX;

        public float y;
        public bool isPositiveY;

        public float z;
        public bool isPositiveZ;
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
        //transform.LookAt(target);

        List<Vector3> positionsList = new List<Vector3>();



        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("PlayerControllable");

        foreach (GameObject obj in playerObjects)
        {
            //Debug.Log(obj.name);
            positionsList.Add(obj.transform.position);
        }

        Vector3 averageLookAt = Vector3.zero;
        // Get most close & far axes and store them in two vect


        List<Vector3> secondPositionsList = new List<Vector3>(positionsList);
        List<PositionInfo> farthestPlayerPositions = new List<PositionInfo>
        {
            new PositionInfo { x = float.MinValue, y = float.MinValue, z = float.MinValue, isPositiveX = false, isPositiveY = false, isPositiveZ = false },
            new PositionInfo { x = float.MinValue, y = float.MinValue, z = float.MinValue, isPositiveX = false, isPositiveY = false, isPositiveZ = false }
        };


        // sa prend 7 
        foreach (Vector3 pos in secondPositionsList) 
        {
            Debug.Log("PLAYER SDSDSDDDSD");
            //Debug.Log(pos);
            float pX = Mathf.Abs(pos.x);
            float pY = Mathf.Abs(pos.y);
            float pZ = Mathf.Abs(pos.z);

            Debug.Log(pX + " / " + farthestPlayerPositions[0].x);
            Debug.Log(pX + " / " + farthestPlayerPositions[0].x);
            Debug.Log(pX + " / " + farthestPlayerPositions[0].x);

            pX = Mathf.Max(pX, farthestPlayerPositions[0].x);
            pY = Mathf.Max(pY, farthestPlayerPositions[0].y);
            pZ = Mathf.Max(pZ, farthestPlayerPositions[0].z);




            farthestPlayerPositions[0] = new PositionInfo { x = pX, y = pY, z = pZ, isPositiveX = pos.x >= 0f, isPositiveY = pos.y >= 0f, isPositiveZ = pos.z >= 0f };


            Debug.Log("TENTATIVE DE FIND PLUS GRANDE POSITION");
            float x = farthestPlayerPositions[0].isPositiveX ? farthestPlayerPositions[0].x : -farthestPlayerPositions[0].x;
            float y = farthestPlayerPositions[0].isPositiveY ? farthestPlayerPositions[0].y : -farthestPlayerPositions[0].y;
            float z = farthestPlayerPositions[0].isPositiveZ ? farthestPlayerPositions[0].z : -farthestPlayerPositions[0].z;
            Debug.Log(farthestPlayerPositions[0].isPositiveX);
            Debug.Log(x);
            Debug.Log(farthestPlayerPositions[0].isPositiveY);
            Debug.Log(y);
            Debug.Log(farthestPlayerPositions[0].isPositiveZ);
            Debug.Log(z);




        }








        if (Input.GetKeyDown(KeyCode.E))
        {

            //GameObject lightObject = new GameObject("BrightLight");
            //Light lightComponent = lightObject.AddComponent<Light>();
            //lightComponent.color = Color.red;
            //lightComponent.intensity = 50f;

            //float x = farthestPlayerPositions[0].isPositiveX ? farthestPlayerPositions[0].x : -farthestPlayerPositions[0].x;
            //float y = farthestPlayerPositions[0].isPositiveY ? farthestPlayerPositions[0].y : -farthestPlayerPositions[0].y;
            //float z = farthestPlayerPositions[0].isPositiveZ ? farthestPlayerPositions[0].z : -farthestPlayerPositions[0].z;


            //Debug.Log(farthestPlayerPositions[0].isPositiveX);
            //Debug.Log(farthestPlayerPositions[0].isPositiveY);
            //Debug.Log(farthestPlayerPositions[0].isPositiveZ);
            //Debug.Log(x);
            //Debug.Log(y);
            //Debug.Log(z);

            // Définir la position de la lumière
            //lightObject.transform.position = new Vector3(x, y, z);
        }





        foreach (Vector3 pos in positionsList)
        {
            averageLookAt += pos;
        }

        averageLookAt /= positionsList.Count;

        //Debug.Log("Position moyenne : " + averageLookAt);

        transform.LookAt(averageLookAt, Vector3.up);

        //GetComponent<Camera>().fieldOfView -= _zoomSpeed * Time.deltaTime;

        //float horizontalInput = Input.GetAxisRaw("Horizontal");
        //float verticalInput = Input.GetAxisRaw("Vertical");

        //Vector3 movement = new Vector3(0f, 0f, verticalInput) * _moveSpeed * Time.deltaTime;
        //transform.position = average;
    }



    //public float rotationSpeed = 6.0f; // Vitesse de rotation de la cam�ra

    //void Update()
    //{
    //    // Rotation autour du point cible
    //    transform.RotateAround(new Vector3(0.0f, 0.0f, 0.0f), Vector3.up, rotationSpeed * Time.deltaTime);
    //}
}
