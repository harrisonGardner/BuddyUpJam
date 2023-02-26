using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
    public static SceneFade Instance;

    private bool fadeToBlack = false;
    private bool fadeIn = false;
    private bool fadeDone = false;
    private bool deathMessageFade = false;

    public float fadeSpeed = 2.5f;
    private float fadeTimer = 0f;
    private Image fadeImage;
    private TextMeshProUGUI deathMessageUI;

    public float deathMessageTime = 5f;
    private float deathMessageTimer = 0f;


    private string sceneName = "";
    private string deathMessage = "";

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
        deathMessageUI = gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeToBlack && !fadeDone)
        {
            fadeTimer += Time.deltaTime;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeTimer / fadeSpeed);

            if (deathMessageFade)
            {
                deathMessageUI.color = new Color(1, 1, 1, fadeTimer / fadeSpeed);
            }

            SoundManager.Instance.ChangeEffectsVolume(1 - fadeTimer / fadeSpeed);
            SoundManager.Instance.ChangeMusicVolume(1 - fadeTimer / fadeSpeed);

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

            if (deathMessageFade)
            {
                deathMessageUI.color = new Color(1, 1, 1, 1 - fadeTimer / fadeSpeed);
            }

            if (fadeTimer >= fadeSpeed)
            {
                fadeDone = true;
                fadeIn = false;
                deathMessageFade = false;
            }
        }
        else if (deathMessageFade)
        {
            deathMessageTimer -= Time.deltaTime;
            if (deathMessageTimer <= 0)
            {
                fadeToBlack = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
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
            deathMessageUI.color = new Color(1, 1, 1, 0);
            this.sceneName = sceneName;
            fadeToBlack = true;
            fadeDone = false;
            fadeTimer = 0f;
        }
    }

    public void DeathMessage(string message)
    {
        if (!fadeToBlack)
        {
            deathMessage = message;
            deathMessageUI.text = message;
            deathMessageFade = true;
            deathMessageUI.color = new Color(1,1,1,0);
            deathMessageTimer = deathMessageTime;
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
