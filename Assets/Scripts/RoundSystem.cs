using UnityEngine;
using System.Collections;
using TMPro;

public class RoundSystem : MonoBehaviour
{
    public int numberOfRounds = 3;
    public float roundDuration = 60f;

    private int currentRound = 0;

    public GameObject ScoreP1 = null;
    public GameObject ScoreP2 = null;
    public GameObject ScoreTextP1 = null;
    public GameObject ScoreTextP2 = null;
    public GameObject RoundText = null;
    public GameObject RoundCount = null;
    public GameObject Timer = null;

    private Vector3 basePosPlayer1;
    private Vector3 basePosPlayer2;
    private GameObject[] playerObjects;

    public static int scorePlayer1;
    public static int scorePlayer2;
    private int timer;

    public static bool isRoundNeedToChange;
    private bool isGameEnd;

    private void Start()
    {
        playerObjects = GameObject.FindGameObjectsWithTag("PlayerControllable");
        basePosPlayer1 = playerObjects[0].transform.position;
        basePosPlayer2 = playerObjects[1].transform.position;

        scorePlayer1 = 0;
        scorePlayer2 = 0;

        currentRound = 0;

        isRoundNeedToChange = true;
        isGameEnd = false;

        DeactivateTexts();
    }

    private void Update()
    {
        StartRounds();
        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        if (Timer != null)
        {
            Timer.GetComponent<TextMeshProUGUI>().text = timer.ToString();
        }
    }

    private void StartRounds()
    {
        if (!isGameEnd && currentRound < numberOfRounds)
        {
            currentRound++;
            ResetPlayers();
            StartCoroutine(ManageRound());
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
        playerObjects[0].GetComponent<PlayerController>().SetPlayerState(PlayerController.PlayerState.Idle);
        playerObjects[1].GetComponent<PlayerController>().SetPlayerState(PlayerController.PlayerState.Idle);
    }

    private IEnumerator ManageRound()
    {
        ActivateTexts();
        SpawnText2dText();
        //yield return StartCoroutine(RoundTimer());
        yield return new WaitForSeconds(2f); // Attendre 2 secondes
        DeactivateTexts();
    }

    private IEnumerator RoundTimer()
    {
        timer = (int)roundDuration;
        while (timer > 0)
        {
            Timer.GetComponent<TextMeshProUGUI>().text = timer.ToString();
            yield return new WaitForSeconds(1f);
            timer--;

            if (timer <= 0)
            {
                timer = 0;
                Timer.GetComponent<TextMeshProUGUI>().text = timer.ToString();

                // Mettre fin au round
                isRoundNeedToChange = true;
                break;
            }
        }
    }


    private IEnumerator SpawnText2dText()
    {
        ScoreP1.GetComponent<TextMeshProUGUI>().text = scorePlayer1.ToString();
        ScoreP2.GetComponent<TextMeshProUGUI>().text = scorePlayer2.ToString();
        RoundCount.GetComponent<TextMeshProUGUI>().text = currentRound.ToString();

        yield return new WaitForSeconds(2f);
    }

    private void ActivateTexts()
    {
        ScoreP1.SetActive(true);
        ScoreP2.SetActive(true);
        ScoreTextP1.SetActive(true);
        ScoreTextP2.SetActive(true);
        RoundText.SetActive(true);
        RoundCount.SetActive(true);
        Timer.SetActive(true);
    }

    private void DeactivateTexts()
    {
        ScoreP1.SetActive(false);
        ScoreP2.SetActive(false);
        ScoreTextP1.SetActive(false);
        ScoreTextP2.SetActive(false);
        RoundText.SetActive(false);
        RoundCount.SetActive(false);
        Timer.SetActive(false);
    }

    private void EndGame()
    {
        isGameEnd = true;
    }
}
