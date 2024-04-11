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

    public virtual void PerformExplosion(Vector3 endPoint)
    {

    }

    public virtual void PerformDamage(Collider collider)
    {
        PlayerController._currentState = PlayerController.PlayerState.Dead;
    }
}


public class ShootingBasic : ShootingPower
{
    public override void OnCollision(Collider collider)
    {
        PerformDamage(collider);
    }
}


public class ShootingPush : ShootingPower
{
    public override void OnCollision(Collider collider)
    {
        //Debug.Log("Perform Shooting PUSH");
        Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {


            Transform trailObj = collider.gameObject.transform.Find("Trail");
            
            TrailRenderer trailRenderer = trailObj.GetComponent<TrailRenderer>();


            Color originalColor = trailRenderer.startColor;
            // Définir la nouvelle couleur (rouge) pour le TrailRenderer
            Color newColor = Color.red;
            trailRenderer.startColor = newColor;
            trailRenderer.endColor = newColor;

            trailRenderer.emitting = true;
            //Debug.Log(trailTransform.name);

            StartCoroutine(ApplyForceOverDuration(rb, collider.gameObject.transform.position, trailRenderer, originalColor));

        }
        PerformDamage(collider);
    }

    private IEnumerator ApplyForceOverDuration(Rigidbody rb, Vector3 targetPosition, TrailRenderer trailRenderer, Color originalColor)
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
        trailRenderer.startColor = originalColor;
        trailRenderer.endColor = originalColor;
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
            Debug.LogError("Le GameObject instanci� ne contient pas de composant Particle System !");
        }
    }

    public override void OnCollision(Collider collider)
    {

        PerformDamage(collider);
    }
}

public class ShootingCrossWall : ShootingPower
{

    public override void PerformExplosion(Vector3 endPoint)
    {
    }

    public override void OnCollision(Collider collider)
    {
        PerformDamage(collider);
    }
}