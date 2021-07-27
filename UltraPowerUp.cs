using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltraPowerUp : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        FindObjectOfType<Player>().UltraPowerUpStart();
        Destroy(gameObject);

    }

}
