using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float  _moveSpeed = 5f;
    public float _rotationSpeed = 600f;

    private List<Power> listOfPower;

    public enum PlayerState
    {
        Idle,
        Running,
        Shield,
        Dash,
        Dead
    }

    public PlayerState _currentState;

    void Start()
    {
        SetPlayerState(PlayerState.Idle);
        listOfPower = new List<Power>();
        Shield shield = GetComponent<Shield>();
        listOfPower.Add(shield);
        DashPower dash = GetComponent<DashPower>();
        listOfPower.Add(dash);
    }

    private void playerState()
    {
        switch (_currentState)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Running:
                break;
            case PlayerState.Shield:
                break;
            case PlayerState.Dash:
                break;
            case PlayerState.Dead:
                 Debug.Log("Un joueur est mort" + "C'est le joueur " + gameObject.name);
                 RoundSystem.End = true;
                 break;
        }
    }

    public void SetPlayerState(PlayerState newState)
    {
        _currentState = newState;
    }

    private void Control(Power power)
    {
        if (_currentState != PlayerState.Shield)
        {
            if (power == listOfPower[0] && power._powerState != Power.PowerState.Using)
            {
                SetPlayerState(PlayerState.Shield);
                power.LunchPower();
            }
            else if (power == listOfPower[1])
            {
                SetPlayerState(PlayerState.Dash);
                power.LunchPower();
            }
        } 
    }  

    private void UpdatePowerState()
    {
        for (int i = 0; i < listOfPower.Count; i++)
        {
            if (listOfPower[i]._powerState == Power.PowerState.Used)
            {
                SetPlayerState(PlayerState.Idle);
            }
            Control(listOfPower[i]);
            listOfPower[i].powerState();
        }
    }

    public void Update()
    {
        UpdatePowerState();
        playerState();
    }
}
