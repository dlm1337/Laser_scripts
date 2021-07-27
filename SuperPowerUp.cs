using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPowerUp : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        FindObjectOfType<Player>().SuperPowerUpStart();
        Destroy(gameObject);
    }

}

