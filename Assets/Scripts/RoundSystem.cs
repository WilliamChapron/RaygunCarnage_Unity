using UnityEngine;
using System.Collections;
using TMPro;
using static PlayerController;

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

    public GameObject bonusSystem;

    public static IEnumerator SetRoundChange(GameObject player)
    {
        Debug.Log("Rs");
        yield return new WaitForSeconds(3f); 
        if (player.name == "Player1")
        {
            scorePlayer1 += 10;
        }
        if (player.name == "Player2")
        {
            scorePlayer2 += 10;
        }
        isRoundNeedToChange = true;
    }


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
        //Debug.Log("Round change : " + isRoundNeedToChange);
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
        if (isRoundNeedToChange)
        {
            currentRound++;
            ResetPlayers();
            ResetPower();
            StartCoroutine(ManageRound());
            isRoundNeedToChange = false;
        }
    }

    private void ResetPlayers()
    {
       
        playerObjects[0].transform.position = basePosPlayer1;
        playerObjects[1].transform.position = basePosPlayer2;
        Player1Controller Pco = playerObjects[0].gameObject.GetComponent<Player1Controller>();
        if (Pco == null)
        {
            Player2Controller Pct = playerObjects[0].gameObject.GetComponent<Player2Controller>();
            Pct.SetPlayerState(PlayerState.Idle);
            Pct.middledead = false;
            Player1Controller Pcot = playerObjects[1].gameObject.GetComponent<Player1Controller>();
            Pcot.SetPlayerState(PlayerState.Idle);
            Pcot.middledead = false;
        }
        else
        {
           Pco.SetPlayerState(PlayerState.Idle);
           Pco.middledead = false;  
           Player2Controller Pctt = playerObjects[1].gameObject.GetComponent<Player2Controller>();
           Pctt.SetPlayerState(PlayerState.Idle);
           Pctt .middledead = false;
   
        }
    }

    private void ResetPower()
    {
        Bonuses theBonus = bonusSystem.GetComponent<Bonuses>();
        theBonus.destroyAll();
        theBonus.SpawnAllCube();
    }

    private IEnumerator ManageRound()
    {
        ActivateTexts();
        SpawnText2dText();
        yield return new WaitForSeconds(2f); // Attendre 2 secondes
        DeactivateTexts();
    }

    //private IEnumerator RoundTimer()
    //{
    //    timer = (int)roundDuration;
    //    while (timer > 0)
    //    {
    //        Timer.GetComponent<TextMeshProUGUI>().text = timer.ToString();
    //        yield return new WaitForSeconds(1f);
    //        timer--;

    //        if (timer <= 0)
    //        {
    //            timer = 0;
    //            Timer.GetComponent<TextMeshProUGUI>().text = timer.ToString();

    //            // Mettre fin au round
    //            isRoundNeedToChange = true;
    //            break;
    //        }
    //    }
    //}


    private void SpawnText2dText()
    {
        ScoreP1.GetComponent<TextMeshProUGUI>().text = scorePlayer1.ToString();
        ScoreP2.GetComponent<TextMeshProUGUI>().text = scorePlayer2.ToString();
        RoundCount.GetComponent<TextMeshProUGUI>().text = currentRound.ToString();
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
