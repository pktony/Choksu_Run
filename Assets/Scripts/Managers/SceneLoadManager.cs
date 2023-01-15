using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Define;

public class SceneLoadManager : Singleton<SceneLoadManager>
{
    private SceneIndex scenes;

    protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        //switch(scene.buildIndex)
        //{
        //    case (int)SceneIndex.Title:
        //        break;
        //    case (int)SceneIndex.In_Game:
        //        break;
        //    case (int)SceneIndex.Loading:
        //        break;
        //    default:
        //        break;
        //}
    }

    public void LoadScene_Ads(SceneIndex sceneIndex, AdType adType)
    {
        AdManager.Inst.ShowAd(adType);
        AdManager.Inst.InterstitialAd.OnAdClosed += (_,_) =>
        {
            SceneManager.LoadSceneAsync((int)sceneIndex);
        };
    }

    public void LoadScene_NoAds()
    {

    }
}
