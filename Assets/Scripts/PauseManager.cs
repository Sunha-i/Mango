using System.Collections;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject PauseMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool isPaused = !PauseMenu.activeSelf;
            PauseMenu.SetActive(isPaused);
            CursorManager.SetPauseState(isPaused);

            if (isPaused)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
}
