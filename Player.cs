using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // configuration parameters
    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 200;
    [SerializeField] AudioClip deathSound;
   
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.65f;

    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] GameObject laserPrefab2;
    [SerializeField] GameObject laserPrefab3;
    [SerializeField] GameObject laserPrefab4;
    [SerializeField] GameObject laserPrefab5;
    [SerializeField] GameObject laserPrefab6;
    [SerializeField] GameObject laserPrefab7;
    [SerializeField] GameObject laserPrefab8;
    [SerializeField] GameObject laserPrefab9;

    [SerializeField] float powerUpTimer = -1f;
    [SerializeField] float superPowerUpTimer = -1f;
    [SerializeField] float ultraPowerUpTimer = -1f;

    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;


    int spriteIndex = 0;

    Coroutine firingCoroutine;


    [SerializeField] Sprite[] playerSprites;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    float projectileFiringPeriod = 0.2F;
    float projectileSpeed = 9.5f;

    bool iceStart = false;
    bool flameStart = false;
    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();

    }
    void Update()
    {
        Move();
        Fire();
        PowerUpCountDown();
        SuperPowerUpCountDown();
        UltraPowerUpCountDown();
      
    }
 

    private void OnTriggerEnter2D(Collider2D other)
    {
   

        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
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

    public void OnDestroy()
    {
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }
    private void Die()
    {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);

    }
    public int GetHealth()
    {
        return health;
    }
    private void Fire()
    {
        if (superPowerUpTimer <= 0 && powerUpTimer <= 0 && ultraPowerUpTimer <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                firingCoroutine = StartCoroutine(FireContinuously());
          
            }
            if (Input.GetButtonUp("Fire1"))
            {
                StopCoroutine(firingCoroutine);

            }
        }
        else if (powerUpTimer > 0 && superPowerUpTimer < 0 && ultraPowerUpTimer < 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                firingCoroutine = StartCoroutine(FireContinuously2());
            }
            if (Input.GetButtonUp("Fire1"))
            {
                StopCoroutine(firingCoroutine);

            }
        }
        else if (superPowerUpTimer > 0 && ultraPowerUpTimer < 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                firingCoroutine = StartCoroutine(FireContinuously3());
            }
            if (Input.GetButtonUp("Fire1"))
            {
                StopCoroutine(firingCoroutine);

            }
        }
        else if (ultraPowerUpTimer > 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                firingCoroutine = StartCoroutine(FireContinuously4());
            }
            if (Input.GetButtonUp("Fire1"))
            {
                StopCoroutine(firingCoroutine);

            }
        }               

    }
    IEnumerator FireContinuously()
    { 
        
        
        while (powerUpTimer <= 0 && superPowerUpTimer <= 0 && ultraPowerUpTimer <= 0)
        {
           
          
            if(flameStart == false)
            {
                if (iceStart == false)
                {
                    GameObject laser = Instantiate(
                        laserPrefab, transform.position, Quaternion.identity) as GameObject;
                    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                    AudioSource.PlayClipAtPoint(shootSound,
                    Camera.main.transform.position,
                    shootSoundVolume);
                    yield return new WaitForSeconds(projectileFiringPeriod);
                }

                if (iceStart == true)
                {
                    Ice();
                    GameObject laser = Instantiate(
                        laserPrefab8, transform.position, Quaternion.identity) as GameObject;
                    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                    AudioSource.PlayClipAtPoint(shootSound,
                    Camera.main.transform.position,
                    shootSoundVolume);
                    yield return new WaitForSeconds(projectileFiringPeriod);

                }
            }

            if (flameStart == true)
            {
                if (iceStart == false)
                {
                    Vector3 temp = new Vector3(.6f, 0, 0);
                    Vector3 temp2 = new Vector3(-.6f, 0, 0);


                   
                    GameObject laser = Instantiate(
                        laserPrefab, transform.position, Quaternion.identity) as GameObject;
                    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                   
                    GameObject laser2 = Instantiate(
                          laserPrefab9, transform.position, Quaternion.identity) as GameObject;
                    laser2.transform.position += temp;
                    laser2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                  
                    GameObject laser3 = Instantiate(
                         laserPrefab9, transform.position, Quaternion.identity) as GameObject;
                    laser3.transform.position += temp2;
                    laser3.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                   
                    AudioSource.PlayClipAtPoint(shootSound,
                    Camera.main.transform.position,
                    shootSoundVolume);
                    yield return new WaitForSeconds(projectileFiringPeriod);
                }

                if (iceStart == true)
                {
                    Vector3 temp = new Vector3(.6f, 0, 0);
                    Vector3 temp2 = new Vector3(-.6f, 0, 0);




                    Ice();
                    GameObject laser = Instantiate(
                        laserPrefab8, transform.position, Quaternion.identity) as GameObject;
                    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                   
                    GameObject laser2 = Instantiate(
                           laserPrefab9, transform.position, Quaternion.identity) as GameObject;
                    laser2.transform.position += temp;
                    laser2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

                    GameObject laser3 = Instantiate(
                         laserPrefab9, transform.position, Quaternion.identity) as GameObject;
                    laser3.transform.position += temp2;
                    laser3.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

                    AudioSource.PlayClipAtPoint(shootSound,
                    Camera.main.transform.position,
                    shootSoundVolume);
                    yield return new WaitForSeconds(projectileFiringPeriod);

                }
            }

        }
    }
   
    public void Ice()
    {

        var gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
        if (gameObjects.Length > 0)
        {
            FindObjectOfType<Enemy>().Ice();

        }
        
    }

    IEnumerator FireContinuously2()//Power Up 1

    {
        while (powerUpTimer > 0)
        {
            
            
            
            if(flameStart == false)
            {

                if (iceStart == false)
                {
                    Vector3 temp = new Vector3(0, 1, 0);
                    GameObject laser = Instantiate(
                        laserPrefab,
                        transform.position,
                        Quaternion.identity) as GameObject;
                    laser.transform.position += temp;
                    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                    GameObject laser2 = Instantiate(laserPrefab2, transform.position, Quaternion.identity) as GameObject;
                    laser2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                    AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
                    yield return new WaitForSeconds(projectileFiringPeriod);
                }
                if (iceStart == true)
                {
                    Ice();
                    Vector3 temp = new Vector3(0, 1, 0);
                    GameObject laser = Instantiate(
                        laserPrefab8,
                        transform.position,
                        Quaternion.identity) as GameObject;
                    laser.transform.position += temp;
                    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                    GameObject laser2 = Instantiate(laserPrefab2, transform.position, Quaternion.identity) as GameObject;
                    laser2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                    AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
                    yield return new WaitForSeconds(projectileFiringPeriod);
                }

            }

            if (flameStart == true)
            {

                if (iceStart == false)
                {
                    Vector3 temp = new Vector3(0, 1, 0);
                    Vector3 temp2 = new Vector3(-0.6f, 0, 0);
                    Vector3 temp3 = new Vector3(0.6f, 0, 0);

                    GameObject laser = Instantiate(
                        laserPrefab,
                        transform.position,
                        Quaternion.identity) as GameObject;
                    laser.transform.position += temp;
                    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                   
                    GameObject laser2 = Instantiate(laserPrefab2, transform.position, Quaternion.identity) as GameObject;
                    laser2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                    
                    GameObject laser3 = Instantiate(
                      laserPrefab9, transform.position, Quaternion.identity) as GameObject;
                    laser3.transform.position += temp2;
                    laser3.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

                    GameObject laser4 = Instantiate(
                   laserPrefab9, transform.position, Quaternion.identity) as GameObject;
                    laser4.transform.position += temp3;
                    laser4.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);


                    AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume); 
                    yield return new WaitForSeconds(projectileFiringPeriod);
                }
                if (iceStart == true)
                {
                    Ice();
                    Vector3 temp = new Vector3(0, 1, 0);
                    Vector3 temp2 = new Vector3(-0.6f, 0, 0);
                    Vector3 temp3 = new Vector3(0.6f, 0, 0);


                    GameObject laser = Instantiate(
                        laserPrefab8,
                        transform.position,
                        Quaternion.identity) as GameObject;
                    laser.transform.position += temp;
                    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                    GameObject laser2 = Instantiate(laserPrefab2, transform.position, Quaternion.identity) as GameObject;
                    laser2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                    GameObject laser3 = Instantiate(
                   laserPrefab9, transform.position, Quaternion.identity) as GameObject;
                    laser3.transform.position += temp2;
                    laser3.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

                    GameObject laser4 = Instantiate(
                   laserPrefab9, transform.position, Quaternion.identity) as GameObject;
                    laser4.transform.position += temp3;
                    laser4.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);


                    AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
                    yield return new WaitForSeconds(projectileFiringPeriod);
                }

            }






        } 
    }

    IEnumerator FireContinuously3()  //Power Up 2

    {
        while (superPowerUpTimer > 0)
        { 
         
      
            
            
            if (flameStart == false)
            {

                if (iceStart == false)
                {
                    Vector3 temp = new Vector3(0, 1, 0);
                    GameObject laser = Instantiate(
                        laserPrefab,
                        transform.position,
                        Quaternion.identity) as GameObject;
                    laser.transform.position += temp;
                    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                    GameObject laser3 = Instantiate(laserPrefab3, transform.position, Quaternion.identity) as GameObject;
                    laser3.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                    AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
                    yield return new WaitForSeconds(projectileFiringPeriod);
                }

                if (iceStart == true)
                {
                    Ice();
                    Vector3 temp = new Vector3(0, 1, 0);
                    GameObject laser = Instantiate(
                        laserPrefab8,
                        transform.position,
                        Quaternion.identity) as GameObject;
                    laser.transform.position += temp;
                    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                    GameObject laser3 = Instantiate(laserPrefab3, transform.position, Quaternion.identity) as GameObject;
                    laser3.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                    AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
                    yield return new WaitForSeconds(projectileFiringPeriod);
                }
            }


            if (flameStart == true)
            {

                if (iceStart == false)
                {
                    Vector3 temp = new Vector3(0, 1, 0);
                    Vector3 temp2 = new Vector3(-0.6f, 0, 0);
                    Vector3 temp3 = new Vector3(0.6f, 0, 0);


                    GameObject laser = Instantiate(
                        laserPrefab,
                        transform.position,
                        Quaternion.identity) as GameObject;
                    laser.transform.position += temp;
                    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                    GameObject laser2 = Instantiate(laserPrefab3, transform.position, Quaternion.identity) as GameObject;
                    laser2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                    GameObject laser3 = Instantiate(
                   laserPrefab9, transform.position, Quaternion.identity) as GameObject;
                    laser3.transform.position += temp2;
                    laser3.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

                    GameObject laser4 = Instantiate(
                   laserPrefab9, transform.position, Quaternion.identity) as GameObject;
                    laser4.transform.position += temp3;
                    laser4.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);




                    AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
                    yield return new WaitForSeconds(projectileFiringPeriod);
                }

                if (iceStart == true)
                {
                    Ice();
                    Vector3 temp = new Vector3(0, 1, 0);
                    Vector3 temp2 = new Vector3(-0.6f, 0, 0);
                    Vector3 temp3 = new Vector3(0.6f, 0, 0);


                    GameObject laser = Instantiate(
                        laserPrefab8,
                        transform.position,
                        Quaternion.identity) as GameObject;
                    laser.transform.position += temp;
                    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                    GameObject laser2 = Instantiate(laserPrefab3, transform.position, Quaternion.identity) as GameObject;
                    laser2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                    GameObject laser3 = Instantiate(
                 laserPrefab9, transform.position, Quaternion.identity) as GameObject;
                    laser3.transform.position += temp2;
                    laser3.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

                    GameObject laser4 = Instantiate(
                   laserPrefab9, transform.position, Quaternion.identity) as GameObject;
                    laser4.transform.position += temp3;
                    laser4.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);




                    AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
                    yield return new WaitForSeconds(projectileFiringPeriod);
                }
            }

        }
    }

    IEnumerator FireContinuously4()  //power Up 3

    { 
        while (ultraPowerUpTimer > 0)
        {
          
            if(flameStart == false)
            {
                if (iceStart == false)
                {
                    Vector3 temp = new Vector3(0, 1, 0);
                    GameObject laser = Instantiate(
                        laserPrefab,
                        transform.position,
                        Quaternion.identity) as GameObject;
                    laser.transform.position += temp;
                    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                    GameObject laser4 = Instantiate(laserPrefab4, transform.position, Quaternion.identity) as GameObject;
                    laser4.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                    AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
                    yield return new WaitForSeconds(projectileFiringPeriod);

                }

                if (iceStart == true)
                {
                    Ice();
                    Vector3 temp = new Vector3(0, 1, 0);
                    GameObject laser = Instantiate(
                        laserPrefab8,
                        transform.position,
                        Quaternion.identity) as GameObject;
                    laser.transform.position += temp;
                    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                    GameObject laser4 = Instantiate(laserPrefab4, transform.position, Quaternion.identity) as GameObject;
                    laser4.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                    AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
                    yield return new WaitForSeconds(projectileFiringPeriod);

                }
            }
            if (flameStart == true)
            {
                if (iceStart == false)
                {
                    Vector3 temp = new Vector3(0, 1, 0);
                    Vector3 temp2 = new Vector3(-0.6f, 0, 0);
                    Vector3 temp3 = new Vector3(0.6f, 0, 0);

                    GameObject laser = Instantiate(
                        laserPrefab,
                        transform.position,
                        Quaternion.identity) as GameObject;
                    laser.transform.position += temp;
                    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                  
                    GameObject laser2 = Instantiate(laserPrefab4, transform.position, Quaternion.identity) as GameObject;
                    laser2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

                    GameObject laser3 = Instantiate(
                    laserPrefab9, transform.position, Quaternion.identity) as GameObject;
                    laser3.transform.position += temp2;
                    laser3.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

                    GameObject laser4 = Instantiate(
                   laserPrefab9, transform.position, Quaternion.identity) as GameObject;
                    laser4.transform.position += temp3;
                    laser4.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);


                    AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
                    yield return new WaitForSeconds(projectileFiringPeriod);

                }

                if (iceStart == true)
                {
                    Ice();
                    Vector3 temp = new Vector3(0, 1, 0);
                    Vector3 temp2 = new Vector3(-0.6f, 0, 0);
                    Vector3 temp3 = new Vector3(0.6f, 0, 0);

                    GameObject laser = Instantiate(
                        laserPrefab8,
                        transform.position,
                        Quaternion.identity) as GameObject;
                    laser.transform.position += temp;
                    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                  
                    GameObject laser2 = Instantiate(laserPrefab4, transform.position, Quaternion.identity) as GameObject;
                    laser2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

                    GameObject laser3 = Instantiate(
                    laserPrefab9, transform.position, Quaternion.identity) as GameObject;
                    laser3.transform.position += temp2;
                    laser3.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

                    GameObject laser4 = Instantiate(
                    laserPrefab9, transform.position, Quaternion.identity) as GameObject;
                    laser4.transform.position += temp3;
                    laser4.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

                    AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
                    yield return new WaitForSeconds(projectileFiringPeriod);

                }
            }


        }
    }

    IEnumerator FireContinuously5()  //silver Power
    { while (spriteIndex != 2) { GameObject laser5 = Instantiate(laserPrefab5, transform.position, Quaternion.identity) as GameObject; laser5.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed); AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume); yield return new WaitForSeconds(projectileFiringPeriod); } }

    IEnumerator FireContinuously6()  //Gold Power
    { while (spriteIndex != 3) { GameObject laser6 = Instantiate(laserPrefab6, transform.position, Quaternion.identity) as GameObject; laser6.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed); AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume); yield return new WaitForSeconds(projectileFiringPeriod); } }


    IEnumerator FireContinuously7()  //Mega Power
    {

        while (true)
        {

            GameObject laser7 = Instantiate(
                 laserPrefab7,
                 transform.position,
                 Quaternion.identity) as GameObject;
            laser7.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
             yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }


    private void PowerUpCountDown() { powerUpTimer -= Time.deltaTime; }
    public void PowerUpStart()
    {
        powerUpTimer = 30f;

    }
    private void SuperPowerUpCountDown() { superPowerUpTimer -= Time.deltaTime; }
    public void SuperPowerUpStart()
    {
        superPowerUpTimer = 35f;
    }

    private void UltraPowerUpCountDown() { ultraPowerUpTimer -= Time.deltaTime; }


    public void UltraPowerUpStart()
    {
        ultraPowerUpTimer = 45f;
    }



    public void GoldPowerUpStart()
    {
        if (spriteIndex != 1 && spriteIndex != 2 && spriteIndex != 3)
        {
            health += 1000;
            spriteIndex = 1;
            GetComponent<SpriteRenderer>().sprite = playerSprites[spriteIndex];
            StartCoroutine(FireContinuously5());
        }
        else if (spriteIndex != 2 && spriteIndex != 3)
        {
            health += 1500;
            spriteIndex = 2;
            GetComponent<SpriteRenderer>().sprite = playerSprites[spriteIndex];
            StartCoroutine(FireContinuously6());


        }
        else if (spriteIndex != 3)
        {
            health += 2000;
            spriteIndex = 3;
            GetComponent<SpriteRenderer>().sprite = playerSprites[spriteIndex];
            StartCoroutine(FireContinuously7());
        }
        else
        {
            health += 2000;
        }
    }
 
    public void IcePowerUpStart()
    {

            health += 1500;
            iceStart = true;
            FindObjectOfType<GameSession>().AddToScore2(100000);
       



    }

    public void FlamePowerUpStart()
    {
         health += 1500;
         flameStart = true;
         FindObjectOfType<GameSession>().AddToScore2(100000);

    }


    public void HealthPowerUp()
    {
        health += 200;
    }

    public void AttackSpeedPowerUpStart()
    {

        if (projectileSpeed <= 22f)
        {
            projectileSpeed += 0.35f;
        }
        else
        {
            health += 100;
            FindObjectOfType<GameSession>().AddToScore2(5000);
        }


    }

    public void RateOfFirePowerUpStart()
    {
        if (projectileFiringPeriod >= 0.14f)
        {
            projectileFiringPeriod -= 0.003f;
        }
        else
        {
            health += 100;
            FindObjectOfType<GameSession>().AddToScore2(5000);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }
    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(.32f, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(.68f, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}