using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TapToContinue : MonoBehaviour
{
    public float delay = 2f;
    public CanvasGroup fadeCanvasGroup;

    private bool isTransitioning = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isTransitioning)
        {
            StartCoroutine(LoadNextSceneWithDelay());
        }
    }

    IEnumerator LoadNextSceneWithDelay()
    {
        isTransitioning = true;

        StartCoroutine(FadeOut());

        yield return new WaitForSeconds(delay);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    IEnumerator FadeOut()
    {
        float fadeDuration = 1f;
        float fadeSpeed = 1 / fadeDuration;
        float progress = 0f;

        while (progress < 1f)
        {
            fadeCanvasGroup.alpha = Mathf.Lerp(1, 0, progress);
            progress += fadeSpeed * Time.deltaTime;
            yield return null;
        }

        fadeCanvasGroup.alpha = 0;
    }
}
