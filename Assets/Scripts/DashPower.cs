using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPower : Power
{
    public float DashSpeed = 3f;
    public float DashTravelTime = 0.3f;

    public PlayerController PC;
    private bool hasBeenDestroy;
    [SerializeField] TrailRenderer tr;

    // Start is called before the first frame update 
    void Start()
    {
        hasBeenDestroy = true;
        havePower = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (NoMoreDash(0.3f))
        {
            if (!hasBeenDestroy)
            {
                tr.emitting = false;
                havePower = false;
                PC._moveSpeed /= DashSpeed;
                hasBeenDestroy=true;
            }  
        }
    }

    override public void LunchPower()
    {
        if (_powerState == PowerState.CanBeUse)     
        {
            tr.emitting = true;
            SetPowerState(PowerState.Using);
            powerCreationTime = Time.time;
            havePower = true;
            PC._moveSpeed *= DashSpeed;
            hasBeenDestroy = false;
        }
    }

    private bool NoMoreDash(float time)
    {
        return Time.time - powerCreationTime >= time;
    }
}
