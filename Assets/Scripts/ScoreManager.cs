using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ScoreManager
{
    private static int score = 0;
    private static int health = 100;
    private static int coins = 0;
    private static int level = 1;
    
    private const int MAX_HEALTH = 100;

    private static float lastBulletShot = 0.0f;
    private static float shootCooldown = 0.5f;
    private static float lastWaveStarted = 0.0f;
    private static float waveCooldown = 20.0f;

    private static int multishotLevel = 0;
    private static int multishotCost = 25;
    private static float lastMultishotted = 0.0f;
    private static int freezeCost = 25;
    private static float lastFrozen = 0.0f;

    public static void incrementMultishotLevel() {
        multishotLevel += 1;

        coins -= multishotCost;
        lastMultishotted = Time.time;

        multishotCost *= 2;
    }
    public static int getMultishotLevel() {
        return multishotLevel;
    }
    public static bool canUseMultishot() {
        return coins - multishotCost >= 0 && Time.time - lastMultishotted > 0.5f;
    }
    public static int getMultishotCost() {
        return multishotCost;
    }

    public static void useFreeze(Vector3 playerPosition) {
        Collider[] victims = Physics.OverlapSphere(playerPosition, 12.5f);
        for (int i = 0; i < victims.Length; i++)
        {
            if(victims[i].tag == "Enemy") {
                victims[i].GetComponent<EnemyMovement>().Freeze(Time.time);
            }
        }

        lastFrozen = Time.time;

        coins -= freezeCost;

        freezeCost += 25;
    }
    public static bool canUseFreeze()
    {
        return coins - freezeCost >= 0 && Time.time - lastFrozen > 0.5f;
    }
    public static int getFreezeCost()
    {
        return freezeCost;
    }

    public static bool isGunReady() {
        return Time.time - lastBulletShot > shootCooldown;
    }
    public static void SetLastShotTime(float time) {
        lastBulletShot = time;
    }
    public static float GetGunCooldownPassed() {
        return isGunReady() ? 1.0f : ((Time.time - lastBulletShot) / shootCooldown);
    }

    public static bool isNextWaveReady() {
        return Time.time - lastWaveStarted > waveCooldown;
    }
    public static void SetLastWaveStartTime(float time) {
        lastWaveStarted = time;
    }
    public static float GetWaveCooldown() {
        return waveCooldown;
    }
    public static float GetWaveTimeElapsed() {
        return isNextWaveReady() ? 1.0f : ((Time.time - lastWaveStarted) / waveCooldown);
    }


    public static int getScore() {
        return score;
    }
    public static void changeScore(int delta) {
        score += delta;
    }

    public static int getMaxHealth() {
        return MAX_HEALTH;
    }

    public static int getHealth()
    {
        return health;
    }
    public static void changeHealth(int delta) {
        health += delta;

        if(health <= 0) {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Game Over Scene");

            return;
        }
    }
    
    public static int getCoins()
    {
        return coins;
    }
    public static void changeCoins(int delta) {
        coins += delta;
    }

    public static int getLevel()
    {
        return level;
    }
    public static void incrementLevel() {
        level += 1;
    }

    public static void resetScores() {
        score = 0;
        health = 100;
        level = 1;
        coins = 0;

        multishotLevel = 0;
        multishotCost = 25;
        freezeCost = 50;
    }
}
