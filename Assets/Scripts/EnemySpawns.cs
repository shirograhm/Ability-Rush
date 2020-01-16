using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawns : MonoBehaviour
{
    public GameObject enemyPrefab;
   
   
   
    private int level = 1;
    private Vector2 xBounds = new Vector2(69, 83);
    private Vector2 zBounds = new Vector2(40, 60);

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemyWave", 10.0f, 30.0f);
    }

    void SpawnEnemyWave() 
    {
        int numberOfEnemies = (int) Mathf.Floor(1.2f * Mathf.Sqrt(100 * level)) - 5;

        for(int eCount = 0; eCount < numberOfEnemies; eCount++) {
            float sideChoice = 2 * Random.Range(0, 2) - 1;
            
            // X sides
            if(Random.Range(0, 1) == 0) {
                float xPosition = sideChoice * Random.Range(xBounds.x, xBounds.y);
                float zPosition = Random.Range(-1 * zBounds.x, zBounds.x);
                Instantiate(enemyPrefab, new Vector3(xPosition, 0.9f, zPosition), Quaternion.identity);
            }
            // Z sides
            else {
                float xPosition = Random.Range(-1 * xBounds.x, xBounds.x);
                float zPosition = sideChoice * Random.Range(zBounds.x, zBounds.y);
                Instantiate(enemyPrefab, new Vector3(xPosition, 0.9f, zPosition), Quaternion.identity);
            }
        }

        level += 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
