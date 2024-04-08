using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject ShieldPrefab;
    public Transform playerTransform;
    private GameObject shieldInstance;
    private float shieldCreationTime;
    public bool haveShield;
    public float chargingTime;

    void Start()
    {
        haveShield = true;
    }

    void Update()
    {
        if (shieldInstance != null && NoMoreShield(1f))
        {
            haveShield = false;
            DestroyShield();
        }
    }

    public void CreateShield()
    {
        shieldInstance = Instantiate(ShieldPrefab, playerTransform.position + new Vector3(0.0f, playerTransform.position.y + 1f), Quaternion.identity);
        shieldInstance.SetActive(true);
        shieldCreationTime = Time.time;
        haveShield = true;
    }

    private bool NoMoreShield(float time)
    {
        return Time.time - shieldCreationTime >= time;
    }

    private void DestroyShield()
    {
        Destroy(shieldInstance);
        shieldInstance = null;
    }

    
}
