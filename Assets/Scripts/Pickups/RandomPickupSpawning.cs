using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomPickupSpawning : MonoBehaviour
{

    public Transform[] spawnPoints;
    public int amountOfPickups;
    
    private int randomNumber;

    public int[] chosenNumbers;
    
    void Start()
    {
        chosenNumbers = new int[amountOfPickups];
        for (int i = 0; i < amountOfPickups; i++)
        {
            
            randomNumber = Random.Range(0, spawnPoints.Length);
            Debug.Log(randomNumber);
            chosenNumbers[i] = randomNumber;
        }
    }

    
}
