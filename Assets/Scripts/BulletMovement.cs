using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private GameObject playerInstance;
    private Vector3 normalizedVelocity;
    private float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += normalizedVelocity * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject, 0f);
        }
    }


    // External function used when instantiated
    public void SetInitialNormalizedVelocity(Vector3 velo)
    {
        this.normalizedVelocity = velo;
    }

    public void SetPlayerObjectInstance(GameObject obj)
    {
        this.playerInstance = obj;
    }
}

