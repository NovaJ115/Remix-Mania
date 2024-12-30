using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomPickupSpawning : MonoBehaviour
{

    public List<GameObject> spawnPoints;
    public int amountOfPickups;
    
    private int randomNumber;

    public List<int> chosenNumbers;
    
    void Start()
    {
        
        for (int i = 0; i < amountOfPickups; i++)
        {
            
            randomNumber = Random.Range(0, spawnPoints.Count);
            //Debug.Log(randomNumber);
            Instantiate(Resources.Load("CoinPickup"), new Vector3(spawnPoints[randomNumber].transform.position.x, spawnPoints[randomNumber].transform.position.y, spawnPoints[randomNumber].transform.position.z), Quaternion.identity);
            spawnPoints.RemoveAt(randomNumber);
            //chosenNumbers.Add(randomNumber);
        }
    }

    
}
