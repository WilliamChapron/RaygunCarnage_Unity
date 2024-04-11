using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Power
{
    public GameObject ShieldPrefab;
    public Transform playerTransform;
    private GameObject shieldInstance;

    void Start()
    {
        havePower = true;
    }

    void Update()
    {
        if (shieldInstance != null && NoMoreShield(1f))
        {
            havePower = false;
            DestroyShield();
        }
    }

    override public void LunchPower()
    {
        if (_powerState == PowerState.CanBeUse )
        {
            SetPowerState(PowerState.Using);
            shieldInstance = Instantiate(ShieldPrefab, playerTransform.position + new Vector3(0.0f, playerTransform.position.y + 1f), Quaternion.identity);
            shieldInstance.SetActive(true);
            powerCreationTime = Time.time;
            havePower = true;
        }
    }

    private bool NoMoreShield(float time)
    {
        return Time.time - powerCreationTime >= time;
    }

    private void DestroyShield()
    {
        Destroy(shieldInstance);
        shieldInstance = null;
    }
}
