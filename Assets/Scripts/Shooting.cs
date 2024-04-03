using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject projectilePrefab; 
    public Transform playerTransform; 
    public float projectileSpeed = 30f; 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot(); 
        }
    }

    void Shoot()
    {

        GameObject projectile = Instantiate(projectilePrefab, playerTransform.position + new Vector3(0.5f, 2.0f, 0.5f), playerTransform.rotation * Quaternion.Euler(90f, 0f, 0f));

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        rb.velocity = playerTransform.forward * projectileSpeed;

    }
}

