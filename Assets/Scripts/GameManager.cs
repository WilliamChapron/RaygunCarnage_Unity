using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public RoundSystem roundSystem;

    void Start()
    {
        //PauseGame();

        //roundSystem.StartRounds();
    }

    void Update()
    {
        //if (roundManager.IsRoundInProgress())
        //{
        //    // Reprise de l'exécution des composants si un round est en cours
        //    ResumeGame();
        //}
        //else
        //{
        //    // Mise en pause de l'exécution des composants si aucun round n'est en cours
        //    PauseGame();
        //}
        //Debug.Log("ROUND ROUND");
        //PauseGame();
    }

    void PauseGame()
    {
        Time.timeScale = 0f; // Mettre le temps à zéro pour mettre en pause le jeu
    }

    void ResumeGame()
    {
        Time.timeScale = 1f; // Réglez le temps à 1 pour reprendre le jeu
    }
}
