using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterShotsScript : MonoBehaviour
{
    public PlayerScript player;
    public float speed = 50.0f;
    private Rigidbody2D rb2D;
    public float maxLifeTime = 30.0f;
    public float powerUpDuration = 10f;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerScript player = other.GetComponent<PlayerScript>();
            if(player != null)
            {
                player.ActivatePowerUp(powerUpDuration);
                Destroy(this.gameObject);
            }
        }
    }

    public void setTrajectory(Vector2 direction)
    {
        rb2D.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.maxLifeTime);
    }

}
