using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject coinPrefab;

    private Color frozenColor = new Color(0.22f, 0.32f, 0.78f, 1.0f);
    private Color normalColor = new Color(0.76f, 0.0f, 0.05f, 1.0f);

    private GameObject playerInstance;
    private const float SPEED = 7.5f;
    private bool isFrozen = false;
    private float timeFrozen = 0.0f;
    private const float FREEZETIME = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        playerInstance = GetGameObjectInstanceWithTag("Player");
        if (playerInstance == null) Debug.LogError("Player not instantiated.");
    }

    // Update is called once per frame
    void Update()
    {
        if(isFrozen) {
            if(Time.time - FREEZETIME >= timeFrozen) {
                Destroy(gameObject, 0.0f);
                // Enemy kill
                EnemyDied();
            }
            GetComponent<Renderer>().material.color = frozenColor;
            return;
        }

        GetComponent<Renderer>().material.color = normalColor;
        // Get normalized, flattened vector for enemy movement
        Vector3 enemyMovement = playerInstance.transform.position - transform.position;
        enemyMovement.y = 0;
        enemyMovement = enemyMovement.normalized;
        // Move enemy
        transform.Translate(enemyMovement * Time.deltaTime * SPEED);
    }

    public void Freeze(float time) {
        this.timeFrozen = time;
        this.isFrozen = true;
    }

    GameObject GetGameObjectInstanceWithTag(string tagToUse)
    {
        GameObject[] allObjects = (GameObject[])FindObjectsOfType(typeof(GameObject));
        // Iterate through all objects to find player
        foreach (GameObject obj in allObjects)
        {
            if (obj.tag == tagToUse)
            {
                return obj;
            }
        }
        return new GameObject();
    }

    void OnTriggerEnter(Collider other)
    {
        // On death
        if (other.gameObject.tag == "Bullet")
        {
            // Destroy bullet
            Destroy(other.gameObject, 0f);
            
            EnemyDied();
        }
    }

    void EnemyDied() {
        ScoreManager.changeScore(100);
        // Drop coins
        int numCoinsToDrop = Random.Range(1, 3);
        for (int i = 0; i < numCoinsToDrop; i++)
        {
            Vector3 variance = new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(-0.4f, 0.4f), Random.Range(-0.4f, 0.4f));
            Instantiate(coinPrefab, transform.position + variance, Quaternion.identity);
        }
        // Destroy enemy
        Destroy(gameObject, 0f);
    }
}
