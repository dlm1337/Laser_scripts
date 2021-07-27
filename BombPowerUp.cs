using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPowerUp : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
  
            var gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
            if (gameObjects.Length > 0)
            {
                FindObjectOfType<Enemy>().BombPowerUpStart();

                Destroy(gameObject);

            }

            var gameObjects2 = GameObject.FindGameObjectsWithTag("Ice Enemy");
            if (gameObjects2.Length > 0)
            {
                FindObjectOfType<IceEnemy>().BombPowerUpStart2();

                Destroy(gameObject);

             }

            else
            {
                FindObjectOfType<GameSession>().AddToScore(200);
            }



        
    }

}
