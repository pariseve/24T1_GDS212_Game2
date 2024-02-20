using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject menuObj;
    [SerializeField] private GameObject fadePanel;
    [SerializeField] private float fadeDuration = 1f;

    private Scene currentScene;
    
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void GoToScene()
    {
        StartCoroutine(FadeAndLoad());
    }

    IEnumerator FadeAndLoad()
    {
        fadePanel.SetActive(true);

        float elapsedTime = 0f;
        Color fadeColor = fadePanel.GetComponent<UnityEngine.UI.Image>().color;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            fadeColor.a = alpha;
            fadePanel.GetComponent<UnityEngine.UI.Image>().color = fadeColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        yield return new WaitForSeconds(0.1f); // Wait for a short time to ensure the scene is loaded

        currentScene = SceneManager.GetSceneByName(sceneName);

        elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            fadeColor.a = alpha;
            fadePanel.GetComponent<UnityEngine.UI.Image>().color = fadeColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadePanel.SetActive(false);
    }

    public void OpenPanel()
    {
        menuObj.SetActive(true);
    }

    public void ClosePanel()
    {
        menuObj.SetActive(false);
    }
}
