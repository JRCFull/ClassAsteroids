using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public float size = 1.0f;
    public float minSize = 3.0f;
    public float maxSize = 15.0f;    // max size for asteroid
    public float speed = 50.0f;
    public float maxLifeTime = 30.0f;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2D;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f); // changes the rotation so each looks different
        this.transform.localScale = Vector3.one * this.size;  

        rb2D.mass = this.size;   
    }
    
    public void setTrajectory(Vector2 direction)
    {
        rb2D.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.maxLifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")     //when the asteroid collides with an object with the tag "Bullet"
        {
            if((this.size * 0.5f) >= this.minSize)  // checks size of asteroid thats hit with bullet
            {
                CreateSplit();
                CreateSplit();
            }
            FindObjectOfType<GameManagerScript>().AsteroidDestroyed(this);
            Destroy(this.gameObject);   // Destroys current object
        }
    }

    private void CreateSplit()
    {
        Vector2 position = this.transform.position;
        position+= Random.insideUnitCircle * 0.5f;  // when a pair of asteroids spawn, this makes it so they dont spawn right on top of one another, they spawn randomly in a circlular area 

        AsteroidScript half = Instantiate(this, position, this.transform.rotation);   // copy current asteroid, random position, rotation is same as parent
        half.size  = this.size * 0.5f; // sets size of new asteroid to be half of original
        half.setTrajectory(Random.insideUnitCircle.normalized * this.speed);   // different traj from parent

    }

}
