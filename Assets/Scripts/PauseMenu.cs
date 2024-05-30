using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject hudUI;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
                Debug.Log("Game Resumed");
            }
            else
            {
                Pause();
                Debug.Log("Game Paused");
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        if (hudUI != null)
        {
            hudUI.SetActive(true); 
        }
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        if (hudUI != null)
        {
            hudUI.SetActive(false);
        }
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnPauseButtonClick()
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
}