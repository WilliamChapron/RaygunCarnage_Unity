using UnityEngine;
using System.Collections;
using System;
using Unity.VisualScripting;

public class RoundSystem : MonoBehaviour
{
    // Param�tres du round
    public int numberOfRounds = 3;
    public float roundDuration = 60f;

    private int currentRound = 0;
    public static bool End = false;

    public void StartRounds()
    {
        if (currentRound <= numberOfRounds) 
        {
            currentRound++;
            //Retour aux positions initiales
            StartCoroutine(RoundTimer());
        }
        else
        {
            //Ins�rer la fin du jeu
        }
    }
    public void Update()
    {
        if (End == true) 
        {
            End = false;
            StopCoroutine(RoundTimer());
            StartRounds();
        }
    }
    
   private IEnumerator RoundTimer()
    {
        yield return new WaitForSeconds(roundDuration);
        End = true;
    } 
}
