using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class CameraController : MonoBehaviour
{
    public float _moveSpeed = 5f;
    private float _zoomSpeed = 5.0f;

    public float updateInterval = 100f; 
    private float lastUpdateTime = 0f; 

    void Start()
    {
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    public Transform target;

    void Update()
    {

        List<Vector3> positionsList = new List<Vector3>();



        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("PlayerControllable");

        foreach (GameObject obj in playerObjects)
        {
            //Debug.Log(obj.name);
            positionsList.Add(obj.transform.position);
        }

        Vector3 averageLookAt = Vector3.zero;
        // Get most close & far axes and store them in two vect

        float distance = Vector3.Distance(playerObjects[0].transform.position, playerObjects[1].transform.position);
        float newFOV = distance * 2f; // Inverse de la distance
        foreach (Vector3 pos in positionsList)
        {
            averageLookAt += pos;
        }
        averageLookAt /= positionsList.Count;

        //transform.LookAt(averageLookAt, Vector3.up);
        float newYPosition = gameObject.transform.position.y;
        Vector3 cameraPosition = new Vector3(averageLookAt.x - 15f, newYPosition, averageLookAt.z); ;
        if (newFOV > 25.0f)
        {
            newYPosition = newFOV;
            cameraPosition = new Vector3(cameraPosition.x, newYPosition, cameraPosition.z); ; ;
        }

        transform.position = Vector3.Lerp(transform.position, cameraPosition, _moveSpeed * Time.deltaTime);
    }
}
