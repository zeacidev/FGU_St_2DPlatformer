using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuObject;

    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Cancel"))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenuObject.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuObject.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ToggleGame()
    {
        if (isPaused)
        {
            pauseMenuObject.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            pauseMenuObject.SetActive(true);
            Time.timeScale = 0f;
        }
        isPaused = !isPaused;
    }
}