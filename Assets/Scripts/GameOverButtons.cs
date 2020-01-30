using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOverButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        
        Button[] buttons = GetComponentsInChildren<Button>();
        Text[] texts = GetComponentsInChildren<Text>();

        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].tag == "Main Menu")
            {
                buttons[i].onClick.AddListener(MenuClick);
            }
            if (buttons[i].tag == "Restart")
            {
                buttons[i].onClick.AddListener(Restart);
            }
        }

        for(int i = 0; i < texts.Length; i++) {
            if(texts[i].tag == "Final Text")
            {
                string textToWrite = "Final Score: " + ScoreManager.getScore() + "\nYou survived " + (ScoreManager.getLevel() - 1);
                textToWrite += (ScoreManager.getLevel() - 1) > 1 ? " waves!" : " wave!";

                texts[i].text = textToWrite;
            }
        }
    }

    void Update()
    {
        
    }

    void MenuClick() {
        SceneManager.LoadScene("Menu Scene");
    }

    void Restart() {
        SceneManager.LoadScene("Game Scene");
    }
}
