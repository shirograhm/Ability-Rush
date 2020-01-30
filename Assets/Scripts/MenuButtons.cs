using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{

    private Button[] buttons;
    void Start() {
        Cursor.lockState = CursorLockMode.Confined;
        
        buttons = GetComponentsInChildren<Button>();
        
        for(int i = 0; i < buttons.Length; i++) {
            if(buttons[i].tag == "Begin Game") {
                buttons[i].onClick.AddListener(BeginGame);
            }
            if (buttons[i].tag == "Exit Game") {
                buttons[i].onClick.AddListener(ExitGame);
            }
        }
    }

    void MouseLeave() {
        GetComponent<Text>().color = Color.black;
    }

    void MouseEnter() {
        GetComponent<Text>().color = Color.white;
    }

    void BeginGame() {
        SceneManager.LoadScene("Game Scene");
    }

    void ExitGame() {
        Application.Quit();
    }
}
