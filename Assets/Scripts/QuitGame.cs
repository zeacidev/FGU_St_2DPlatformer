using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void quitGame()
    {
        Application.Quit();

    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
}
