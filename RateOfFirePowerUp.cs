using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateOfFirePowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {

        FindObjectOfType<Player>().RateOfFirePowerUpStart();
        Destroy(gameObject);



    }
}
