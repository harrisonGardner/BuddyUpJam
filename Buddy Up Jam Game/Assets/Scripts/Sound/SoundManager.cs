using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource _musicSource, _effectsSource;

    [SerializeField]  private List<AudioSource> musicSources = new List<AudioSource>();
    [SerializeField]  private List<AudioSource> effectsSources = new List<AudioSource>();

    private float effectsVolume = 1f;
    private float musicVolume = 1f;

    private bool fadingIn = true;
    private float fadeInTime = 2.5f;
    private float fadeInTimer = 2.5f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded");

        fadingIn = true;
        fadeInTimer = 0f;

        FindSources();
    }

    private void Update()
    {
        if (fadingIn)
        {
            fadeInTimer += Time.deltaTime;

            ChangeEffectsVolume(fadeInTimer/fadeInTime);
            ChangeMusicVolume(fadeInTimer/fadeInTime);

            if (fadeInTimer >= fadeInTime)
            {
                fadingIn = false;
            }
        }
    }

    private void FindSources()
    {
        effectsSources.Clear();
        musicSources.Clear();

        GameObject effectSourceContainer = GameObject.FindGameObjectWithTag("EffectsSources");
        GameObject musicSourceContainer = GameObject.FindGameObjectWithTag("MusicSources");

        for (int i = 0; i < effectSourceContainer.transform.childCount; i++)
        {
            effectsSources.Add(effectSourceContainer.transform.GetChild(i).GetComponent<AudioSource>());
        }

        for (int i = 0; i < musicSourceContainer.transform.childCount; i++)
        {
            musicSources.Add(musicSourceContainer.transform.GetChild(i).GetComponent<AudioSource>());
        }
    }


    public void PlaySound(AudioClip clip)
    {
        _effectsSource.PlayOneShot(clip);
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void ToggleEffects()
    {
        foreach(AudioSource effectSource in effectsSources)
            effectSource.mute = !effectSource.mute;
    }

    public void ToggleMusic()
    {
        foreach (AudioSource musicSource in musicSources)
            musicSource.mute = !musicSource.mute;
    }

    // These methods are currently being used to fade out the sound during the scene transitions.
    // We will need to make something different if we want each type of sound to be controlled individually through the settings
    public void ChangeEffectsVolume(float value)
    {
        effectsVolume = value;
        foreach (AudioSource effectSource in effectsSources)
            effectSource.volume = value;
    }

    public void ChangeMusicVolume(float value)
    {
        musicVolume = value;
        foreach (AudioSource musicSource in musicSources)
            musicSource.volume = value;
    }

    public float GetEffectsVolume()
    {
        return effectsVolume;
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }
}
