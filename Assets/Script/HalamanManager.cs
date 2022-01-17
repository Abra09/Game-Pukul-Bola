using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HalamanManager : MonoBehaviour
{
    public GameObject PanelPause;
    public bool isEscapeToExit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp (KeyCode.Escape))
        {
            if (isEscapeToExit) {
                Application.Quit();
            }
            else {
                KembaliKeMenu();
            }
        }
    }

    public void MulaiPermainan () {
        Time.timeScale = 1;
        SceneManager.LoadScene ("GamePong");
    }

    public void KembaliKeMenu () {
        SceneManager.LoadScene ("HalamanUtama");
    }

    public void PauseControl () {
        if (Time.timeScale ==  1)
        {
            PanelPause.SetActive (true);
            Time.timeScale = 0;
        }else {
            Time.timeScale = 1;
            PanelPause.SetActive (false);
        } 
    }
    public void MenuUtama () {
        Application.LoadLevel("HalamanUtama");
    }
}
