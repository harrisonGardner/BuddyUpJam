using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
    public static SceneFade Instance;

    private bool fadeToBlack = false;
    private bool fadeIn = false;
    private bool fadeDone = false;

    public float fadeSpeed = 2.5f;
    private float fadeTimer = 0f;
    private Image fadeImage;

    private string sceneName = "";

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
        SceneManager.sceneLoaded += LoadTransition;
    }

    private void Start()
    {
        fadeImage = gameObject.transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeToBlack && !fadeDone)
        {
            fadeTimer += Time.deltaTime;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeTimer / fadeSpeed);

            SoundManager.Instance.ChangeEffectsVolume(1 - fadeTimer / fadeSpeed);
            SoundManager.Instance.ChangeMusicVolume(1 - fadeTimer/fadeSpeed);

            if (fadeTimer >= fadeSpeed)
            {
                fadeDone = true;
            }
        }
        else if (fadeIn && !fadeDone)
        {
            fadeTimer += Time.deltaTime;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1 - (fadeTimer / fadeSpeed));

            SoundManager.Instance.ChangeEffectsVolume(fadeTimer / fadeSpeed);
            SoundManager.Instance.ChangeMusicVolume(fadeTimer / fadeSpeed);

            if (fadeTimer >= fadeSpeed)
            {
                fadeDone = true;
            }
        }
        else if (fadeDone && fadeToBlack)
        {
            //Transition scene or whatever else
            fadeToBlack = false;
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }

    public void SceneTransition(string sceneName)
    {
        if (!fadeToBlack)
        {
            this.sceneName = sceneName;
            fadeToBlack = true;
            fadeDone = false;
            fadeTimer = 0f;
        }
    }

    public void LoadTransition(Scene scene, LoadSceneMode mode)
    {
        fadeIn = true;
        fadeDone = false;
        fadeTimer = 0f;
    }
}
