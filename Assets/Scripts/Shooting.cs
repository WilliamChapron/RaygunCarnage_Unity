using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject projectilePrefab; 
    public Transform playerTransform;
    public bool canShoot;
    public float projectileSpeed = 30f;

    public float _maxLaserRange = 100f; 
    public float laserWidth = 0.1f; 
    public Material laserMaterial; 
    private LineRenderer lineRenderer;


    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = laserWidth;
        lineRenderer.endWidth = laserWidth;
        lineRenderer.material = laserMaterial;
        lineRenderer.positionCount = 2; 
        lineRenderer.enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (canShoot) {
                FireLaser();
            }
        }
    }

    //void Shoot()
    //{

    //    GameObject projectile = Instantiate(projectilePrefab, playerTransform.position + new Vector3(0.0f, 1.0f, 1.5f), playerTransform.rotation * Quaternion.Euler(90f, 0f, 0f));
    //    projectile.SetActive(true);
    //    Rigidbody rb = projectile.GetComponent<Rigidbody>();

    //    rb.velocity = playerTransform.forward * projectileSpeed;

    //}



    void FireLaser()
    {
        RaycastHit hit;
        Vector3 endPoint;

        Vector3 startPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        if (Physics.Raycast(startPoint, transform.forward, out hit, _maxLaserRange))
        {
            endPoint = hit.point;
            Collider collider = hit.collider;
            if (collider != null)
            {
                Debug.Log("Objet touch� : " + collider.gameObject.name);

                ColliderComponent colliderComponent = gameObject.GetComponent<ColliderComponent>();

                colliderComponent.OnLaserCollision();

                //Collision collision = new Collision
                //{
                //    collider = colliderComponent
                //};

                //if (colliderComponent != null)
                //{
                //    colliderComponent.OnCollisionEnter(new Collision(collider));
                //}
            }
        }
        else
        {
            endPoint = startPoint + transform.forward * _maxLaserRange;
        }

        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);


        lineRenderer.enabled = true;


        Invoke("DisableLaser", 0.1f);
    }

    void DisableLaser()
    {
        lineRenderer.enabled = false;
    }
}

