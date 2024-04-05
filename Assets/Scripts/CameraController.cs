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

    //public struct PositionInfo
    //{
    //    public float x;
    //    public float y;
    //    public float z;
    //}

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

        //if (Time.time - lastUpdateTime > updateInterval)
        //{
        //    // Exécutez votre code ici

        //    // Mettez à jour le temps de la dernière mise à jour
        //    lastUpdateTime = Time.time;
        //}

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
        List<Vector3> farthestPlayerPositions = new List<Vector3>();
        farthestPlayerPositions.Add(new Vector3(0.0f, 0.0f, 0.0f));
        farthestPlayerPositions.Add(new Vector3(0.0f, 0.0f, 0.0f));


        // sa prend 7 
        int i = 0;
        foreach (Vector3 pos in secondPositionsList)
        {
            Debug.Log("ITERATION : " + i);

            Vector3 position = farthestPlayerPositions[0];
            if (Mathf.Abs(pos.x) >= Mathf.Abs(farthestPlayerPositions[0].x))
            {
                Debug.Log("Pos du player supérieur a pos du tableau, regarde ->  " + Mathf.Abs(pos.x) + " > " + Mathf.Abs(farthestPlayerPositions[0].x));
                position.x = pos.x;
            }
            else
            {
                Debug.Log("Pos du player inferieur a celle du tableau, regarde ->  " + Mathf.Abs(pos.x) + " < " + Mathf.Abs(farthestPlayerPositions[0].x));
            }

            if (Mathf.Abs(pos.y) >= Mathf.Abs(farthestPlayerPositions[0].y))
            {
                Debug.Log("Pos du player supérieur a pos du tableau, regarde ->  " + Mathf.Abs(pos.y) + " > " + Mathf.Abs(farthestPlayerPositions[0].y));
                position.y = pos.y;
            }
            else
            {
                Debug.Log("Pos du player inferieur a celle du tableau, regarde ->  " + Mathf.Abs(pos.y) + " < " + Mathf.Abs(farthestPlayerPositions[0].y));
            }

            if (Mathf.Abs(pos.z) >= Mathf.Abs(farthestPlayerPositions[0].z))
            {
                Debug.Log("Pos du player supérieur a pos du tableau, regarde ->  " + Mathf.Abs(pos.z) + " > " + Mathf.Abs(farthestPlayerPositions[0].z));

                position.z = pos.z;
            }
            else
            {
                Debug.Log("Pos du player inferieur a celle du tableau, regarde ->  " + Mathf.Abs(pos.z) + " < " + Mathf.Abs(farthestPlayerPositions[0].z));
            }

            farthestPlayerPositions[0] = position;

            Debug.Log("farthestPlayerPositions après mise à jour : " + farthestPlayerPositions[0]);
            i++;
        }

        foreach (Vector3 pos in secondPositionsList)
        {
            Debug.Log("ITERATION : " + i);

            Vector3 position = farthestPlayerPositions[1];
            if (Mathf.Abs(pos.x) >= Mathf.Abs(farthestPlayerPositions[1].x))
            {
                Debug.Log("Pos du player supérieur a pos du tableau, regarde ->  " + Mathf.Abs(pos.x) + " > " + Mathf.Abs(farthestPlayerPositions[1].x));
                if (pos.x != farthestPlayerPositions[0].x)
                {
                    position.x = pos.x;
                }   
            }
            else
            {
                Debug.Log("Pos du player inferieur a celle du tableau, regarde ->  " + Mathf.Abs(pos.x) + " < " + Mathf.Abs(farthestPlayerPositions[1].x));
            }

            if (Mathf.Abs(pos.y) >= Mathf.Abs(farthestPlayerPositions[1].y))
            {
                Debug.Log("Pos du player supérieur a pos du tableau, regarde ->  " + Mathf.Abs(pos.y) + " > " + Mathf.Abs(farthestPlayerPositions[1].y));
                if (pos.y != farthestPlayerPositions[0].y)
                {
                    position.y = pos.y;
                }
            }
            else
            {
                Debug.Log("Pos du player inferieur a celle du tableau, regarde ->  " + Mathf.Abs(pos.y) + " < " + Mathf.Abs(farthestPlayerPositions[1].y));
            }

            if (Mathf.Abs(pos.z) >= Mathf.Abs(farthestPlayerPositions[1].z))
            {
                Debug.Log("Pos du player supérieur a pos du tableau, regarde ->  " + Mathf.Abs(pos.z) + " > " + Mathf.Abs(farthestPlayerPositions[1].z));
                if (pos.z != farthestPlayerPositions[0].z)
                {
                    position.z = pos.z;
                }
                
            }
            else
            {
                Debug.Log("Pos du player inferieur a celle du tableau, regarde ->  " + Mathf.Abs(pos.z) + " < " + Mathf.Abs(farthestPlayerPositions[1].z));
            }

            farthestPlayerPositions[1] = position;

            //Debug.Log("farthestPlayerPositions après mise à jour : " + farthestPlayerPositions[1]);
            i++;
        }






        //Debug.Log("La première valeur la plus grande en x est " + farthestPlayerPositions[0].x);
        //Debug.Log("La première valeur la plus grande en y est " + farthestPlayerPositions[0].y);
        //Debug.Log("La première valeur la plus grande en z est " + farthestPlayerPositions[0].z);

        //Debug.Log("La deuxième valeur la plus grande en x est " + farthestPlayerPositions[1].x);
        //Debug.Log("La deuxième valeur la plus grande en y est " + farthestPlayerPositions[1].y);
        //Debug.Log("La deuxième valeur la plus grande en z est " + farthestPlayerPositions[1].z);







        if (Input.GetKeyDown(KeyCode.E))
        {

            GameObject lightObject = new GameObject("BrightLight");
            Light lightComponent = lightObject.AddComponent<Light>();
            lightComponent.color = Color.red;
            lightComponent.intensity = 50f;

            // Définir la position de la lumière
            lightObject.transform.position = new Vector3(farthestPlayerPositions[0].x, farthestPlayerPositions[0].y, farthestPlayerPositions[0].z);

            GameObject lightObject2 = new GameObject("BrightLight2");
            Light lightComponent2 = lightObject2.AddComponent<Light>();
            lightComponent2.color = Color.blue;
            lightComponent2.intensity = 50f;

            // Définir la position de la lumière
            lightObject.transform.position = new Vector3(farthestPlayerPositions[1].x, farthestPlayerPositions[1].y, farthestPlayerPositions[1].z);
        }


        float distance = Vector3.Distance(farthestPlayerPositions[0], farthestPlayerPositions[1]);
        float newFOV = distance * 1f; // Inverse de la distance
        Debug.Log(" Fov " + newFOV);
        GetComponent<Camera>().fieldOfView = newFOV;
        Debug.Log("ddd");


        foreach (Vector3 pos in positionsList)
        {
            averageLookAt += pos;
        }

        averageLookAt /= positionsList.Count;

        Debug.Log("Position moyenne : " + averageLookAt);
        Debug.Log("Position PLAYER 1 : " + playerObjects[0].transform.position);
        Debug.Log("Position PLAYER 2 : " + playerObjects[1].transform.position);

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
