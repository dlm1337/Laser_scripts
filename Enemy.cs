using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 150;

    [Header("Shooting")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject iceEnemy;


    [SerializeField] float projectileSpeed = 10f;

    [Header("Sound Effects")]
    [SerializeField] GameObject deathVFX;

    [SerializeField] float durationOfExplosion = 1f;

    [SerializeField] AudioClip deathSound;
 

    [SerializeField][Range(0,1)] float deathSoundVolume = 0.65f;

    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;






   
    bool ice = false;
  

    // Start is called before the first frame update
    void Start()
    {
       
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);  
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
  
    }
    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

        }
    
     }

    public void Ice()
    {
        
      
            ice = true;
        
       
    }



    private void Fire()
    {
        GameObject laser = Instantiate(
            projectile,
            transform.position,
            Quaternion.identity
            ) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
    }

  



    public void OnTriggerEnter2D(Collider2D other)
    {
       
        {

            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            if (!damageDealer) { return; }
            ProcessHit(damageDealer);
            
        }
        if (ice == true)
       {
            GameObject iceEnemyStart = Instantiate(
            iceEnemy,
            transform.position,
            Quaternion.identity
            ) as GameObject;
            iceEnemyStart.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        
                Destroy(gameObject);
            
        }

    


    }

    public void OnDestroy()
    {

        if (ice == false)
        {

            GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
            Destroy(explosion, durationOfExplosion);
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        }
  
    
    
    }

    private void ProcessHit(DamageDealer damageDealer)
    {   
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();

        }
    }

    private void Die()
    {

        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);

    }

    public void BombPowerUpStart()
    {
        
        var gameObjects = GameObject.FindGameObjectsWithTag("Enemy");

      
            for (var i = 0; i < gameObjects.Length; i++)
            {


                FindObjectOfType<GameSession>().AddToScore2(scoreValue);
                Destroy(gameObjects[i]);
                GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
                Destroy(explosion, durationOfExplosion);
                AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
           }
        
       
            
           
        
       
       
    }


}
