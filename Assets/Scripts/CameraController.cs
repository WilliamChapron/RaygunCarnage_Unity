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

    private float basePositionY;

    private float oldYPos;

    void Start()
    {
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;

        basePositionY = transform.position.y;
        oldYPos = transform.position.y;
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


        


        foreach (Vector3 pos in positionsList)
        {
            averageLookAt += pos;
        }
        averageLookAt /= positionsList.Count;



        //if (newFOV > 10.0f)
        //{
        //    float newYPosition = newFOV;
        //    cameraPosition = new Vector3(averageLookAt.x - 15f, basePositionY + (newYPosition/2), averageLookAt.z);
        //    if (cameraPosition.y >= basePositionY + 10f)
        //    {
        //        cameraPosition.y = basePositionY + 10f;
        //    }
        //    transform.position = Vector3.Lerp(transform.position, cameraPosition, _moveSpeed * Time.deltaTime);
        //    oldYPos = newYPosition;
        //}
        //else
        //{
        //    float newYPosition = newFOV;
        //    cameraPosition = new Vector3(averageLookAt.x - 15f, basePositionY + oldYPos, averageLookAt.z);
        //    if (cameraPosition.y >= basePositionY + 10f)
        //    {
        //        cameraPosition.y = basePositionY + 10f;
        //    }
        //    transform.position = Vector3.Lerp(transform.position, cameraPosition, _moveSpeed * Time.deltaTime);
        //}


        float distance = Vector3.Distance(playerObjects[0].transform.position, playerObjects[1].transform.position);
        float newFOV = distance * 0.3f;

        Debug.Log("FOV : " + newFOV);

        float fovCoefficient = Mathf.Clamp(newFOV / 10f, 0.1f, 1.0f); // Coeff Multiplicator for Distance calcul because it zoom less when it's far, so it's embarassing

        float targetYPosition = basePositionY + (newFOV * fovCoefficient); // Appliquer le coefficient

        // Lerp Before set 
        float smoothedYPosition = Mathf.Lerp(transform.position.y, targetYPosition, _moveSpeed * Time.deltaTime);
        Vector3 cameraPosition = new Vector3(averageLookAt.x - 17f, smoothedYPosition, averageLookAt.z);

        // Limit caméra positioning in Y
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, basePositionY - 50f, basePositionY + 15f);

        Debug.Log("Camera Y : " + cameraPosition.y);

        transform.position = cameraPosition;


    }
}
