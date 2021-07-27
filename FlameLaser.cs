using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameLaser : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
 



        public void OnTriggerEnter2D(Collider2D other)
    {

        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);

    }
 }
