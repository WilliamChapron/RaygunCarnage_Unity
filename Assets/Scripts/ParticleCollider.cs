using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticuleCollider : MonoBehaviour
{
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {



    Debug.Log("Collision de la particule avec" + other.name);


        
    }
}