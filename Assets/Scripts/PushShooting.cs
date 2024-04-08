using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingPower : MonoBehaviour
{

    public virtual void OnCollision(Collider collider)
    {

    }
}


public class ShootingPush : ShootingPower
{

    public override void OnCollision(Collider collider)
    {
        HealthComponent healthComponent = collider.gameObject.GetComponent<HealthComponent>();

        Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            StartCoroutine(ApplyForceOverDuration(rb, collider.gameObject.transform.position));
        }

        if (healthComponent != null)
        {
            healthComponent.TakeDamage(20);
            //Debug.Log("Le laser a touché cet objet : " + collider.name);
        }
    }

    private IEnumerator ApplyForceOverDuration(Rigidbody rb, Vector3 targetPosition)
    {
        float totalForceMagnitude = 20000.0f;
        float duration = 1.0f;
        int numIterations = 50;

        float forceMagnitudePerIteration = totalForceMagnitude / numIterations;

        Vector3 forceDirection = (targetPosition - transform.position).normalized;

        for (int i = 0; i < numIterations; i++)
        {
            Debug.Log("Apply force");
            rb.AddForce(forceDirection * forceMagnitudePerIteration, ForceMode.Force);
            yield return new WaitForSeconds(duration / numIterations);
        }
    }
}

public class ShootingExplosion : ShootingPower
{

    GameObject particlePrefab;
    public void Start()
    {
        particlePrefab = Resources.Load<GameObject>("Hit_02");
    }

    public void PerformExplosion(Vector3 endPoint)
    {
        Debug.Log(particlePrefab.name);

        GameObject particleObject = Instantiate(particlePrefab, endPoint, Quaternion.identity);
        ParticleSystem particleSystem = particleObject.GetComponent<ParticleSystem>();

        //SphereExplode

        if (particleSystem != null)
        {
            particleSystem.Play();
            //Debug.Log("Play particule");
        }
        else
        {
            Debug.LogError("Le GameObject instancié ne contient pas de composant Particle System !");
        }
    }

    public override void OnCollision(Collider collider)
    {

        HealthComponent healthComponent = collider.gameObject.GetComponent<HealthComponent>();
        if (healthComponent != null)
        {
            healthComponent.TakeDamage(20);
        }
    }
}