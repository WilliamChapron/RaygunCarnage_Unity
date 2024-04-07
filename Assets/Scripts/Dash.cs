using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float DashSpeed = 3f;
    public float DashTravelTime = 0.3f;
    public float DashCooldownTime = 2f;
    private bool CanDash = true;
    private bool isDashing = false;

    [SerializeField] TrailRenderer tr;

    // Start is called before the first frame update 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing == true)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            ToDash();
            StartCoroutine(DashCooldown());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing == true)
        {
            return;
        }
    }

    public void ToDash()
    {
        PlayerController._moveSpeed *= DashSpeed;
    }

    private IEnumerator DashCooldown()
    {
        CanDash = false;
        isDashing = true;
        tr.emitting = true;
        yield return new WaitForSeconds(DashTravelTime);
        PlayerController._moveSpeed /= DashSpeed;
        isDashing = false;
        tr.emitting = false;
        yield return new WaitForSeconds(DashCooldownTime);
        CanDash = true;
    }

    
}
