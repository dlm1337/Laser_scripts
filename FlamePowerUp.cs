using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamePowerUp : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {

        FindObjectOfType<Player>().FlamePowerUpStart();

        Destroy(gameObject);

    }

}

