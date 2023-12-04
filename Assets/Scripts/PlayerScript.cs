using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public AudioSource src;
    public AudioClip shootingAudio, dyingAudio;
    public BulletScript bulletPrefab;
    public float thrustSpeed = 1.0f;
    public float turnSpeed = 1.0f;
    private Rigidbody2D rb2D;
    private bool thrusting;
    private float turnDirection;
    public GameObject pauseMenuUI;
    private bool isShootingBetter = false;
    private float powerUpTimer = 0f;

    

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

   private void Update()
   {
        thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            turnDirection = 1.0f;
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            turnDirection = -1.0f;
        }
        else 
        {
            turnDirection = 0.0f;
        }

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (isShootingBetter)
            {
            // Call ShootBetter with the desired angles
                Shoot(); 
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                     ShootBetter(45f);
                     ShootBetter(135f);
                }
            // ... (similar lines for other shooting directions)
            }
            Shoot();
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }

        // Update the power-up timer
        if (isShootingBetter)
        {
            powerUpTimer -= Time.deltaTime;
            if (powerUpTimer <= 0f)
            {
                DeactivatePowerUp();
            }
        }
   }

   private void FixedUpdate()
   {
        if(thrusting)
        {
            GetComponent<Rigidbody2D>().AddForce(this.transform.up * this.thrustSpeed);
        }
        if(turnDirection != 0.0f)
        {
            rb2D.AddTorque(turnDirection * this.turnSpeed);
        }
   }

   private void Shoot()
   {
        BulletScript bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
        shootSound();
   }

   private void OnCollisionEnter2D(Collision2D collision)
   {
        if(collision.gameObject.tag == "Asteroid")  // if collides with asteroid
        {
            rb2D.velocity = Vector3.zero;   // stops movement
            rb2D.angularVelocity = 0.0f;     //stops rotation
            dyingSound();

            this.gameObject.SetActive(false);   // stops player entirely

            FindObjectOfType<GameManagerScript>().PlayerDied();
        }
   }

    public void ActivatePowerUp(float duration)
    {
        // Activate the ShootBetter mode and set the timer
        isShootingBetter = true;
        powerUpTimer = duration;
    }

    void DeactivatePowerUp()
    {
        // Deactivate the ShootBetter mode
        isShootingBetter = false;
    }

    void ShootBetter(float angle)
    {
        // Convert the player's rotation to radians
        float playerRotation = transform.eulerAngles.z * Mathf.Deg2Rad;

        // Calculate the direction for 45 degrees
        float bulletAngle45 = playerRotation + 45f * Mathf.Deg2Rad;
        Vector2 direction45 = new Vector2(Mathf.Cos(bulletAngle45), Mathf.Sin(bulletAngle45));

        // Calculate the direction for 135 degrees
        float bulletAngle135 = playerRotation + 135f * Mathf.Deg2Rad;
        Vector2 direction135 = new Vector2(Mathf.Cos(bulletAngle135), Mathf.Sin(bulletAngle135));

        // Instantiate and shoot the enhanced bullets in the calculated directions
        BulletScript bullet45 = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet45.Project(direction45);

        BulletScript bullet135 = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet135.Project(direction135);
    }

   public void shootSound()
    {
        src.clip = shootingAudio;
        src.Play();
    }

    public void dyingSound()
    {
        src.clip = dyingAudio;
        src.Play();
    }
}
