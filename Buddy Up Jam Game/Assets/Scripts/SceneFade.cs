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
    private bool endGameFade = false;

    public float fadeSpeed = 2.5f;
    private float fadeTimer = 0f;
    private Image fadeImage;
    private TextMeshProUGUI deathMessageUI;

    public float deathMessageTime = 5f;
    private float deathMessageTimer = 0f;


    private string sceneName = "";
    private string deathMessage = "";

    public Button mainMenuButton;

    public float holdBlackScreenBeforeFadeIn = 4f;

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

        mainMenuButton.onClick.AddListener(ResetGame);
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

            if (fadeTimer <= 0)
            {
                fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1);
            }
            else
                fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1 - (fadeTimer / fadeSpeed));

            if (LevelManager.GetLevel() == 0 && deathMessageUI.text != "")
            {
               
                deathMessageUI.color = new Color(1, 1, 1, 1 - (Mathf.Max(fadeTimer, 0) / fadeSpeed));
            }

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
                GetComponent<Canvas>().sortingOrder = 0;
            }
            else
                GetComponent<Canvas>().sortingOrder = 1;
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
        else if (endGameFade)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            deathMessageUI.color = new Color(1, 1, 1, 1);
            mainMenuButton.GetComponent<Image>().raycastTarget = true;
            mainMenuButton.GetComponent<CanvasGroup>().alpha = 1;
            mainMenuButton.interactable = true;
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

    public void GameEndTransition()
    {
        if (!fadeToBlack)
        {
            endGameFade = true;
            fadeToBlack = true;
            fadeDone = false;
            fadeTimer = 0f;
            deathMessageUI.color = new Color(1, 1, 1, 0);
            deathMessageUI.text = "Just a little more and I'll be further ahead than I was before.";
        }
    }

    public void LoadTransition(Scene scene, LoadSceneMode mode)
    {
        fadeIn = true;
        fadeDone = false;
        fadeTimer = 0f;
        deathMessageUI = gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        if (SceneManager.GetActiveScene().name == "Dream World" && LevelManager.GetLevel() == 0)
        {
            deathMessageUI.color = new Color(1, 1, 1, 1);
            deathMessageUI.text = "Space to jump \nSpace while in air to double jump";
            fadeTimer -= holdBlackScreenBeforeFadeIn;
        }
        else if (SceneManager.GetActiveScene().name == "Depression Room" && LevelManager.GetLevel() == 0)
        {
            deathMessageUI.color = new Color(1, 1, 1, 1);
            deathMessageUI.text = "Trigger warning";
            fadeTimer -= holdBlackScreenBeforeFadeIn;
        }
        else
        {
            deathMessageUI.color = new Color(1, 1, 1, 0);
            deathMessageUI.text = "";
        }

    }

    private void ResetGame()
    {
        MainMenu.viewing = true;
        fadeToBlack = false;
        fadeDone = true;
        fadeIn = false;
        endGameFade = false;

        deathMessageUI.color = new Color(1, 1, 1, 0);
        mainMenuButton.GetComponent<Image>().raycastTarget = false;
        mainMenuButton.GetComponent<CanvasGroup>().alpha = 0;
        mainMenuButton.interactable = false;

        LevelManager.SetLevel(0);
        SceneManager.LoadScene("Depression Room");
    }
}
