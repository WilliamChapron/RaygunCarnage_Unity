using UnityEngine;
using System.Collections;
using System;
using Unity.VisualScripting;
using UnityEngine.TextCore.Text;
using UnityEditor.Rendering;

public class RoundSystem : MonoBehaviour
{
    // Paramètres du round
    public int numberOfRounds = 3;
    public float roundDuration = 60f;

    private int currentRound = 0;
    private float TextOnScreen = 3f;
    public static bool End = false;
    private GameObject Temp1;
    private GameObject Temp2;
    private TextMesh TextCounter;

    public void StartRounds()
    {
        if (currentRound <= numberOfRounds) 
        {
            currentRound++;
            StartCoroutine(SpawnText());
            //Retour aux positions initiales
            StartCoroutine(RoundTimer());
        }
        else
        {
            //Insérer la fin du jeu
        }
    }

    public void Start()
    {
        Temp1 = GameObject.Find("Text Round Counter");
        Temp2 = GameObject.Find("Text Rounds");
        Temp1.SetActive(false);
        Temp2.SetActive(false);
        TextCounter = GameObject.Find("Text Round Counter").GetComponent<TextMesh>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) 
        {
            StartRounds();
        }
        else if (End == true) 
        {
            End = false;
            StopCoroutine(RoundTimer());
            StartRounds();
        }
    }
    
   private IEnumerator SpawnText()
    {
        TextCounter.GetComponent<TextMesh>().text = "Round " + currentRound;
        Debug.Log(currentRound);
        Temp1.SetActive(true);
        Temp2.SetActive(true);
        yield return new WaitForSeconds(TextOnScreen);
        Temp1.SetActive(false);
        Temp2.SetActive(false);
    }
   private IEnumerator RoundTimer()
    {
        yield return new WaitForSeconds(roundDuration);
        End = true;
    } 
}
