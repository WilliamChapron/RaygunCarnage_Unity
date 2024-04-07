/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public Animator animator;
    private bool _isRunning = false;
    public float _moveSpeed = 5f;
    public float _rotationSpeed = 600f;
    private float _lastInputTime;
    public List<KeyCode> _movementKeys;

    // Constructeur pour accepter les paramètres personnalisés
    public void Initialize(List<KeyCode> movementKeys)
    {
        _movementKeys = movementKeys;
    }

    void Start()
    {
        animator.Play("Idle", 0, 0f);
    }

    private void PerformComeBackIdle()
    {
        if (Time.time - _lastInputTime > 1f)
        {
            animator.SetBool("isRunning", false);
            _isRunning = false;
            animator.CrossFade("Idle", 0f);
        }
    }

    private void RotateControl()
    {
        // Rotate player !not camera
        float mouseX = Input.GetAxis("Mouse X") * _rotationSpeed * Time.deltaTime;

        //Debug.Log("Log " + mouseX);
        transform.Rotate(Vector3.up, mouseX);
    }

    private void MovementControl()
    {
        // Controls WASD
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * _moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    public void Update()
    {
        if (Input.GetKey(_movementKeys[0]) || Input.GetKey(_movementKeys[1]) || Input.GetKey(_movementKeys[2]) || Input.GetKey(_movementKeys[3]))
        {
            // Controls
            RotateControl();
            MovementControl();

            _lastInputTime = Time.time;
            //
            animator.SetBool("isRunning", true);
            _isRunning = true;
            animator.CrossFade("Running", 0f);
        }

        // Check for ComeBack Idle
        PerformComeBackIdle();




    }
}
*/