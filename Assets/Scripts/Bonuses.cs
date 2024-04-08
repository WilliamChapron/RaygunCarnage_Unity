using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
public class Bonuses : MonoBehaviour
{
    List<string> bonus = new List<string> { "Ethereal", "Explosive", "Bouncy" };
    // Start is called before the first frame update
    static System.Random random = new System.Random();

    public GameObject PowerUpOne;
    public GameObject PowerUpTwo;
    public GameObject PowerUpThree;

    public bool PowerUpInCooldown = false;
    public float PowerUpCooldownTime = 10f;
    public int PowerRandom = 0;

    public void PowerUpSpawn()
    {
        if (PowerRandom == 3) { PowerRandom = 0; }
        if (PowerRandom == 0)
        {
            Instantiate(PowerUpOne);
            return;
        }
        else if (PowerRandom == 1)
        {
            Instantiate(PowerUpTwo);
            return;
        }
        else if (PowerRandom == 2)
        {
            Instantiate(PowerUpThree);
            return;
        }
        return;
    }

    void Start()
    {

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
        PowerRandom += 1;
        
        Debug.Log("Power Random est " + PowerRandom);
        PowerUpSpawn();
        PowerUpInCooldown = false;
        
    }
}

