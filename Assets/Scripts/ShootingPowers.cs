using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Unity.VisualScripting;
using UnityEngine;
using static PlayerController;

public class ShootingPower : MonoBehaviour
{
    public int index;
    public virtual void OnCollision(Collider collider)
    {
    }

    public virtual void PerformExplosion(Vector3 endPoint)
    {

    }

    public virtual void PerformDamage(Collider collider)
    {
        if (collider.gameObject.CompareTag("PlayerControllable"))
        {
            Player1Controller Pco = collider.gameObject.GetComponent<Player1Controller>();
            if (Pco == null)
            {
                Player2Controller Pct = collider.gameObject.GetComponent<Player2Controller>();
                
                if (Pct._currentState != PlayerState.Shield)
                {
                    
                    if (Pct.middledead == true)
                    {
                        StartCoroutine(RoundSystem.SetRoundChange(Pct.gameObject));
                        Pct.SetPlayerState(PlayerState.Dead);
                    }
                    else
                    {
                        Pct.middledead = true;
                        GameObject lightObject = new GameObject("BrightLight");

                        Light lightComponent = lightObject.AddComponent<Light>();
                        lightComponent.color = Color.red;
                        lightComponent.intensity = 50f;

                        lightObject.transform.position = collider.gameObject.transform.position;

                        Destroy(lightObject, 0.5f);
                    }
                }

            }
            else
            {
                if (Pco._currentState != PlayerState.Shield)
                {
                    
                    if (Pco.middledead == true)
                    {
                        StartCoroutine(RoundSystem.SetRoundChange(Pco.gameObject));
                        Pco.SetPlayerState(PlayerState.Dead);
                    }
                    else
                    {
                        Pco.middledead = true;
                        GameObject lightObject = new GameObject("BrightLight");

                        Light lightComponent = lightObject.AddComponent<Light>();
                        lightComponent.color = Color.red;
                        lightComponent.intensity = 50f;

                        lightObject.transform.position = collider.gameObject.transform.position;

                        Destroy(lightObject, 0.5f);
                    }
                }
            }

        }
    }
}


public class ShootingBasic : ShootingPower
{
    private void Start()
    {
        this.index = 0;
    }

    public override void OnCollision(Collider collider)
    {
        PerformDamage(collider);
    }
}


public class ShootingPush : ShootingPower
{
    private void Start()
    {
        this.index = 1;
    }
    public override void OnCollision(Collider collider)
    {
        //Debug.Log("Perform Shooting PUSH");
        Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();


        Transform trailObj = null;
        TrailRenderer trailRenderer = null;
        Color originalColor = Color.white;

        if (collider.gameObject.CompareTag("PlayerControllable")) {
            trailObj = collider.gameObject.transform.Find("Trail");
            trailRenderer = trailObj.GetComponent<TrailRenderer>();
            originalColor = trailRenderer.startColor;
            trailRenderer.emitting = true;
        }
        

        if (rb != null && trailRenderer != null)
        {
            StartCoroutine(ApplyForceOverDuration(rb, collider.gameObject.transform.position, trailRenderer));
        }
        PerformDamage(collider);
    }

    private IEnumerator ApplyForceOverDuration(Rigidbody rb, Vector3 targetPosition, TrailRenderer trailRenderer)
    {
        float totalForceMagnitude = 20000.0f;
        float duration = 1.0f;
        int numIterations = 50;

        float forceMagnitudePerIteration = totalForceMagnitude / numIterations;

        Vector3 forceDirection = (targetPosition - transform.position).normalized;

        for (int i = 0; i < numIterations; i++)
        {
            //Debug.Log("Apply force");
            rb.AddForce(forceDirection * forceMagnitudePerIteration, ForceMode.Force);
            yield return new WaitForSeconds(duration / numIterations);
        }

        trailRenderer.emitting = false;
    }

    public override void PerformExplosion(Vector3 endPoint)
    {

    }
}

public class ShootingExplosion : ShootingPower
{
    GameObject particlePrefab;
    public void Start()
    {
        particlePrefab = Resources.Load<GameObject>("Particule_02");
        this.index = 2;
    }


    public override void PerformExplosion(Vector3 endPoint)
    {
        //Debug.Log("Perform Shooting Explode");

        GameObject particleObject = Instantiate(particlePrefab, endPoint, Quaternion.identity);
        ParticleSystem particleSystem = particleObject.GetComponent<ParticleSystem>();

        particleObject.GetComponent<ColliderComponent>().SetPlayerName(gameObject.name);

        if (particleSystem != null)
        {
            particleSystem.Play();
            //Debug.Log("Play particule");
        }
        else
        {
            Debug.LogError("Le GameObject instancie ne contient pas de composant Particle System !");
        }
    }

    public override void OnCollision(Collider collider)
    {

        PerformDamage(collider);
    }
}

public class ShootingCrossWall : ShootingPower
{
    private void Start()
    {
        this.index = 3;
    }
    public override void PerformExplosion(Vector3 endPoint)
    {
    }

    public override void OnCollision(Collider collider)
    {
        PerformDamage(collider);
    }
}