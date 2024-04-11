using UnityEngine;
using System.Collections;
using System;
using Unity.VisualScripting;

using UnityEngine.TextCore.Text;
using UnityEditor.Rendering;
using static PlayerController;

public class RoundSystem : MonoBehaviour
{
    // Param�tres du round
    public int numberOfRounds = 3;
    public float roundDuration = 60f;

    private int currentRound = 0;
    private float TextOnScreen = 3f;
    private GameObject ScoreP1;
    private GameObject ScoreP2;
    private GameObject RoundText;
    private GameObject RoundCount;

    private Vector3 basePosPlayer1;
    private Vector3 basePosPlayer2;

    private GameObject[] playerObjects;

    public static int scorePlayer1;
    public static int scorePlayer2;

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

        //Temp1 = GameObject.Find("Text Round Counter");
        //Temp2 = GameObject.Find("Text Rounds");
        //Temp1.SetActive(false);
        //Temp2.SetActive(false);
        //TextCounter = GameObject.Find("Text Round Counter").GetComponent<TextMesh>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            StartRounds();
        }
        //if (isRoundNeedToChange)
        //{
        //    isRoundNeedToChange = false;
        //    StartRounds();
        //}
    }

    private void StartRounds()
    {
        if (!isGameEnd && currentRound < numberOfRounds)
        {
            currentRound++;
            ResetPlayers();
            //SpawnText2dText();
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
        if (isRoundNeedToChange == false)
        {
            isRoundNeedToChange = true;
        }

    }

    private void EndGame()
    {
        isGameEnd = true;
    }
}
