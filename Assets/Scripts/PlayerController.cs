using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float _moveSpeed = 9f;
    public float _rotationSpeed = 700f;
    public bool middledead;

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
        middledead = false;
    }

    public void playerState()
    {
        switch (_currentState)
        {
            case PlayerState.Idle:
                animator.CrossFade("Idle", 0f);
                break;
            case PlayerState.Running:
                animator.CrossFade("Fight", 0f);
                break;
            case PlayerState.Shield:
                animator.CrossFade("Idle", 0f);
                break;
            case PlayerState.Dash:
                animator.CrossFade("Fight", 0f);
                break;
            case PlayerState.Dead:
                animator.CrossFade("Death", 0f);
                break;

        }
    }

    public void SetPlayerState(PlayerState newState)
    {
        _currentState = newState;
    }

    public void shieldPower()
    {
        if (_currentState != PlayerState.Shield)
        {
            SetPlayerState(PlayerState.Shield);
            listOfPower[0].LunchPower();
        }
    }

    public void dashPower()
    {
        if (_currentState != PlayerState.Dash)
        {
            SetPlayerState(PlayerState.Dash);
            listOfPower[1].LunchPower();
        }
    }

    public void shootingAction()
    {
        Shooting myShooting = GetComponent<Shooting>();
        myShooting.FireLaser();
    }

    public void UpdatePowerState()
    {
        for (int i = 0; i < listOfPower.Count; i++)
        {
            if (listOfPower[i]._powerState == Power.PowerState.Used)
            {
                SetPlayerState(PlayerState.Idle);
            }
            listOfPower[i].powerState();
        }
    }
}
