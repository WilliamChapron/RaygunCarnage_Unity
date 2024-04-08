using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 directionToCamera = Camera.main.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(directionToCamera, Vector3.up);
    }
}
