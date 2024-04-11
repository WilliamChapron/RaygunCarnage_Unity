using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : PlayerController
{
    Animator animator;
    int isRunningHash;
    PlayerControl input;
    Vector2 currentMovement;
    Vector2 currentAim;



    void Awake()
    {

        input = new PlayerControl();

        input.Player1Controls.Movement.performed += ctx =>
        {
            currentMovement = ctx.ReadValue<Vector2>();
        };

        input.Player1Controls.Aim.performed += ctx =>
        {
            currentAim = ctx.ReadValue<Vector2>();
        };
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        isRunningHash = Animator.StringToHash("isRunning");
    }

    void RotateControl()
    {
        if (currentAim != Vector2.zero)
        {
            float angle = Mathf.Atan2(currentAim.x, currentAim.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }

    void MovementControl()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool movementPressed = currentMovement.magnitude > 0.1f;

        if (movementPressed && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }

        if (!movementPressed && !isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }

        Vector3 movementDirection = new Vector3(currentMovement.x, 0, currentMovement.y);
        transform.Translate(movementDirection * Time.deltaTime * _moveSpeed, Space.World);
    }

    void Update()
    {
        MovementControl();
        RotateControl();
    }

    void OnEnable()
    {
        input.Player1Controls.Enable();
    }

    void OnDisable()
    {
        input.Player1Controls.Disable();
    }

    public void SetInputVector(Vector2 input)
    {
        currentMovement = input;
    }

}
