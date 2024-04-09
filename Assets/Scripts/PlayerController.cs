using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float  _moveSpeed = 5f;
    public float _rotationSpeed = 600f;
    public List<KeyCode> _movementKeys;

    private List<Power> listOfPower;

    public enum PlayerState
    {
        Idle,
        Running,
        Shield,
        Dash
    }

    private PlayerState _currentState;

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
                animator.CrossFade("Idle", 0f);
                break;
            case PlayerState.Dash:
                if (Input.GetKey(_movementKeys[0]) || Input.GetKey(_movementKeys[1]) || Input.GetKey(_movementKeys[2]) || Input.GetKey(_movementKeys[3]))
                {
                    Movement();
                    animator.CrossFade("Running", 0f);
                    if (!Input.GetKey(_movementKeys[0]) && !Input.GetKey(_movementKeys[1]) && !Input.GetKey(_movementKeys[2]) && !Input.GetKey(_movementKeys[3]))
                    {
                        SetPlayerState(PlayerState.Idle);
                    }
                }
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
        
        if (_currentState != PlayerState.Shield)
        {
            if (Input.GetKeyDown(_movementKeys[4]) && power == listOfPower[0] && power._powerState != Power.PowerState.Using)
            {
                SetPlayerState(PlayerState.Shield);
                power.LunchPower();
            }
            else if (Input.GetKeyDown(_movementKeys[5]) && power == listOfPower[1])
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
        Debug.Log(_moveSpeed);
        RotateControl();
        UpdatePowerState();
        playerState();
    }
}
