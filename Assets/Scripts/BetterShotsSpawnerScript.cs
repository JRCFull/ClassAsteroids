using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterShotsSpawner : MonoBehaviour
{
    public BetterShotsScript betterShot;
    public float trajectoryVariance = 15.0f;
    public float spawnRate = 45.0f;
    public int spawnAmount = 1;
    public float spawnDistance = 15.0f;   // spawns asteroid 15f away from spawners position
    
    private void Start()
    {
        InvokeRepeating(nameof(SpawnBetterShots), this.spawnRate, this.spawnRate); // every two seconds spawn will be called
    }

    private void SpawnBetterShots()
    {
        for(int i = 0; i < this.spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;   // randomly spawns
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            BetterShotsScript betterShot = Instantiate(this.betterShot, spawnPoint, rotation);

            betterShot.setTrajectory(rotation * -spawnDirection);
        }
    }


}
