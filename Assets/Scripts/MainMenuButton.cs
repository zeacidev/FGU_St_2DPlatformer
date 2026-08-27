using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    public static MainMenuButton instance;
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeSpeed = 2f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeSceneByName(string sceneName)
    {
        StartCoroutine(TransitionAndLoadByName(sceneName));
    }

    private IEnumerator TransitionAndLoadByName(string sceneName)
    {
        fadeImage.raycastTarget = true;
        yield return StartCoroutine(FadeScreen(1f));

        yield return new WaitForSeconds(1.0f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        yield return StartCoroutine(FadeScreen(0f));
        fadeImage.raycastTarget = false;
    }

    public void ChangeSceneByIndex(int sceneIndex)
    {
        StartCoroutine(TransitionAndLoadByIndex(sceneIndex));
    }

    private IEnumerator TransitionAndLoadByIndex(int sceneIndex)
    {
        fadeImage.raycastTarget = true;
        yield return StartCoroutine(FadeScreen(1f));

        yield return new WaitForSeconds(1.0f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        yield return StartCoroutine(FadeScreen(0f));
        fadeImage.raycastTarget = false;
    }

    private IEnumerator FadeScreen(float targetAlpha)
    {
        Color currentColor = fadeImage.color;

        while (!Mathf.Approximately(fadeImage.color.a, targetAlpha))
        {
            currentColor.a = Mathf.MoveTowards(currentColor.a, targetAlpha, fadeSpeed * Time.deltaTime);
            fadeImage.color = currentColor;

            yield return null;
        }
    }

    public IEnumerator FadeScreenPublic(float targetAlpha)
    {
        Color currentColor = fadeImage.color;

        while (!Mathf.Approximately(fadeImage.color.a, targetAlpha))
        {
            currentColor.a = Mathf.MoveTowards(currentColor.a, targetAlpha, fadeSpeed * Time.deltaTime);
            fadeImage.color = currentColor;
            yield return null;
        }
    }
}
