using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuLogic : MonoBehaviour
{
    private GameObject mainMenu;
    private GameObject optionsMenu;
    
    public AudioSource buttonSound;
    // Start is called before the first frame update
    void Start()
    {
        mainMenu = GameObject.Find("MainMenuCanvas");
        optionsMenu = GameObject.Find("OptionsCanvas");

        mainMenu.GetComponent<Canvas>().enabled = true;
        //optionsMenu.GetComponent<Canvas>().enabled = false;
    }

    public void ResumeButton()
    {
        transform.parent.gameObject.SetActive(false);
        buttonSound.Play();
        Time.timeScale = 1f;
        CursorManager.SetPauseState(false);
    }

    public void OptionsButton()
    {
        buttonSound.Play();
        mainMenu.GetComponent<Canvas>().enabled = false;
        optionsMenu.GetComponent<Canvas>().enabled = true;
    }

    public void MainButton()
    {
        buttonSound.Play();
        transform.parent.gameObject.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGameButton()
    {
        buttonSound.Play();
        Application.Quit(); //build에서는 작동함
        Debug.Log("App Has Exited.");
    }

    public void ReturnToMainMenuButton()
    {
    buttonSound.Play();
    mainMenu.GetComponent<Canvas>().enabled = true;
    //optionsMenu.GetComponent<Canvas>().enabled = false;
    }

    void Update()
    {

    }
}
