using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Power Up Wave Config")]
public class PowerUpWaveConfig : ScriptableObject
{

    [SerializeField] GameObject powerUpPrefab;
    [SerializeField] GameObject pwrUpLocPrefab;


    [SerializeField] int powerUps = 1;
    float timeBetweenPowerUps;
   
    [SerializeField] float minTimeToPowerUp = 25;
    [SerializeField] float maxTimeToPowerUp = 75;

 public GameObject GetPowerUpPrefab() { return powerUpPrefab; }


   
 public List<Transform> GetWaypointsPowerUps()
 {
    var waveWaypoints = new List<Transform>();
    foreach (Transform child in pwrUpLocPrefab.transform)
    {
        waveWaypoints.Add(child);

    }

    return waveWaypoints; ;
 }

  public float GetTimeBetweenPowerUps()
  {
        timeBetweenPowerUps = Random.Range(minTimeToPowerUp, maxTimeToPowerUp);

        return timeBetweenPowerUps; }

  public int GetNumberOfPowerUps() { return powerUps; }
  }
