using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject SoundManager;

    // PLAYER STUFF
    private CharacterController controller;
    private float speed;
    private Vector3 facingDirection;

    // Start is called before the first frame update
    void Start()
    {
        // Tag for enemy movement toward player
        tag = "Player";
        // Initialize other stuff
        speed = 10.0f;
        controller = GetComponent<CharacterController>();
        facingDirection = new Vector3(0, 0, 1);
        Cursor.lockState = CursorLockMode.Locked;
        ScoreManager.resetScores();
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

        // Check for firing bullets
        if (Input.GetButton("Fire1") && ScoreManager.isGunReady())
        {
            Invoke("Shoot", 0f);
            ScoreManager.SetLastShotTime(Time.time);
            SoundManager.GetComponent<SoundManager>().playBulletShotSound();
        }

        // Check for upgrade usage
        if(Input.GetButton("Fire2") && ScoreManager.canUseMultishot()) {
            SoundManager.GetComponent<SoundManager>().playUpgradeSound();
            
            ScoreManager.incrementMultishotLevel();
        }

        // Check for forcefield usage
        if(Input.GetButton("Fire3") && ScoreManager.canUseFreeze()) {
            SoundManager.GetComponent<SoundManager>().playFreezeSound();
            GetComponent<ParticleSystem>().Play();
            
            ScoreManager.useFreeze(transform.position);
        }
    }

    void Shoot()
    {
        // Initial shot
        GameObject bulletCreated = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        bulletCreated.GetComponent<BulletMovement>().SetInitialNormalizedVelocity(facingDirection);
        bulletCreated.GetComponent<BulletMovement>().SetPlayerObjectInstance(gameObject);

        // Multishots
        for(int i = 0; i < ScoreManager.getMultishotLevel(); i++) {
            Vector3 variance = new Vector3(Random.Range(-0.2f, 0.2f), 0, Random.Range(-0.2f, 0.2f));

            GameObject extraShot = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            extraShot.GetComponent<BulletMovement>().SetInitialNormalizedVelocity(facingDirection + variance);
            extraShot.GetComponent<BulletMovement>().SetPlayerObjectInstance(gameObject);
        }


        ScoreManager.changeScore(-2);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            ScoreManager.changeHealth(-10);
            ScoreManager.changeScore(-25);

            SoundManager.GetComponent<SoundManager>().playPlayerDamageSound();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            // Remove coin
            Destroy(other.gameObject, 0f);

            ScoreManager.changeCoins(1);
            ScoreManager.changeScore(5);

            SoundManager.GetComponent<SoundManager>().playCoinPickupSound();
        }
    }
}
