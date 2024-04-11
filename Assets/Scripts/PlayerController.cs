using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float _moveSpeed = 50f;
    public float _rotationSpeed = 700f;

    private List<Power> listOfPower;
    
    public enum PlayerState
    {
        Idle,
        Running,
        Shield,
        Dash,
        MiddleDead,
        Dead
    }

    public PlayerState _currentState;

    void Start()
    {
        SetPlayerState(PlayerState.Idle);
        animator.SetBool(Animator.StringToHash("isRunning"), false);
        listOfPower = new List<Power>();
        Shield shield = GetComponent<Shield>();
        listOfPower.Add(shield);
        DashPower dash = GetComponent<DashPower>();
        listOfPower.Add(dash);
    }

    public void playerState()
    {
        switch (_currentState)
        {
            case PlayerState.Idle:
                animator.SetBool(Animator.StringToHash("isRunning"), false);
                break;
            case PlayerState.Running:
                animator.SetBool(Animator.StringToHash("isRunning"), true);
                break;
            case PlayerState.Shield:
                animator.SetBool(Animator.StringToHash("isRunning"), false);
                break;
            case PlayerState.Dash:
                animator.SetBool(Animator.StringToHash("isRunning"), true);
                break;
            case PlayerState.Dead:
                {
                    //Debug.Log("Un joueur est mort" + "C'est le joueur " + gameObject.name);
                    animator.CrossFade("Death", 0f);
                    SetPlayerState(PlayerState.Idle);
                    StartCoroutine(RoundSystem.SetRoundChange());
                    
                }
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
