using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource playerDamage;
    public AudioSource bulletShot;
    public AudioSource coinPickup;
    public AudioSource freeze;
    public AudioSource background;
    public AudioSource upgrade;

    // Start is called before the first frame update
    void Start()
    {
        background.loop = true;
        background.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onDestroy() {
        background.Stop();
    }

    public void playPlayerDamageSound() {
        this.playerDamage.Play();
    }
    public void playBulletShotSound()
    {
        this.bulletShot.Play();
    }
    public void playCoinPickupSound()
    {
        this.coinPickup.Play();
    }
    public void playFreezeSound() {
        this.freeze.Play();
    }
    public void playUpgradeSound() {
        this.upgrade.Play();
    }
}
