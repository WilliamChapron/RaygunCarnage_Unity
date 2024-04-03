using UnityEngine;
using System.Collections;
using System;

public class RoundSystem : MonoBehaviour
{
    // Événement déclenché à la fin de chaque round
    public event Action OnRoundEnd;

    // Paramètres du round
    public int numberOfRounds = 3;
    public float roundDuration = 60f;

    private int currentRound = 1;

    // Démarrer les rounds
    public void StartRounds()
    {
        StartCoroutine(RunRounds());
    }

    // Coroutine pour exécuter les rounds
    private IEnumerator RunRounds()
    {
        while (currentRound <= numberOfRounds)
        {
            Debug.Log("Début du round " + currentRound);

            // Déclencher un événement de début de round
            // Vous pouvez ajouter des méthodes de gestionnaires d'événements ici pour effectuer des actions spécifiques au début du round

            yield return new WaitForSeconds(roundDuration);

            Debug.Log("Fin du round " + currentRound);

            // Déclencher un événement de fin de round
            OnRoundEnd?.Invoke();

            // Passer au round suivant
            currentRound++;
        }

        Debug.Log("Tous les rounds terminés");
    }
}
