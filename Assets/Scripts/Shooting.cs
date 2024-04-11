using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using static Power;

public class Shooting : MonoBehaviour
{
    public Slider slider;
    private float shootcurrentTime;
    private float startTime;
    public bool haveshoot;
    public float TimeToCharge;

    public Transform playerTransform;
    public bool canShoot;

    public float _maxLaserRange = 100f;
    public float laserWidth = 0.1f;
    public Material laserMaterial;
    protected LineRenderer lineRenderer;

    public List<ShootingPower> shootingPowers;
    private bool haveAshootingPower;

    public LayerMask layerCollide;

    public void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = laserWidth;
        lineRenderer.endWidth = laserWidth;
        lineRenderer.material = laserMaterial;
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;

        // Shooting Components
        shootingPowers = new List<ShootingPower>();

        ShootingBasic basicShooting = gameObject.AddComponent<ShootingBasic>();
        shootingPowers.Add(basicShooting);

        haveAshootingPower = false;
        //ShootingPush pushShooting = gameObject.AddComponent<ShootingPush>();
        //shootingPowers.Add(pushShooting);

        //ShootingExplosion explosionShooting = gameObject.AddComponent<ShootingExplosion>();
        //shootingPowers.Add(explosionShooting);

        //ShootingCrossWall crossWallShooting = gameObject.AddComponent<ShootingCrossWall>();
        //shootingPowers.Add(crossWallShooting);
    }

    public void Update()
    {
        if (Input.GetButtonDown("Fire1") && canShoot && haveshoot)
        {
            FireLaser();
            if (haveAshootingPower) 
            {
                goBackToNormalShoot();
                haveAshootingPower=false;
            }
            haveshoot = false;
            startTime = Time.time;  
        }
        else if (!haveshoot) 
        {
            reload();
        }
    }

    void CreateDynamicLight(Vector3 position)
    {
        GameObject lightObject = new GameObject("DynamicLight");
        Light lightComponent = lightObject.AddComponent<Light>();

        lightComponent.type = LightType.Point;
        lightComponent.color = Color.white;
        lightComponent.range = 30f;
        lightComponent.intensity = 30f;


        lightObject.transform.position = new Vector3(position.x, position.y, position.z);

        Destroy(lightObject, 2f);
    }

    private void CreateParticule(Vector3 position)
    {
        GameObject particlePrefab = Resources.Load<GameObject>("Particule_05");
        GameObject particleObject = Instantiate(particlePrefab, position, Quaternion.identity);

        Destroy(particleObject, 2f);
    }

    private void PerformObjectTouch(RaycastHit hit)
    {
        CreateDynamicLight(hit.point);
        CreateParticule(hit.point);
        Renderer renderer = hit.collider.GetComponent<Renderer>();

        if (renderer != null)
        {
            Material material = renderer.material;
            Material oldMaterial = new Material(material);
            material.SetFloat("_Metallic", 0.5f);
            material.color = Color.black;
            material.SetFloat("_Glossiness", 0.2f);
            renderer.material = material;

            StartCoroutine(ResetMaterial(oldMaterial, renderer));
        }
    }

    private IEnumerator ResetMaterial(Material material, Renderer renderer)
    {
        yield return new WaitForSeconds(2f);

        Debug.Log("Reset le material de l'objet : " + renderer.gameObject.name);

        renderer.material = material;
    }

    private void PerformRaycast(RaycastHit hit, Vector3 startPoint, Vector3 endPoint)
    {
        Debug.Log("RAYCAST !!!!" + hit.collider.gameObject.name);



        endPoint = hit.point;

        Collider collider = hit.collider;

        if (collider != null && !hit.collider.gameObject.CompareTag("Shield"))
        {
            if (shootingPowers[0].GetType() == typeof(ShootingCrossWall))
            {
                if (hit.collider.gameObject.CompareTag("Obstacle"))
                {
                    PerformObjectTouch(hit);
                }
                RaycastHit[] hits = Physics.RaycastAll(endPoint, transform.forward, 100000000000000000f);
                foreach (RaycastHit oneHit in hits)
                {
                    if (oneHit.collider.gameObject.CompareTag("Obstacle"))
                    {
                        PerformObjectTouch(oneHit);
                    };

                    if (oneHit.collider.gameObject.CompareTag("PlayerControllable"))
                    {
                        endPoint = oneHit.point;
                        shootingPowers[0].OnCollision(oneHit.collider);
                        break;
                    }
                    endPoint = oneHit.point;
                }
            }
            else
            {
                Debug.Log("One collide push");
                shootingPowers[0].OnCollision(hit.collider);
            }
        }

        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
        lineRenderer.enabled = true;
        Invoke("DisableLaser", 0.1f);


        shootingPowers[0].PerformExplosion(endPoint);

    }


    public void PerformNoRaycast(RaycastHit hit, Vector3 startPoint, Vector3 endPoint)
    {
        //Debug.Log("NO RAYCAST !!!!");
        endPoint = startPoint + transform.forward * _maxLaserRange;

        if (shootingPowers[0].GetType() == typeof(ShootingCrossWall))
        {
            RaycastHit[] hits = Physics.RaycastAll(endPoint, transform.forward, 100000000000000000f);
            foreach (RaycastHit oneHit in hits)
            {
                endPoint = oneHit.point;
            }
        }


        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
        lineRenderer.enabled = true;

        Invoke("DisableLaser", 0.1f);
        //Debug.Log("We touch nothing but we perform Explode");

        shootingPowers[0].PerformExplosion(endPoint);


    }

    protected void FireLaser()
    {
        RaycastHit hit;
        Vector3 endPoint = Vector3.zero;

        Vector3 startPoint = new Vector3(transform.position.x, transform.position.y + 2.0f, transform.position.z);

        bool isRaycast = Physics.Raycast(startPoint, transform.forward, out hit, _maxLaserRange, layerCollide);

        if (isRaycast)
        {
            PerformRaycast(hit, startPoint, endPoint);
        }
        else
        {
            PerformNoRaycast(hit, startPoint, endPoint);
        }


    }

    protected void DisableLaser()
    {
        lineRenderer.enabled = false;
    }

    public void addPower(int Index)
    {
        shootingPowers.RemoveAt(0);
        if (Index == 1)
        {
            ShootingPush pushShooting = gameObject.AddComponent<ShootingPush>();
            shootingPowers.Add(pushShooting);
        }
        else if (Index == 2) 
        {
            ShootingExplosion explosionShooting = gameObject.AddComponent<ShootingExplosion>();
            shootingPowers.Add(explosionShooting);
        }
        else if (Index == 3)
        {
            ShootingCrossWall crossWallShooting = gameObject.AddComponent<ShootingCrossWall>();
            shootingPowers.Add(crossWallShooting);
        }  
        haveAshootingPower = true;
    }

    private void goBackToNormalShoot()
    {
        shootingPowers.RemoveAt(0);
        ShootingBasic basicShooting = gameObject.AddComponent<ShootingBasic>();
        shootingPowers.Add(basicShooting);
    }

    private void reload()
    {
        if (shootcurrentTime < TimeToCharge)
        {
            shootcurrentTime = Time.time - startTime;
            slider.value = (shootcurrentTime * 100f / TimeToCharge) / 100f;
        }
        else
        {
            slider.value = 1;
            shootcurrentTime = 0.0f;
            haveshoot = true;
        }
    }
}

