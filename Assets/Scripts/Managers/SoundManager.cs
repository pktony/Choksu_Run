using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class SoundManager : MonoBehaviour, IBootingComponent
{
  
    public float masterVolumeSFX = 1f;
    public float masterVolumeBGM = 1f;

    // TODO : save setting value in device;
    private float lastMasterVolumeSFX;
    private float lastMasterVolumeBGM;

    [SerializeField] AudioClip[] BGMClips;
    [SerializeField] AudioClip[] SFXClips;

    private AudioSource sfxPlayer;
    private AudioSource bgmPlayer;

    private Coroutine coroutine = null;

    private bool isReady = false;
    public bool IsReady => isReady;

    public void Initialize()
    {
        sfxPlayer = GameManager.Inst.gameObject.AddComponent<AudioSource>();
        bgmPlayer = GameManager.Inst.gameObject.AddComponent<AudioSource>();

        isReady = true;
    }
    #region BGM
    public void PlayBGM(BGM bgm, int Type) // Type 0 : �ٷ� ���
    {
        SetupBGM(bgm);
        Action bgmPlay = Type.Equals(0) ? bgmPlayer.Play : FadeIn;
        bgmPlay.Invoke();
    }

    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    private void FadeIn() //BGM Fade In
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(Fading());
    }

    private IEnumerator Fading()
    {
        float fadeDeltaTime = 0f;
        float fadeSeconds = 2f;

        bgmPlayer.volume = 0f;
        bgmPlayer.Play();

        for (; ; )
        {
            fadeDeltaTime += Time.deltaTime;
            if (fadeDeltaTime >= fadeSeconds)
            {
                fadeDeltaTime = fadeSeconds;
                break;
            }
            bgmPlayer.volume = (fadeDeltaTime / fadeSeconds);

            yield return null;
        }
    }

    private void SetupBGM(BGM bgm)
    {
        if (BGMClips.Length < 1)
        {
            Debug.LogError($"There is no BGM Resource in Array");
            return;
        }
        bgmPlayer.clip = BGMClips[(int)bgm];
        bgmPlayer.volume = masterVolumeBGM;
    }

    #endregion

    #region SFX

    public void PlaySFXDelay(SFX _sfx, float delayTime) => StartCoroutine(Play_Delay(_sfx, delayTime));

    public void PlaySFX(SFX _sfx)
    {
        if (SFXClips == null)
        {
            Debug.LogError($"There is no SFX Resource.. Plz Insert SFX Resource in Hierarchy");
            return;
        }
        sfxPlayer.PlayOneShot(SFXClips[(int)_sfx]);
    }

    private IEnumerator Play_Delay(SFX _sfx, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        sfxPlayer.PlayOneShot(SFXClips[(int)_sfx]);
    }

    #endregion

    #region �ɼǿ��� ��������
    public void SetVolumeSFX(float a_volume)
    {
        masterVolumeSFX = a_volume;
    }

    public void SetVolumeBGM(float a_volume)
    {
        masterVolumeBGM = a_volume;
        bgmPlayer.volume = masterVolumeBGM;
    }

    public void MuteAll()
    {
        lastMasterVolumeBGM = masterVolumeBGM;
        lastMasterVolumeSFX = masterVolumeSFX;

        SetVolumeBGM(0f);
        SetVolumeSFX(0f);
    }

    public void UnMuteAll()
    {
        SetVolumeBGM(lastMasterVolumeBGM);
        SetVolumeSFX(lastMasterVolumeSFX);
    }
    #endregion
}
