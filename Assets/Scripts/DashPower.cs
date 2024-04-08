using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPower : Power
{
    public float DashSpeed = 3f;
    public float DashTravelTime = 0.3f;

    private PlayerController PC;

    [SerializeField] TrailRenderer tr;

    // Start is called before the first frame update 
    void Start()
    {
        havePower = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (NoMoreDash(0.3f))
        {
            havePower = false;
        }
    }

    override public void LunchPower()
    {
        SetPowerState(PowerState.Using);
        powerCreationTime = Time.time;
        havePower = true;
        PC._moveSpeed *= DashSpeed;
    }

    private bool NoMoreDash(float time)
    {
        return Time.time - powerCreationTime >= time;
    }
}
