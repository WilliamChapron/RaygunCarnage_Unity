using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab du projectile � tirer
    public Transform firePoint; // Point de d�part du tir
    public float projectileSpeed = 10f; // Vitesse du projectile

    // Update is called once per frame
    void Update()
    {
        // Si la touche de tir est enfonc�e (par exemple, la touche gauche de la souris)
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot(); // Appeler la fonction de tir
        }
    }

    void Shoot()
    {
        // Instancier le projectile � partir du prefab au point de d�part du tir
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Obtenir le Rigidbody du projectile
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        // V�rifier si le Rigidbody existe
        if (rb != null)
        {
            // Appliquer une v�locit� au projectile dans la direction du firePoint
            rb.velocity = firePoint.forward * projectileSpeed;
        }
        else
        {
            Debug.LogWarning("Le prefab du projectile ne contient pas de composant Rigidbody.");
        }
    }
}

