using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner2 : MonoBehaviour
{

    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;
        
    bool firstWave = false;

    // Start is called before the first frame update

    IEnumerator Start()
    {


        do
        {
            yield return StartCoroutine(SpawnAllWaves());

        }
        while (looping);



    }

    private IEnumerator SpawnAllWaves()
    {
        if (firstWave == false)
        {
            float randomSpawn = Random.Range(60f, 200f);
            yield return new WaitForSeconds(randomSpawn);
        }
  

        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)


        {
            firstWave = true;
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
         
        }


    }


    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(
            waveConfig.GetEnemyPrefab(),
            waveConfig.GetWaypoints()[0].transform.position,
            Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
        float randomSpawn = Random.Range(65f, 240f);
        yield return new WaitForSeconds(randomSpawn);
    }

}
