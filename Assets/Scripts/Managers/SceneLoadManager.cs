using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

using Define;
using UIs;

public class SceneLoadManager : Singleton<SceneLoadManager>
{
    [SerializeField] private UI_Slide loadingPanel = default;
    private SceneIndex scenes;

    private bool isLoadingPending = false;

    private IEnumerator loadingHandler = null;

    public void LoadScene_Ads(SceneIndex sceneIndex, AdType adType, Action loadSceneAction = null)
    {
        AdManager.Inst.ShowAd(adType, (_, _) =>
        {
            loadingPanel.gameObject.SetActive(true);
            if (loadingHandler == null)
                StartCoroutine(loadingHandler = LoadSceneProcess((int)sceneIndex, loadSceneAction, 1f));
        });
    }

    public void LoadScene_NoAds(SceneIndex sceneIndex, Action loadProceessAction = null)
    {
        loadingPanel.gameObject.SetActive(true);
        if (loadingHandler == null)
            StartCoroutine(loadingHandler = LoadSceneProcess((int)sceneIndex, loadProceessAction, 1f));
    }

    private IEnumerator LoadSceneProcess(int sceneIndex, Action loadProcessAction = null, float intentionalWaitSeconds = 0f)
    {
        loadingPanel.Slide();

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
        asyncOperation.allowSceneActivation = false;

        while (asyncOperation.progress < 0.9f) 
        {
            Debug.Log($"{asyncOperation.progress}");
            yield return null;
        }
        while (loadingPanel.IsPanelSliding) 
        {
            //Debug.Log("IsPanelSliding");
            yield return null;
        }

        loadProcessAction?.Invoke();
        yield return new WaitForSeconds(intentionalWaitSeconds);

        asyncOperation.allowSceneActivation = true;
        while (!asyncOperation.isDone) 
        {
            //Debug.Log("!asyncOperation.isDone");
            yield return null;
        }

        yield return null;

        loadingPanel.Slide(() => loadingPanel.gameObject.SetActive(false));

        loadingHandler = null;
    }
}
