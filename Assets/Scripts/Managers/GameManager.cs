using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [Header("Managers")]
    public UIManager ui;
    public SoundManager sound;
    public ResourceManager resource;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }


    public void GameOver()
    {

    }

    
}
