using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform playerTransform;
    public bool canShoot;
    public float projectileSpeed = 30f;

    public float _maxLaserRange = 50f;
    public float laserWidth = 0.1f;
    public Material laserMaterial;
    protected LineRenderer lineRenderer;

    public List<ShootingPower> shootingPowers;

    protected virtual void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = laserWidth;
        lineRenderer.endWidth = laserWidth;
        lineRenderer.material = laserMaterial;
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;

        // Shooting Components
        shootingPowers = new List<ShootingPower>();

        //ShootingPush pushShooting = gameObject.AddComponent<ShootingPush>();
        //shootingPowers.Add(pushShooting);

        ShootingExplosion explosionShooting = gameObject.AddComponent<ShootingExplosion>();
        shootingPowers.Add(explosionShooting);
    }

    protected virtual void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (canShoot)
            {
                FireLaser();
            }
        }
    }

    protected void FireLaser()
    {
        RaycastHit hit;
        Vector3 endPoint;

        Vector3 startPoint = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);

        if (Physics.Raycast(startPoint, transform.forward, out hit, _maxLaserRange))
        {
            endPoint = hit.point;
            Collider collider = hit.collider;
            if (collider != null)
            {
                Debug.Log("Objet touché : " + collider.gameObject.name);
                shootingPowers[0].OnCollision(hit.collider);
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

        if (shootingPowers[0].GetType() == typeof(ShootingExplosion))
        {
            ShootingExplosion explosionPower = (ShootingExplosion)shootingPowers[0];
            explosionPower.PerformExplosion(endPoint);
        }
        
    }

    protected void DisableLaser()
    {
        lineRenderer.enabled = false;
    }
}

