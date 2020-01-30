using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawns : MonoBehaviour
{
    public GameObject enemyPrefab;

    private GameObject playerInstance;
    private Vector2 xBounds = new Vector2(64.5f, 74.5f);
    private Vector2 zBounds = new Vector2(30, 40);

    // Start is called before the first frame update
    void Start()
    {
        playerInstance = GetGameObjectInstanceWithTag("Player");

        InvokeRepeating("SpawnEnemyWave", 1.0f, ScoreManager.GetWaveCooldown());
    }

    GameObject GetGameObjectInstanceWithTag(string tagToUse)
    {
        GameObject[] allObjects = (GameObject[])FindObjectsOfType(typeof(GameObject));
        // Iterate through all objects to find canvas
        foreach (GameObject obj in allObjects)
        {
            if (obj.tag == tagToUse)
            {
                return obj;
            }
        }
        return new GameObject();
    }

    void SpawnEnemyWave()
    {
        // Spawn function
        int numberOfEnemies = (int)Mathf.Floor(2.5f * Mathf.Sqrt(100 * ScoreManager.getLevel()));

        for (int eCount = 0; eCount < numberOfEnemies; eCount++)
        {
            float sideChoice = 2 * Random.Range(0, 2) - 1;

            GameObject newEnemy;

            // X sides
            if (Random.Range(0, 1) == 0)
            {
                float xPosition = sideChoice * Random.Range(xBounds.x, xBounds.y);
                float zPosition = Random.Range(-1 * zBounds.x, zBounds.x);
                newEnemy = Instantiate(enemyPrefab, new Vector3(xPosition, 0.9f, zPosition), Quaternion.identity);
            }
            // Z sides
            else
            {
                float xPosition = Random.Range(-1 * xBounds.x, xBounds.x);
                float zPosition = sideChoice * Random.Range(zBounds.x, zBounds.y);
                newEnemy = Instantiate(enemyPrefab, new Vector3(xPosition, 0.9f, zPosition), Quaternion.identity);
            }

            // Set tags for bullet and player collision
            newEnemy.tag = "Enemy";
        }
        
        // Increment level on HUD
        ScoreManager.incrementLevel();
        // Set wave start time for HUD
        ScoreManager.SetLastWaveStartTime(Time.time);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
