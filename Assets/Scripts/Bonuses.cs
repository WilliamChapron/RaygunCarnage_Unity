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

    public void PowerUpSpawn()
    {
        if (PowerRandom == 0)
        {
            if (RandomSpawn == 1)
            {
                Instantiate(PowerUpOne, spot1, Quaternion.identity);
                return;
            }
            else if (RandomSpawn == 2)
            {
                Instantiate(PowerUpOne, spot2, Quaternion.identity);
                return;
            }
            else if (RandomSpawn == 3)
            {
                Instantiate(PowerUpOne, spot3, Quaternion.identity);
                return;
            }
            else if(RandomSpawn == 4)
            {
                Instantiate(PowerUpOne, spot4, Quaternion.identity);
                return;
            }
            else if(RandomSpawn == 5)
            {
                Instantiate(PowerUpOne, spot5, Quaternion.identity);
                return;
            }
            else if(RandomSpawn == 6)
            {
                Instantiate(PowerUpOne, spot6, Quaternion.identity);
                return;
            }
            else if(RandomSpawn == 7)
            {
                Instantiate(PowerUpOne, spot7, Quaternion.identity);
                return;
            }
            else if(RandomSpawn == 8)
            {
                Instantiate(PowerUpOne, spot8, Quaternion.identity);
                return;
            }

        }
        else if (PowerRandom == 1)
        {
            if (RandomSpawn == 1)
            {
                Instantiate(PowerUpTwo, spot1, Quaternion.identity);
                return;
            }
            else if(RandomSpawn == 2)
            {
                Instantiate(PowerUpTwo, spot2, Quaternion.identity);
                return;
            }
            else if(RandomSpawn == 3)
            {
                Instantiate(PowerUpTwo, spot3, Quaternion.identity);
                return;
            }
            else if(RandomSpawn == 4)
            {
                Instantiate(PowerUpTwo, spot4, Quaternion.identity);
                return;
            }
            else if(RandomSpawn == 5)
            {
                Instantiate(PowerUpTwo, spot5, Quaternion.identity);
                return;
            }
            else if(RandomSpawn == 6)
            {
                Instantiate(PowerUpTwo, spot6, Quaternion.identity);
                return;
            }
            else if(RandomSpawn == 7)
            {
                Instantiate(PowerUpTwo, spot7, Quaternion.identity);
                return;
            }
            else if(RandomSpawn == 8)
            {
                Instantiate(PowerUpTwo, spot8, Quaternion.identity);
                return;
            }
        }
        else if (PowerRandom == 2)
        {
            if (RandomSpawn == 1)
            {
                Instantiate(PowerUpThree, spot1, Quaternion.identity);
                return;
            }
            else if(RandomSpawn == 2)
            {
                Instantiate(PowerUpThree, spot2, Quaternion.identity);
                return;
            }
            else if(RandomSpawn == 3)
            {
                Instantiate(PowerUpThree, spot3, Quaternion.identity);
                return;
            }
            else if(RandomSpawn == 4)
            {
                Instantiate(PowerUpThree, spot4, Quaternion.identity);
                return;
            }
            else if(RandomSpawn == 5)
            {
                Instantiate(PowerUpThree, spot5, Quaternion.identity);
                return;
            }
            else if(RandomSpawn == 6)
            {
                Instantiate(PowerUpThree, spot6, Quaternion.identity);
                return;
            }
            else if(RandomSpawn == 7)
            {
                Instantiate(PowerUpThree, spot7, Quaternion.identity);
                return;
            }
            else if(RandomSpawn == 8)
            {
                Instantiate(PowerUpThree, spot8, Quaternion.identity);
                return;
            }
        }
        return;
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

