using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float _moveSpeed = 5f;
    public float _rotationSpeed = 600f;
    private float _lastInputTime;
    public List<KeyCode> _movementKeys;
    
    private Shield playerShield; 
    public Slider slider;
    private float powercurrentTime;
    private float startTime;

    public enum PlayerState
    {
        Idle,
        Running,
        Shield
    }

    private PlayerState _currentState;

    public enum PowerState
    {
        CanBeUse,
        Used,
        Charching
    }

    private PowerState _powerState1;
    private PowerState _powerState2;

    void Start()
    {
        SetPlayerState(PlayerState.Idle);
        SetPowerState(PowerState.CanBeUse , _powerState1);
        SetPowerState(PowerState.CanBeUse, _powerState2);
        playerShield = GetComponent<Shield>();
    }

    private void playerState()
    {
        switch (_currentState)
        {
            case PlayerState.Idle:
                animator.CrossFade("Idle", 0f);
                if (Input.GetKey(_movementKeys[0]) || Input.GetKey(_movementKeys[1]) || Input.GetKey(_movementKeys[2]) || Input.GetKey(_movementKeys[3]))
                {
                    SetPlayerState(PlayerState.Running);
                }
                break;
            case PlayerState.Running:
                Movement();
                animator.CrossFade("Running", 0f);
                if (!Input.GetKey(_movementKeys[0]) && !Input.GetKey(_movementKeys[1]) && !Input.GetKey(_movementKeys[2]) && !Input.GetKey(_movementKeys[3]))
                {
                    SetPlayerState(PlayerState.Idle);
                }
                break;
            case PlayerState.Shield:
                animator.CrossFade("Shield", 0f);
                break;
        }
    }

    private void SetPlayerState(PlayerState newState)
    {
        _currentState = newState;
    }

    private void powerState()
    {
        switch (_powerState1)
        {
            case PowerState.CanBeUse:
                if (!playerShield.haveShield)
                {
                    _powerState1 = PowerState.Used;
                }
                else
                {
                    slider.value = 1;
                    shieldControl();
                }
                break;
            case PowerState.Used:
                //Plus tard avoir des verif
                startTime = Time.time;
                _powerState1 = PowerState.Charching;
                SetPlayerState(PlayerState.Idle);
                break;
            case PowerState.Charching:
                ChargePower();
                break;
        }
    }

    private void SetPowerState(PowerState newState, PowerState powerState)
    {
        powerState = newState;
    }

    private void ChargePower()
    {
        if (powercurrentTime < playerShield.chargingTime)
        {
            powercurrentTime = Time.time - startTime;
            slider.value = (powercurrentTime * 100f / playerShield.chargingTime) / 100f;
        }
        else
        {
            slider.value = 1;
            playerShield.haveShield = true;
            _powerState1 = PowerState.CanBeUse;
            powercurrentTime = 0.0f;
        } 
    }

    private void RotateControl()
    {
        float mouseX = Input.GetAxis("Mouse X") * _rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, mouseX);
    }

    private void MovementControl()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * _moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    private void Movement()
    {
        MovementControl();
        _lastInputTime = Time.time; 
    }

    private void shieldControl() 
    {
       
        if (_currentState != PlayerState.Shield)
        {
            if (Input.GetKeyDown(_movementKeys[4]))
            {
                SetPlayerState(PlayerState.Shield);
                playerShield.CreateShield();
            }
        }
    }

    public void Update()
    {
        RotateControl();
        powerState();
        playerState();
    }
}
