using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player2Controller : MonoBehaviour
{

    public float speed = 5.0f;

    Animator animator;

    int isRunningHash;
    //#TODO animation pour le dash =
    //int isDashingHash; 

    PlayerInput input;

    Vector2 currentMovement;
    bool movementPressed;

    void Awake()
    {
        input = new PlayerInput();

        input.CharacterControls.Movement.performed += ctx =>
        {
            currentMovement = ctx.ReadValue<Vector2>();
            movementPressed = currentMovement.x != 0 || currentMovement.y != 0;
        };

        //#TODO pour le dash 
        //input.CharacterControls.Movement.performed += ctx => currentMovement = ctx.ReadValueAsButton();
    }

    void Start()
    {
        animator = GetComponent<Animator>();

        isRunningHash = Animator.StringToHash("isRunning");
    }

    void RotateControl()
    {
        Vector3 currentPosition = transform.position;

        Vector3 newPosition = new Vector3(currentMovement.x, 0, currentMovement.y);

        Vector3 positionToLookAt = currentPosition + newPosition;

        transform.LookAt(positionToLookAt);
    }

    void MovementControl()
    {
        bool isRunning = animator.GetBool(isRunningHash);

        if (movementPressed && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }

        if (!movementPressed && !isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }

        Vector3 movementDirection = new Vector3(currentMovement.x, 0, currentMovement.y);
        transform.Translate(movementDirection * Time.deltaTime * speed, Space.World);
    }


    public void Update()
    {
        MovementControl();
        RotateControl();
    }

    private void OnEnable()
    {
        input.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        input.CharacterControls.Disable();
    }

}
