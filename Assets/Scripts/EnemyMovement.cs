using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject coinPrefab;

    private GameObject playerInstance;
    private float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerInstance = GetPlayerPrefabInstance(playerPrefab);
        if (playerInstance == null) Debug.LogError("Player not instantiated.");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offsetFromPlayer = playerInstance.transform.position - transform.position;

        transform.Translate(offsetFromPlayer.normalized * Time.deltaTime * speed);
    }

    GameObject GetPlayerPrefabInstance(Object myPrefab)
    {
        GameObject[] allObjects = (GameObject[])FindObjectsOfType(typeof(GameObject));
        // Iterate through all objects to find player
        foreach (GameObject obj in allObjects)
        {
            if (obj.tag == "Player")
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
            // Update player score (100pts for enemy kill)
            playerInstance.GetComponent<ScoreManager>().changeScore(100);
            // Drop coins
            int numCoinsToDrop = Random.Range(2, 4);
            for (int i = 0; i < numCoinsToDrop; i++)
            {
                Vector3 variance = new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(-0.4f, 0.4f), Random.Range(-0.4f, 0.4f));
                Instantiate(coinPrefab, transform.position + variance, Quaternion.identity);
            }
            // Destroy enemy
            Destroy(gameObject, 0f);
        }
    }
}
