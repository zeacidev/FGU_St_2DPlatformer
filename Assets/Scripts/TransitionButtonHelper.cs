using UnityEngine;
using UnityEngine.UI; // Required to talk to the button component

public class TransitionButtonHelper : MonoBehaviour
{
    [SerializeField] private string targetSceneName;
    [SerializeField] private int targetSceneIndex = -1;

    void Awake()
    {
        // Automatically hook up our click detector via code
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(OnButtonClicked);
        }
    }

    private void OnButtonClicked()
    {
        // Double-check that our persistent SceneManager object successfully traveled to this scene
        if (MainMenuButton.instance != null)
        {
            if (!string.IsNullOrEmpty(targetSceneName))
            {
                MainMenuButton.instance.ChangeSceneByName(targetSceneName);
            }
            else if (targetSceneIndex != -1)
            {
                MainMenuButton.instance.ChangeSceneByIndex(targetSceneIndex);
            }
            else
            {
                Debug.LogError("No target");
            }
        }
        else
        {
            Debug.LogError("Helper Error: Couldn't find MainMenuButton.instance in this scene!");
        }
    }
}
