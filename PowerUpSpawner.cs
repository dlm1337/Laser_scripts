using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] List<PowerUpWaveConfig> powerUpWaveConfigs;
  
    [SerializeField] bool looping = false;
    [SerializeField] int timeToSpawn;
     
    [Header("Has to match list size")]
    [SerializeField] int minPowerUpRange = 1;
    [SerializeField] int maxPowerUpRange = 16;

     int startingPowerUp;
     int randomLocation;
     bool spawnOnce = true;
    
    // Start is called before the first frame update

    IEnumerator Start()
    { do
        {
            startingPowerUp = Random.Range(minPowerUpRange, maxPowerUpRange);
            yield return StartCoroutine(SpawnAllPowerUpWaves());
        }
        while (looping);
    }
  
    public void Awake() 
    {
        startingPowerUp = Random.Range(minPowerUpRange, maxPowerUpRange);
         randomLocation = Random.Range(1, 5);
    }
    public void Update()
    {
        startingPowerUp = Random.Range(minPowerUpRange, maxPowerUpRange);
    }



    public int TimeToSpawn()
    {
        timeToSpawn = Random.Range(5, 30);
        return timeToSpawn;
    }

 



    private IEnumerator SpawnAllPowerUpWaves()
    {
        int waveIndex = startingPowerUp;


        if (spawnOnce == true)
        {
            yield return new WaitForSeconds(TimeToSpawn());
            spawnOnce = false;
        }
       
            var currentWave = powerUpWaveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllPowerUps(currentWave));
    }


    private IEnumerator SpawnAllPowerUps(PowerUpWaveConfig powerUpWaveConfig)
    {
        for (int powerUpCount = 0; powerUpCount < powerUpWaveConfig.GetNumberOfPowerUps(); powerUpCount++)
        {
            randomLocation = Random.Range(1, 5);
            var newPowerUp = Instantiate(
            powerUpWaveConfig.GetPowerUpPrefab(),
            powerUpWaveConfig.GetWaypointsPowerUps()[randomLocation].transform.position,
            Quaternion.identity);
           
            yield return new WaitForSeconds(powerUpWaveConfig.GetTimeBetweenPowerUps());
        }

    }
}
