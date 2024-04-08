using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Power : MonoBehaviour
{
    public Slider slider;
    private float powercurrentTime;
    private float startTime;
    public bool havePower;
    public float TimeToCharge;
    public float powerCreationTime;

    public enum PowerState
    {
        CanBeUse,
        Using,
        Used,
        Charching
    }

    public PowerState _powerState;
    // Start is called before the first frame update
    void Start()
    {
        SetPowerState(PowerState.CanBeUse);
    }

    // Update is called once per frame
    void Update()
    {
        powerState();
    }

    virtual public void LunchPower()
    {

    }

    public void powerState()
    {
        switch (_powerState)
        {
            case PowerState.CanBeUse:
                slider.value = 1;
                break;
            case PowerState.Using:
                if (!havePower)
                {
                    _powerState = PowerState.Used;
                }
                break;
            case PowerState.Used:
                //Plus tard avoir des verif
                startTime = Time.time;
                _powerState = PowerState.Charching;
                break;
            case PowerState.Charching:
                ChargePower();
                break;
        }
    }

    public void SetPowerState(PowerState newState)
    {
        _powerState = newState;
    }

    private void ChargePower()
    {
        if (powercurrentTime < TimeToCharge)
        {
            powercurrentTime = Time.time - startTime;
            slider.value = (powercurrentTime * 100f / TimeToCharge) / 100f;
        }
        else
        {
            slider.value = 1;
            havePower = true;
            _powerState = PowerState.CanBeUse;
            powercurrentTime = 0.0f;
        }
    }
}
