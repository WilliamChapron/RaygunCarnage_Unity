using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashControl : MonoBehaviour
{
    public float dashDistance = 5f; // Distance à parcourir pendant le dash
    public float dashDuration = 0.2f; // Durée du dash en secondes
    public float dashCooldown = 1f; // Temps de recharge du dash en secondes

    private bool isDashing = false; // Indique si le joueur est en train de dasher
    private float dashTimer = 0f; // Timer pour suivre la durée du dash
    private float cooldownTimer = 0f; // Timer pour suivre le temps de recharge du dash

    // Met à jour la logique du dash
    void Update()
    {
        // Vérifie si le dash est en cours et met à jour sa durée
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
            // Si le dash n'est pas en cours, met à jour le temps de recharge
            cooldownTimer += Time.deltaTime;
        }

        // Vérifie si le joueur peut dasher (cooldown)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Lance le dash
            PerformDash();
        }
    }

    // Fonction pour exécuter le dash
    void PerformDash()
    {
        // Calcule la direction du dash (ici, vers la droite)
        Vector3 dashDirection = Vector3.right;

        // Applique le dash en déplaçant l'objet dans la direction souhaitée
        transform.position += dashDirection * dashDistance;

        // Réinitialise les compteurs de temps
        isDashing = true;
        dashTimer = 0f;
        cooldownTimer = 0f;
    }
}
