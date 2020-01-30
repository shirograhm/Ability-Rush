using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    private Color HP_RED = new Color(0.79f, 0.09f, 0.0f, 1.0f);
    private Color HP_YELLOW = new Color(0.79f, 0.69f, 0.0f, 1.0f);
    private Color HP_GREEN = new Color(0.0f, 0.65f, 0.16f, 1.0f);

    // Update is called once per frame
    void Update()
    {
        Text[] textsInGUI = GetComponentsInChildren<Text>();
        Image[] imagesInGUI = GetComponentsInChildren<Image>();

        for(int i = 0; i < textsInGUI.Length; i++) {
            if(textsInGUI[i].tag == "Text Score") {
                textsInGUI[i].text = "Score: " + ScoreManager.getScore();
            }
            if(textsInGUI[i].tag == "Text Bank") {
                textsInGUI[i].text = "Coins: " + ScoreManager.getCoins();
            }
            if(textsInGUI[i].tag == "Text Wave Count") {
                string textToPut = "WAVE " + (ScoreManager.getLevel() - 1);
                for(int j = 0; j < ScoreManager.getLevel() / 10; j++) {
                    textToPut += "!";
                }

                textsInGUI[i].text = textToPut;
            }
            if(textsInGUI[i].tag == "Multishot Cost")
            {
                textsInGUI[i].text = ScoreManager.getMultishotCost() + " coins";
            }
            if (textsInGUI[i].tag == "Forcefield Cost")
            {
                textsInGUI[i].text = ScoreManager.getFreezeCost() + " coins";
            }
        }

        for(int i = 0; i < imagesInGUI.Length; i++) {
            if(imagesInGUI[i].tag == "HP Fill") {
                float currentHealth = 1.0f * ScoreManager.getHealth() / ScoreManager.getMaxHealth();
                // Set HP level
                imagesInGUI[i].fillAmount = currentHealth;
                // Set HP color
                if(currentHealth < 0.3f) {
                    imagesInGUI[i].color = HP_RED;
                }
                else if(currentHealth < 0.7f) {
                    imagesInGUI[i].color = HP_YELLOW;
                }
                else {
                    imagesInGUI[i].color = HP_GREEN;
                }
            }
            if(imagesInGUI[i].tag == "CD Fill") {
                // Set CD
                imagesInGUI[i].fillAmount = ScoreManager.GetGunCooldownPassed();
            }
            if(imagesInGUI[i].tag == "Wave Fill") {
                // Set wave bar
                imagesInGUI[i].fillAmount = ScoreManager.GetWaveTimeElapsed();
            }
            if(imagesInGUI[i].tag == "Multishot Icon")
            {
                imagesInGUI[i].fillAmount = ScoreManager.canUseMultishot() ? 1.0f : (1.0f * ScoreManager.getCoins() / ScoreManager.getMultishotCost());
            }
            if (imagesInGUI[i].tag == "Forcefield Icon")
            {
                imagesInGUI[i].fillAmount = ScoreManager.canUseFreeze() ? 1.0f : (1.0f * ScoreManager.getCoins() / ScoreManager.getFreezeCost());
            }
        }
    }
}
