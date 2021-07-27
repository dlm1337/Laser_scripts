using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedPowerUp : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        
            FindObjectOfType<Player>().AttackSpeedPowerUpStart();
            Destroy(gameObject);

        

    }

}

