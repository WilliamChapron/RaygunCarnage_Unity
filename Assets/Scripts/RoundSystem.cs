using UnityEngine;
using System.Collections;
using System;
using Unity.VisualScripting;
using static PlayerController;

public class RoundSystem : MonoBehaviour
{
    // Paramètres du round
    public int numberOfRounds = 3;
    public float roundDuration = 60f;

    private int currentRound = 0;

    private Vector3 basePosPlayer1;
    private Vector3 basePosPlayer2;

    private GameObject[] playerObjects;

    private int scorePlayer1;
    private int scorePlayer2;

    public static bool isRoundNeedToChange;
    private bool isGameEnd;

    private void Start()
    {
        playerObjects = GameObject.FindGameObjectsWithTag("PlayerControllable");
        basePosPlayer1 = playerObjects[0].transform.position;
        basePosPlayer2 = playerObjects[1].transform.position;

        scorePlayer1 = 0;
        scorePlayer2 = 0;

        isRoundNeedToChange = true;
        isGameEnd = false;
    }

    private void Update()
    {
        if (isRoundNeedToChange)
        {
            isRoundNeedToChange = false;
            StartRounds();
        }
    }

    private void StartRounds()
    {
        if (!isGameEnd && currentRound < numberOfRounds)
        {
            currentRound++;
            ResetPlayers();
            StartCoroutine(RoundTimer());
        }
        else
        {
            EndGame();
        }
    }

    private void ResetPlayers()
    {
        playerObjects[0].transform.position = basePosPlayer1;
        playerObjects[1].transform.position = basePosPlayer2;

        playerObjects[0].GetComponent<PlayerController>().SetPlayerState(PlayerState.Idle);
        playerObjects[1].GetComponent<PlayerController>().SetPlayerState(PlayerState.Idle);
    }

    private IEnumerator RoundTimer()
    {
        yield return new WaitForSeconds(roundDuration);
        isRoundNeedToChange = true;
    }

    private void EndGame()
    {
        isGameEnd = true;
    }
}
