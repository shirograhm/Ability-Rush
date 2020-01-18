using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject bulletPrefab;

    // PLAYER STUFF
    private CharacterController controller;
    private float speed;
    private Vector3 facingDirection;
    // BULLET STUFF
    private float lastBulletShotTime = 0.0f;
    private float shootCooldown = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        // Tag for enemy movement toward player
        tag = "Player";
        // Initialize other stuff
        controller = GetComponent<CharacterController>();
        facingDirection = new Vector3(0, 0, 1);
        speed = 10.0f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Set facing direction for bullet velocity
        facingDirection = new Vector3(transform.forward.x, 0.0f, transform.forward.z);

        // Get move input vectors
        Vector3 move = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        // Move player
        controller.SimpleMove(move.normalized * speed);

        if (Input.GetButton("Fire1") && Time.time - lastBulletShotTime > shootCooldown)
        {
            Invoke("Shoot", 0f);
            lastBulletShotTime = Time.time;
        }
    }

    void Shoot()
    {
        GameObject bulletCreated = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        bulletCreated.GetComponent<BulletMovement>().SetInitialNormalizedVelocity(facingDirection);
        bulletCreated.GetComponent<BulletMovement>().SetPlayerObjectInstance(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // TODO: Play OUCH sound effect

            GetComponent<ScoreManager>().changeHealth(-15);
            GetComponent<ScoreManager>().changeScore(-25);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            // Remove coin
            Destroy(other.gameObject, 0f);

            GetComponent<ScoreManager>().changeBank(1);
        }
    }

    void FixedUpdate()
    {
        // float horizontalInput = Input.GetAxis("Horizontal");
        // float verticalInput = Input.GetAxis("Vertical");

        // Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);

        // Vector3 finalMovement = movement.normalized * speed;

        // GetComponent<Rigidbody>().AddForce(finalMovement, ForceMode.Force);
    }
}
