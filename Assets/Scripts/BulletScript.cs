using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D rb2D;

    public float speed = 500.0f;
    public float maxLifeTime = 10.0f;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        rb2D.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.maxLifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);   // if it collides with anything it destroys the bullet
    }
}
