using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float _moveSpeed = 5f;
    public float _rotationSpeed = 600f;
    public List<KeyCode> _movementKeys;
    
    private Shield playerShield;
    private List<Power> listOfPower;
    public enum PlayerState
    {
        Idle,
        Running,
        OnPower
    }

    private PlayerState _currentState;

    void Start()
    {
        SetPlayerState(PlayerState.Idle);
        listOfPower = new List<Power>();
        Shield shield = GetComponent<Shield>();
        listOfPower.Add(shield);
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
            case PlayerState.OnPower:
                animator.CrossFade("Idle", 0f);
                break;
        }
    }

    private void SetPlayerState(PlayerState newState)
    {
        _currentState = newState;
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
    }

    private void Control(Power power) 
    {
        
        if (power._powerState != Power.PowerState.Using)
        {
            if (_currentState != PlayerState.OnPower)
            {
                if (Input.GetKeyDown(_movementKeys[4]))
                {
                    SetPlayerState(PlayerState.OnPower);
                    power.LunchPower();
                }
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
        RotateControl();
        UpdatePowerState();
        playerState();
    }
}
