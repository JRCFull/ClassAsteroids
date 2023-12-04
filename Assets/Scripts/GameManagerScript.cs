using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManagerScript : MonoBehaviour
{
    public PlayerScript player;
    public BulletScript bulletPrefab;
    public float respawnTime = 0.3f;
    public float respawnInvuTime = 3.0f;
    public int lives = 3;
    public int score = 0;
    public int highScore = 0;
    public float shootAngle = 45.0f;
    public Text scoreText;
    public Text highScoreText;

    void Start()
    {
        scoreText.text = score.ToString() + " Points";
        highScore = PlayerPrefs.GetInt("HighScore: ", 0);
        highScoreText.text = "HighScore: " + highScore.ToString();
    }

    public void AsteroidDestroyed(AsteroidScript asteroid)
    {
        if(asteroid.size < 5.0f)
        {
            this.score += 100;
        }
        else if(asteroid.size < 10.0f)
        {
            this.score += 50;
        }
        else
        {
            this.score += 25;
        }
        scoreText.text = "Score: " + score;
    }

    public void PlayerDied()
    {
        this.lives--;

        if(this.lives <= 0) // If player has no more lives
        {
            GameOver();

            Invoke(nameof(Respawn), this.respawnTime);

        }
        else
        {
            Invoke(nameof(Respawn), this.respawnTime);  // will make player setActive(true) after respawn time

        }

    }

    public void addLife()
    {
        this.lives++;
    }


    private void Respawn()
    {
        this.player.transform.position = Vector3.zero;   //puts player back in the middle
        this.player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");  // ignores collisions for invincibility
        this.player.gameObject.SetActive(true); // Changes players set active to true after being set to false from dying in player script
        Invoke(nameof(TurnOnCollisions), this.respawnInvuTime); // after 3 seconds collisions turns back on
    }

    private void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");    // returns player to original layer for collisions
    }

    private void GameOver()
    {
        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore: ", highScore);
        }
        this.lives = 3;
        this.score = 0;
        scoreText.text = score.ToString() + " Points";
        highScoreText.text = "HighScore: " + highScore;
    }


}
