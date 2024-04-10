using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.VFX;

public class Bonuses : MonoBehaviour
{
    // Start is called before the first frame update
    static System.Random random = new System.Random();

    public GameObject PowerUpOne;
    public GameObject PowerUpTwo;
    public GameObject PowerUpThree;

    public bool PowerUpInCooldown = false;
    public float PowerUpCooldownTime = 10f;
    public int PowerRandom = 0;
    public int RandomSpawn = 0;

    Vector3 spot1 = new Vector3(50, 2, 3);

    Vector3 spot2 = new Vector3(50, 2, 16);

    Vector3 spot3 = new Vector3(64, 2, 28);

    Vector3 spot4 = new Vector3(37, 2, 28);

    Vector3 spot5 = new Vector3(32, 2, 64);

    Vector3 spot6 = new Vector3(68, 2, 59);

    Vector3 spot7 = new Vector3(73, 2, 5);

    Vector3 spot8 = new Vector3(35, 2, 5);

    private void InCube(GameObject thisPower, Vector3 thisSpot)
    {
        if (thisPower != null)
        {
            GameObject bonusInstance = Instantiate(thisPower, thisSpot, Quaternion.identity);
            bonusInstance.SetActive(true);
        }
    }

    public void PowerUpSpawn()
    {

        GameObject powerUpPrefab = null;
        Vector3 spawnPosition = Vector3.zero;

        switch (PowerRandom)
        {
            case 0:
                powerUpPrefab = PowerUpOne;
                break;
            case 1:
                powerUpPrefab = PowerUpTwo;
                break;
            case 2:
                powerUpPrefab = PowerUpThree;
                break;
        }

        switch (RandomSpawn)
        {
            case 1:
                spawnPosition = spot1;
                break;
            case 2:
                spawnPosition = spot2;
                break;
            case 3:
                spawnPosition = spot3;
                break;
            case 4:
                spawnPosition = spot4;
                break;
            case 5:
                spawnPosition = spot5;
                break;
            case 6:
                spawnPosition = spot6;
                break;
            case 7:
                spawnPosition = spot7;
                break;
            case 8:
                spawnPosition = spot8;
                break;

        }

        InCube(powerUpPrefab, spawnPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (PowerUpInCooldown == false) 
        { 
        StartCoroutine(PowerUpCooldownIE());
        }
    }
    private IEnumerator PowerUpCooldownIE()
    {
        PowerUpInCooldown = true;
        yield return new WaitForSeconds(PowerUpCooldownTime);
        PowerRandom = Random.Range(0, 2);
        RandomSpawn = Random.Range(1, 8);
        Debug.Log("Power Random est " + PowerRandom);
        PowerUpSpawn();
        PowerUpInCooldown = false;
        
    }
}

