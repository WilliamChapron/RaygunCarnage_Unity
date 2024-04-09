using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashControl : MonoBehaviour
{
    public float dashDistance = 5f; // Distance � parcourir pendant le dash
    public float dashDuration = 0.2f; // Dur�e du dash en secondes
    public float dashCooldown = 1f; // Temps de recharge du dash en secondes

    private bool isDashing = false; // Indique si le joueur est en train de dasher
    private float dashTimer = 0f; // Timer pour suivre la dur�e du dash
    private float cooldownTimer = 0f; // Timer pour suivre le temps de recharge du dash

    // Met � jour la logique du dash
    void Update()
    {
        // V�rifie si le dash est en cours et met � jour sa dur�e
        if (isDashing)
        {
            dashTimer += Time.deltaTime;
            if (dashTimer >= dashDuration)
            {
                isDashing = false;
                dashTimer = 0f;
            }
        }
        else
        {
            // Si le dash n'est pas en cours, met � jour le temps de recharge
            cooldownTimer += Time.deltaTime;
        }

        // V�rifie si le joueur peut dasher (cooldown)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Lance le dash
            PerformDash();
        }
    }

    // Fonction pour ex�cuter le dash
    void PerformDash()
    {
        // Calcule la direction du dash (ici, vers la droite)
        Vector3 dashDirection = Vector3.right;

        // Applique le dash en d�pla�ant l'objet dans la direction souhait�e
        transform.position += dashDirection * dashDistance;

        // R�initialise les compteurs de temps
        isDashing = true;
        dashTimer = 0f;
        cooldownTimer = 0f;
    }
}
