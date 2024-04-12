using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.VFX;
using static UnityEditor.PlayerSettings;

public class Bonuses : MonoBehaviour
{
    public GameObject PowerUpOne;
    public GameObject PowerUpTwo;
    public GameObject PowerUpThree;

    public bool PowerUpInCooldown = false;

    public List<GameObject> allCube;
    Vector3[] spawnPositions = new Vector3[8];

    private void Start()
    {
        InitializeSpawnPositions();
        SpawnAllCube();
    }

    private void InitializeSpawnPositions()
    {
        spawnPositions[0] = new Vector3(-30, 1, -8);
        spawnPositions[1] = new Vector3(-30, 1, 5);
        spawnPositions[2] = new Vector3(-20, 1, 5);
        spawnPositions[3] = new Vector3(-12, 1, 5);
        spawnPositions[4] = new Vector3(-10, 1, -2);
        spawnPositions[5] = new Vector3(-10, 1, 12);
        spawnPositions[6] = new Vector3(-19, 1, 12);
        spawnPositions[7] = new Vector3(-6, 1, 17);
    }

    public void SpawnAllCube()
    {
        StartCoroutine(SpawnCubesWithDelay());
    }

    public void destroyAll()
    {
       for (int i = 0; i < allCube.Count; i++)
       {
            allCube[i].GetComponent<BonusCube>().Destroyme();
       }
    }

    private IEnumerator SpawnCubesWithDelay()
    {
        foreach (Vector3 pos in spawnPositions)
        {
            GameObject powerUpPrefab = null;
            int powerRandom = Random.Range(0, 3);
            switch (powerRandom)
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
            yield return new WaitForSeconds(3f);
            GameObject bonusInstance = Instantiate(powerUpPrefab, pos, Quaternion.identity);
            allCube.Add(bonusInstance);
            bonusInstance.SetActive(true);
        }
    }
}