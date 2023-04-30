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

    public void LoadScene_Ads(SceneIndex sceneIndex, AdType adType)
    {
        AdManager.Inst.ShowAd(adType);
        AdManager.Inst.InterstitialAd.OnAdClosed += (_,_) =>
        {
            if (loadingHandler == null)
                StartCoroutine(loadingHandler = LoadSceneProcess((int)sceneIndex, 1f));
        };
    }

    public void LoadScene_NoAds(SceneIndex sceneIndex)
    {
        if (loadingHandler == null)
            StartCoroutine(loadingHandler = LoadSceneProcess((int)sceneIndex));
    }

    private IEnumerator LoadSceneProcess(int sceneIndex, float intentionalWaitSeconds = 0f)
    {
        loadingPanel.gameObject.SetActive(true);
        loadingPanel.Slide();

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
        asyncOperation.allowSceneActivation = false;

        while (asyncOperation.progress < 0.9f) yield return null;
        while (loadingPanel.IsPanelSliding) yield return null;

        yield return new WaitForSeconds(intentionalWaitSeconds);

        asyncOperation.allowSceneActivation = true;
        while (!asyncOperation.isDone) yield return null;


        loadingPanel.Slide(() => this.gameObject.SetActive(false));

        loadingHandler = null;
    }
}
