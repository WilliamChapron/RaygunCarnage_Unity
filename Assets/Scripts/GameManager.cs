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
        //    // Reprise de l'ex�cution des composants si un round est en cours
        //    ResumeGame();
        //}
        //else
        //{
        //    // Mise en pause de l'ex�cution des composants si aucun round n'est en cours
        //    PauseGame();
        //}
        //Debug.Log("ROUND ROUND");
        //PauseGame();
    }

    void PauseGame()
    {
        Time.timeScale = 0f; // Mettre le temps � z�ro pour mettre en pause le jeu
    }

    void ResumeGame()
    {
        Time.timeScale = 1f; // R�glez le temps � 1 pour reprendre le jeu
    }
}
