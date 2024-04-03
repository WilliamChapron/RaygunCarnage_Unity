using UnityEngine;
using System.Collections;
using System;

public class RoundSystem : MonoBehaviour
{
    // �v�nement d�clench� � la fin de chaque round
    public event Action OnRoundEnd;

    // Param�tres du round
    public int numberOfRounds = 3;
    public float roundDuration = 60f;

    private int currentRound = 1;

    // D�marrer les rounds
    public void StartRounds()
    {
        StartCoroutine(RunRounds());
    }

    // Coroutine pour ex�cuter les rounds
    private IEnumerator RunRounds()
    {
        while (currentRound <= numberOfRounds)
        {
            Debug.Log("D�but du round " + currentRound);

            // D�clencher un �v�nement de d�but de round
            // Vous pouvez ajouter des m�thodes de gestionnaires d'�v�nements ici pour effectuer des actions sp�cifiques au d�but du round

            yield return new WaitForSeconds(roundDuration);

            Debug.Log("Fin du round " + currentRound);

            // D�clencher un �v�nement de fin de round
            OnRoundEnd?.Invoke();

            // Passer au round suivant
            currentRound++;
        }

        Debug.Log("Tous les rounds termin�s");
    }
}
