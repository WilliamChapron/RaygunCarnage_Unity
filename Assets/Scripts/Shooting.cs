using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform playerTransform;
    public float projectileSpeed = 30f;

    PlayerInput input;

    void Awake()
    {
        input = new PlayerInput();

        input.CharacterControls.Shoot.started += _ => Shoot();
    }

    void OnEnable()
    {
        input.CharacterControls.Enable();
    }

    void OnDisable()
    {
        input.CharacterControls.Disable();
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, playerTransform.position + new Vector3(0.5f, 2.0f, 0.5f), playerTransform.rotation * Quaternion.Euler(90f, 0f, 0f));
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = playerTransform.forward * projectileSpeed;
    }
}
