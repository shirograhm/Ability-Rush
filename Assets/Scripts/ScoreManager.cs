using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public GameObject canvasGUI;
    public GameObject sceneManager;

    private int bank;
    private int score;
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        bank = 0;
        score = 0;
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        Text[] textsInGUI = canvasGUI.GetComponentsInChildren<Text>();

        for(int i = 0; i < textsInGUI.Length; i++) {
            if(textsInGUI[i].tag == "Text Score") {
                textsInGUI[i].text = "Score: " + score;
            }
            if(textsInGUI[i].tag == "Text Bank") {
                textsInGUI[i].text = "Coins: " + bank;
            }
        }
    }

    public void changeBank(int amount)
    {
        this.bank += amount;

        print("Bank: " + bank);
    }

    // Returns true if the player is still alive
    // Else returns false
    public void changeHealth(int amount)
    {
        this.health += amount;

        print("Health: " + health);
    }

    public void changeScore(int amount)
    {
        this.score += amount;

        print("Score: " + score);
    }
}
