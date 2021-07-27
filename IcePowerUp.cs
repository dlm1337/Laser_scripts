using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePowerUp : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
      
        FindObjectOfType<Player>().IcePowerUpStart();
        
        Destroy(gameObject);

    }

}

