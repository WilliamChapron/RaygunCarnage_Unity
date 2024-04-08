using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingPower : MonoBehaviour
{
    protected Transform playerTransform;

    public ShootingPower(Transform transform)
    {
        playerTransform = transform;
        Debug.Log("Transform?" + playerTransform.gameObject.name);
    }

    public virtual void OnCollision(Collider collider)
    {

    }
}


public class ShootingPush : ShootingPower
{
    public ShootingPush(Transform transform) : base(transform)
    {

    }

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

        Vector3 forceDirection = (targetPosition - playerTransform.position).normalized;

        for (int i = 0; i < numIterations; i++)
        {
            Debug.Log("Apply force");
            rb.AddForce(forceDirection * forceMagnitudePerIteration, ForceMode.Force);
            yield return new WaitForSeconds(duration / numIterations);
        }
    }
}
